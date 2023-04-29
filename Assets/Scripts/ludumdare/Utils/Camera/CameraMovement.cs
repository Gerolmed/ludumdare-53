﻿using Cinemachine;
using UnityEngine;

namespace LudumDare.Utils.Camera
{
    public class CameraMovement: MonoBehaviour
    {

        [SerializeField]
        private CameraPositionLock cameraPositionLock;
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private Vector3 _lastMousePos;

        private const float Speed = 7;
        private const float MouseSpeed = 10;
        
        private void Update()
        {
            MouseScroll();
            if(!MouseMovement())
            {
                KeyboardMovement();
            }
        }


        public void KeyboardMovement()
        {
            var position = transform.position;
            var dir = new Vector3();

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                dir = new Vector3(0, 1);
            } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                dir = new Vector3(0, -1);
            }
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                dir += new Vector3(1, 0);
            } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                dir += new Vector3(-1, 0);
            }

            transform.position = LimitPosition(position + dir.normalized * Time.deltaTime * Speed);
        }

        public void MouseScroll()
        {
            var delta = Input.mouseScrollDelta;
            var size = virtualCamera.m_Lens.OrthographicSize;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Min(Mathf.Max(3, size - delta.y), 10);

        }
        public bool MouseMovement()
        {
            if (!Input.GetMouseButton(0)) return false;

            if (Input.GetMouseButtonDown(0))
            {
                _lastMousePos = Input.mousePosition;
            }
            
            
            var current = Input.mousePosition;
            var delta = Input.mousePosition - _lastMousePos;
            _lastMousePos = current;
            
            transform.position = LimitPosition(transform.position + -delta * Time.deltaTime * MouseSpeed * CameraZoomSpeed());

            return true;
        }


        private float CameraZoomSpeed()
        {
            return virtualCamera.m_Lens.OrthographicSize / 5;
        }


        public Vector3 LimitPosition(Vector3 position)
        {

            position.x = Mathf.Min(cameraPositionLock.upperX, position.x);
            position.x = Mathf.Max(cameraPositionLock.lowerX, position.x);

            position.y = Mathf.Min(cameraPositionLock.upperY, position.y);
            position.y = Mathf.Max(cameraPositionLock.lowerY, position.y);
            
            return position;
        }
    }
}