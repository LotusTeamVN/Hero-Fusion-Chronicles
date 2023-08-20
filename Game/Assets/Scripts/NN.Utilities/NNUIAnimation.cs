using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NN.Utilities
{
    [RequireComponent(typeof(CanvasGroup))]
    public class NNUIAnimation : MonoBehaviour
    {
        public enum Direction { None, Left, Right, Top, Bottom }

        public class AnimationBehavior
        {
            public UnityEvent OnStart = new UnityEvent();
            public UnityEvent OnFinish = new UnityEvent();
        }

        [System.Serializable]
        public class Parameter
        {
            public bool animationRoot = false;
            public RectTransform rect;
            public RectTransform container;
        }

        [System.Serializable]
        public class AnimationBase
        {
            public bool enable = false;
            public Ease easyType = Ease.Linear;
            public float duration = 0.5f;
            public float delayTime = 0f;
        }

        [System.Serializable]
        public class MoveAnimation : AnimationBase
        {
            public Direction direction = Direction.None;
        }

        [System.Serializable]
        public class ScaleAnimation : AnimationBase
        {
            public Vector3 value = Vector3.zero;
        }

        [System.Serializable]
        public class FadeAnimation : AnimationBase
        {
            [Range(0, 1)]
            public float value = 1;
        }

        [System.Serializable]
        public class PlaceAnimation
        {
            public MoveAnimation moveAnimation = null;
            public ScaleAnimation scaleAnimation = null;
            public FadeAnimation fadeAnimation = null;
        }

        [Space(10)]
        public Parameter parameter = null;
        [Space(10)]
        public PlaceAnimation showAnimation = null;
        [Space(10)]
        public PlaceAnimation hideAnimation = null;

        public RectTransform Rect => this.TryGetComponent(ref parameter.rect);

        public RectTransform Container
        {
            get
            {
                if (parameter.container == null)
                    parameter.container = transform.Find("Container") as RectTransform;
                return parameter.container;
            }
        }

        private CanvasGroup _canvasGroup = null;
        public CanvasGroup CanvasGroup => this.TryGetComponent(ref _canvasGroup);

        public AnimationBehavior ShowBehavior = new AnimationBehavior();
        public AnimationBehavior HideBehavior = new AnimationBehavior();

        public bool AnimationFinished => countTweenFinish != 0 && countTweenFinish >= tweenActive;
        public bool IsVisible => CanvasGroup.alpha != 0;

        private int tweenActive = 0;
        private int countTweenFinish = 0;

        private NNUIPopup popup = null;
        private Vector3 defaultAnchorPos = Vector3.zero;
        private Vector3 defaultScale = Vector3.zero;

        private Tween moveTwwen = null;
        private Tween scaleTween = null;
        private Tween fadeTween = null;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (defaultAnchorPos == Vector3.zero)
                defaultAnchorPos = Container.anchoredPosition;

            if (defaultScale == Vector3.zero)
                defaultScale = Container.localScale;

            if (popup == null)
                popup = GetComponent<NNUIPopup>();

            CanvasGroup.DeActive();
        }

        public void DoShowAnimation()
        {
            ShowBehavior.OnStart?.Invoke();

            MoveAnimation moveAnimation = showAnimation?.moveAnimation ?? null;
            ScaleAnimation scaleAnimation = showAnimation?.scaleAnimation ?? null;
            FadeAnimation fadeAnimation = showAnimation?.fadeAnimation ?? null;

            if (!moveAnimation.enable && !scaleAnimation.enable && !fadeAnimation.enable)
            {
                ShowBehavior.OnFinish?.Invoke();
                return;
            }

            KillAnimation();

            CountTweenActive(showAnimation);
            countTweenFinish = 0;
            void OnFinish()
            {
                countTweenFinish++;
                if (countTweenFinish == tweenActive)
                {
                    CanvasGroup.Active();
                    ShowBehavior.OnFinish?.Invoke();
                }
            }

            if (fadeAnimation != null && fadeAnimation.enable)
            {
                CanvasGroup.alpha = 0;
                fadeTween = CanvasGroup.DOFade(fadeAnimation.value, fadeAnimation.duration).SetDelay(fadeAnimation.delayTime).SetEase(fadeAnimation.easyType).OnUpdate(() =>
                {
                    if (this == null) return;
                }).OnComplete(() =>
                {
                    OnFinish();
                });
            }

            if (moveAnimation != null && moveAnimation.enable)
            {
                moveTwwen = Container.DOAnchorPos(defaultAnchorPos, moveAnimation.duration).OnUpdate(() =>
                {
                    if (this == null) return;
                }).SetEase(moveAnimation.easyType).SetDelay(moveAnimation.delayTime).OnComplete(() =>
                {
                    ShowBehavior.OnFinish?.Invoke();
                }).OnComplete(() =>
                {
                    OnFinish();
                });
            }

            if (scaleAnimation != null && scaleAnimation.enable)
            {
                scaleTween = Container.DOScale(defaultScale, scaleAnimation.duration).OnUpdate(() =>
                {
                    if (this == null) return;
                }).SetEase(scaleAnimation.easyType).SetDelay(scaleAnimation.delayTime).OnComplete(() =>
                {
                    OnFinish();
                });
            }

            if (!Application.isPlaying)
                PreViewAnimation();
        }

        public void SetPositionOnShow()
        {
            //if (!Application.isPlaying)
                ResetDefaultUI();

            if (parameter.animationRoot)
            {
                Rect.anchorMin = new Vector2(0f, 0f);
                Rect.anchorMax = new Vector2(1f, 1f);
                Rect.pivot = new Vector2(0.5f, 0.5f);

                Rect.offsetMin = Vector2.zero;
                Rect.offsetMax = Vector2.zero;

                Rect.anchoredPosition3D = Vector3.zero;
            }
            
            Container.anchoredPosition3D = defaultAnchorPos;

            MoveAnimation moveAnimation = showAnimation?.moveAnimation ?? null;

            if (moveAnimation != null && moveAnimation.enable)
                Container.anchoredPosition = GetStartDirection(moveAnimation.direction);

            ScaleAnimation scaleAnimation = showAnimation?.scaleAnimation ?? null;

            if (scaleAnimation != null && scaleAnimation.enable)
                Container.localScale = scaleAnimation.value;

            if (parameter.animationRoot)
                LayoutRebuilder.MarkLayoutForRebuild(Rect);
            CanvasGroup.Active();
        }

        public void DoHideAnimation()
        {
            HideBehavior.OnStart?.Invoke();

            if (!Application.isPlaying)
                ResetDefaultUI();

            MoveAnimation moveAnimation = hideAnimation?.moveAnimation ?? null;
            ScaleAnimation scaleAnimation = hideAnimation?.scaleAnimation ?? null;
            FadeAnimation fadeAnimation = hideAnimation?.fadeAnimation ?? null;

            if (!moveAnimation.enable && !scaleAnimation.enable && !fadeAnimation.enable)
            {
                HideBehavior.OnFinish?.Invoke();
                return;
            }

            KillAnimation();

            CountTweenActive(hideAnimation);
            countTweenFinish = 0;
            void OnFinish()
            {
                countTweenFinish++;
                if (countTweenFinish == tweenActive)
                {
                    CanvasGroup.DeActive();
                    HideBehavior.OnFinish?.Invoke();
                }
            }

            if (fadeAnimation != null && fadeAnimation.enable)
            {
                fadeTween = CanvasGroup.DOFade(fadeAnimation.value, fadeAnimation.duration).SetDelay(fadeAnimation.delayTime).SetEase(fadeAnimation.easyType).OnUpdate(() =>
                {
                    if (this == null) return;
                }).OnComplete(() =>
                {
                    OnFinish();
                });
            }

            Vector3 endValue = GetStartDirection(moveAnimation.direction) * -1;

            if (moveAnimation != null && moveAnimation.enable)
            {
                moveTwwen = Container.DOAnchorPos(endValue, moveAnimation.duration).SetEase(moveAnimation.easyType).SetDelay(moveAnimation.delayTime).OnUpdate(() =>
                {
                    if (this == null) return;
                }).OnComplete(() =>
                {
                    OnFinish();
                });
            }

            if (scaleAnimation != null && scaleAnimation.enable)
            {
                scaleTween = Container.DOScale(scaleAnimation.value, scaleAnimation.duration).SetEase(scaleAnimation.easyType).OnUpdate(() =>
                {
                    if (this == null) return;
                }).SetDelay(scaleAnimation.delayTime).OnComplete(() =>
                {
                    OnFinish();
                });
            }

            if (!Application.isPlaying)
                PreViewAnimation();
        }

        private void KillAnimation()
        {
            moveTwwen.Stop();
            scaleTween.Stop();
            fadeTween.Stop();
        }

        public void PreViewAnimation()
        {
#if UNITY_EDITOR
            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(moveTwwen);
            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(scaleTween);
            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(fadeTween);
            DG.DOTweenEditor.DOTweenEditorPreview.Start();
#endif
        }

        public void ResetDefaultUI()
        {
            if (parameter.animationRoot)
                Rect.anchoredPosition = Vector2.zero;
            Container.anchoredPosition = defaultAnchorPos;
            Container.localScale = defaultScale;
            CanvasGroup.Active();
        }

        private void CountTweenActive(PlaceAnimation place)
        {
            tweenActive = 0;
            if (place.moveAnimation != null && place.moveAnimation.enable)
                tweenActive++;
            if (place.scaleAnimation != null && place.scaleAnimation.enable)
                tweenActive++;
            if (place.fadeAnimation != null && place.fadeAnimation.enable)
                tweenActive++;
        }

        public Vector2 GetStartDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Vector2((Screen.width + parameter.container.sizeDelta.x), 0) * -1;
                case Direction.Right:
                    return new Vector2((Screen.width + parameter.container.sizeDelta.x), 0);
                case Direction.Top:
                    return new Vector2(0, (Screen.height + parameter.container.sizeDelta.y) / 2);
                case Direction.Bottom:
                    return new Vector2(0, (Screen.height + parameter.container.sizeDelta.y) / 2) * -1;
            }
            return Vector2.zero;
        }
    }
}
