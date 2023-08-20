using System.Linq;
using UnityEngine;

namespace NN.Utilities
{
    [RequireComponent(typeof(CanvasGroup))]
    public class NNUIPopup : MonoBehaviour, IPool
    {
        private string popupName = string.Empty;
        public string Name 
        {
            get
            {
                if (string.IsNullOrEmpty(popupName))
                    popupName = gameObject.name.Replace("(Clone)", "");
                return popupName;
            }
        }

        private CanvasGroup _canvasGroup = null;
        public CanvasGroup CanvasGroup => this.TryGetComponent(ref _canvasGroup);

        private NNUIAnimation[] _animations = null;
        public NNUIAnimation[] Animations => this.TryGetComponentsInChildren(ref _animations);

        private NNUIAnimation _animationRoot = null;
        public NNUIAnimation AniamtionRoot 
        {
            get
            {
                if (_animationRoot == null)
                    _animationRoot = Animations.First(a => a.parameter.animationRoot);
                return _animationRoot;
            }
        }

        public NNUIAnimation.AnimationBehavior ShowBehavior 
        { 
            get => AniamtionRoot.ShowBehavior;
            set => AniamtionRoot.ShowBehavior = value; 
        }

        public NNUIAnimation.AnimationBehavior HideBehavior 
        { 
            get => AniamtionRoot.HideBehavior;
            set => AniamtionRoot.HideBehavior = value; 
        }

        public bool AnimationFinished => AniamtionRoot.AnimationFinished;
        public bool IsVisible => AniamtionRoot.IsVisible;
        


        public void Show()
        {
            CanvasGroup.Active();
            foreach (var animation in Animations)
            {
                if (!Application.isPlaying)
                    animation.Initialize();
                animation.SetPositionOnShow();
                animation.DoShowAnimation();
            }
        }

        public void Hide()
        {
            AniamtionRoot.HideBehavior.OnFinish.AddListener(() =>
            {
                CanvasGroup.DeActive();
                if (Application.isPlaying)
                    ObjectPooling.Instance.PushPopup(this, false);
                ResetEvents();
            });

            foreach (var animation in Animations)
                animation.DoHideAnimation();
        }

        private void ResetEvents()
        {
            ShowBehavior = new NNUIAnimation.AnimationBehavior();
            HideBehavior = new NNUIAnimation.AnimationBehavior();
        }

        public void SetParent(Transform parent) => transform.SetParent(parent);
    }
}
