using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Common
{
    public class InputHandleObj : MonoBehaviour
    {
        #region Custom objects
        public enum InputType
        {
            Down, Moved, Up
        }

        [Serializable]
        public class TouchData
        {
            public InputType Type;
            public Vector3 InitPos;
            public Vector3 CurrentPos;
            public Camera Camera;
        }
        #endregion

        public Action<TouchData> TouchChanged;

        [SerializeField]
        private Camera currentCamera;

        [SerializeField]
        private TouchData currentTouchData;


        private void Awake()
        {
            currentCamera = Camera.main;
            currentTouchData.Camera = currentCamera;
            TouchChanged = null;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentTouchData.Type = InputType.Down;
                currentTouchData.InitPos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
                currentTouchData.CurrentPos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
                currentTouchData.InitPos.z = 0;
                currentTouchData.CurrentPos.z = 0;

                TouchChanged?.Invoke(currentTouchData);
            }

            if (Input.GetMouseButton(0))
            {
                currentTouchData.Type = InputType.Moved;
                currentTouchData.CurrentPos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
                currentTouchData.CurrentPos.z = 0;

                if (currentTouchData.CurrentPos != currentTouchData.InitPos)
                    TouchChanged?.Invoke(currentTouchData);
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentTouchData.Type = InputType.Up;
                currentTouchData.CurrentPos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
                currentTouchData.CurrentPos.z = 0;
                TouchChanged?.Invoke(currentTouchData);
            }
        }
    }
}
