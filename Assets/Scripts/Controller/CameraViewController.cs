using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace OjbectHunt.GamePlay
{
    public class CameraViewController : MonoBehaviour
    {
        public List<SpriteRenderer> BGSpriteLst = new List<SpriteRenderer>();

        [Header("Zoom Properties")] public bool ZoomEnable;
        public float ZoomMin = 2f;
        public float ZoomMax = 5.4f;

        private float ZoomPan = 0f;

        [Header("Pan Properties")] public bool PanEnable;

        private float PanMinX, PanMinY, PanMaxX, PanMaxY;
        private Camera MainCam;

        private Vector3 DragOrigin;
        private Vector3 TouchStart;

        private void Awake()
        {
            MainCam = Camera.main;
        }

        private void Start()
        {
            CalulateBoundaries();
        }

        private void Update()
        {
            if (PanEnable) HandleCameraPan();
            if (ZoomEnable) HandleCameraZoom();
        }

        private void HandleCameraPan()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DragOrigin = MainCam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                var dragDifference = DragOrigin - MainCam.ScreenToWorldPoint(Input.mousePosition);
                if(MainCam) MainCam.transform.position = ClampCamera(MainCam.transform.position + dragDifference);
            }
        }

        private void HandleCameraZoom()
        {
            #if UNITY_EDITOR
                HandleEditorZoom();
            #elif UNITY_ANDROID
                HandleMobileZoom();
            #endif
        }

        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            var orthographicSize = MainCam.orthographicSize;
            var camWidth = orthographicSize * MainCam.aspect;

            if (MainCam.orthographicSize < ZoomMax + ZoomPan)
            {
                var minX = PanMinX + camWidth;
                var minY = PanMinY + orthographicSize;
                var maxX = PanMaxX - camWidth;
                var maxY = PanMaxY - orthographicSize;

                var clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
                var clampY = Mathf.Clamp(targetPosition.y, minY, maxY);
                return new Vector3(clampX, clampY, targetPosition.z);
            }
            else
            {
                PanMinX = 0f;
                PanMinY = -0.5f;
                PanMaxX = 0;
                PanMaxY = -0.5f;

                var minX = PanMinX; // + camWidth;
                var minY = PanMinY; //+ orthographicSize;
                var maxX = PanMaxX; // - camWidth;
                var maxY = PanMaxY; //- orthographicSize;

                var clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
                var clampY = Mathf.Clamp(targetPosition.y, minY, maxY);
                return new Vector3(clampX, clampY, targetPosition.z);
            }
        }

        private void CalulateBoundaries()
        {
            if (BGSpriteLst != null && BGSpriteLst.Count <= 0) return;
            
            PanMinX = float.MaxValue;
            PanMinY = float.MaxValue;
            PanMaxX = float.MinValue;
            PanMaxY = float.MinValue;

            foreach (var bg in BGSpriteLst)
            {
                PanMinX = Mathf.Min(PanMinX, bg.bounds.min.x);
                PanMaxX = Mathf.Max(PanMaxX, bg.bounds.max.x);
                PanMinY = Mathf.Min(PanMinY, bg.bounds.min.y);
                PanMaxY = Mathf.Max(PanMaxY, bg.bounds.max.y);
                
            }
        }

        private void HandleEditorZoom()
        {
            var zoomValue = Input.GetAxis("Mouse ScrollWheel");
            Zoom(zoomValue);
        }
        
        private void HandleMobileZoom()
        {
            if (Input.touchCount != 2) return;

            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);

            var touch0PrevPos = touch0.position - touch0.deltaPosition;
            var touch1PrevPos = touch1.position - touch1.deltaPosition;

            var prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            var currentMagnitude = (touch0.position - touch1.position).magnitude;

            var touchDifference = currentMagnitude - prevMagnitude;

            Zoom(touchDifference * 0.01f);
        }
        
        private void Zoom(float increment)
        {
            MainCam.orthographicSize = Mathf.Clamp(MainCam.orthographicSize - increment, ZoomMin, ZoomMax);
        }
    }
}