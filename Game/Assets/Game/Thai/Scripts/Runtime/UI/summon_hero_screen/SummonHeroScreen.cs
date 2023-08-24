using Cysharp.Threading.Tasks;
using Runtime.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.UI
{
    public partial class SummonHeroScreen : MonoBehaviour
    {
        #region Members

        [SerializeField] private InputHandleObj _inputHandle;
        private Vector3 lastMousePos;
        #endregion Members

        #region Events
        public static event Action<bool> ActionOnSwipeDownUISummon;

        #endregion Events

        private void Awake()
        {
            Initialize().Forget();
        }
        private void OnEnable()
        {
            _inputHandle.TouchChanged += OnTouchChanged;
        }
        private void OnDisable()
        {
            OnCleanUp();
        }

        #region Class Methods
        public async UniTask Initialize()
        {
            await UniTask.Delay(100); // Load addressable sprite to image
            SetupData();
            InitButtons();
            InitLayout();
        }

        private void OnTouchChanged(InputHandleObj.TouchData data)
        {
            if (data.Type == InputHandleObj.InputType.Down)
            {
                lastMousePos = data.CurrentPos;
            }

            if (data.Type == InputHandleObj.InputType.Moved)
            {

            }

            if (data.Type == InputHandleObj.InputType.Up)
            {
                if (data.CurrentPos.y - lastMousePos.y == 0 || isOnSwiping) return;
                var tmp = data.CurrentPos.y - lastMousePos.y < 0;
                ActionOnSwipeDownUISummon?.Invoke(tmp);
                SwipeDownUISummon(tmp);
            }
        }

        partial void InitButtons();
        partial void SetupData();
        partial void InitLayout();

        private void OnCleanUp()
        {
            _inputHandle.TouchChanged -= OnTouchChanged;
        }
        
        private void SwipeDownUISummon(bool isDown)
        {
            Debug.Log("swipe down: " + isDown);
            _fightWhenSwipeDownButton.gameObject.SetActive(isDown);
            Swipe(isDown);
        }


        #endregion Class Methods
    }
}

