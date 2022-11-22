using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
 
namespace editor {

    [EditorTool("Scene Navigator")]
    public class SceneNavigator : EditorTool
    {
        private float _lastCameraSize = 1;
        private Vector2? _lastMousePosition = null;
        public override void OnToolGUI(EditorWindow window)
        {
            var currentEvent = Event.current;
            _lastMousePosition = _lastMousePosition ?? currentEvent.mousePosition;
            if (!IsSwipeEvent(currentEvent))
            {
                base.OnToolGUI(window);
                _lastCameraSize = SceneView.lastActiveSceneView.size;
                _lastMousePosition = currentEvent.mousePosition;
            }
            else
            {
                SceneView.lastActiveSceneView.size = _lastCameraSize;
                HandleMouse();
            }
        }
 
        private bool IsSwipeEvent(Event currentEvent)
        {
            if (currentEvent.command) return false;
            if (currentEvent.control) return false;
            if (currentEvent.isMouse) return false;
           
            return currentEvent.mousePosition == _lastMousePosition;
        }
 
        private void HandleMouse()
        {
            var currentEvent = Event.current;
            var currentEventDelta = currentEvent.delta;
            if(Math.Abs(currentEventDelta.x) < 1f && Math.Abs(currentEventDelta.y) < 1f) return;
 
           
            if (currentEvent.shift)
            {
                ApplyPivotLocation(GetNewPivotPointByEventDelta(currentEventDelta));
            }
            else
            {
                var newRotation = SceneView.lastActiveSceneView.rotation.eulerAngles;
                newRotation.x += currentEventDelta.y;
                newRotation.y += currentEventDelta.x;
 
                ApplyRotation(Quaternion.Euler(newRotation));
            }
        }
 
        private static Vector3 GetNewPivotPointByEventDelta(Vector2 currentEventDelta)
        {
            var sceneCameraTransform = SceneView.lastActiveSceneView.camera.transform;
            var cameraRight = sceneCameraTransform.TransformDirection(Vector3.right) / 10f;
            var cameraDown = sceneCameraTransform.TransformDirection(Vector3.down) / 10f;
            var newPivotPoint = SceneView.lastActiveSceneView.pivot;
            var newCameraOffset = default(Vector3);
            newCameraOffset += cameraRight * currentEventDelta.x;
            newCameraOffset += cameraDown * currentEventDelta.y;
            newPivotPoint += newCameraOffset;
            return newPivotPoint;
        }
 
        private static void ApplyPivotLocation(Vector3 newPivotPoint)
        {
            SceneView.lastActiveSceneView.pivot = newPivotPoint;
        }
 
        private static Vector2? GetRotationOffsetFromKeyboardInput(Event currentEvent)
        {
            if (!currentEvent.isMouse) return null;
           
            var rotationOffset = new Vector2(0, 0); //currentEvent.delta;
            var isLeftPressed = currentEvent.keyCode == KeyCode.LeftArrow;
            var isRightPressed = currentEvent.keyCode == KeyCode.RightArrow;
            rotationOffset.x += isLeftPressed ? 2f : 0;
            rotationOffset.x -= isRightPressed ? 2f : 0;
            var isUpPressed = currentEvent.keyCode == KeyCode.UpArrow;
            var isDownPressed = currentEvent.keyCode == KeyCode.DownArrow;
            rotationOffset.y += isUpPressed ? 2f : 0;
            rotationOffset.y -= isDownPressed ? 2f : 0;
            return rotationOffset;
        }
        private void OnDrawGizmos()
        {
            // Your gizmo drawing thing goes here if required...
#if UNITY_EDITOR
            // Ensure continuous Update calls.
            if (!Application.isPlaying)
            {
                UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
                UnityEditor.SceneView.RepaintAll();
            }
#endif
        }
 
        /// <summary>
        /// The rotation to restore when going back to perspective view. If we don't have anything,
        /// default to the 'Front' view. This avoids the problem of an invalid rotation locking out
        /// any further mouse rotation
        /// </summary>
        static Quaternion sPerspectiveRotation = Quaternion.Euler(0, 0, 0);
 
        /// <summary>
        /// Whether the camera should tween between views or snap directly to them
        /// </summary>
        static bool sShouldTween = false;
 
        /// <summary>
        /// When switching from a perspective view to an orthographic view, record the rotation so
        /// we can restore it later
        /// </summary>
        static private void StorePerspective()
        {
            if (SceneView.lastActiveSceneView.orthographic == false)
            {
                sPerspectiveRotation = SceneView.lastActiveSceneView.rotation;
            }
        }
 
        static private void ApplyRotation(Quaternion newRotation)
        {
            var shouldApplyPerspectiveRotation = !(SceneView.lastActiveSceneView.orthographic);
            ApplyOrthoRotation(newRotation);
            if(shouldApplyPerspectiveRotation)
            {
                PerspCamera();
            }      
            var appliedRotation = SceneView.lastActiveSceneView.rotation.eulerAngles;
            //Debug.Log($"rotationx has been reset to: {appliedRotation.x}");
        }
 
        /// <summary>
        /// Apply an orthographic view to the scene views camera. This stores the previously active
        /// perspective rotation if required
        /// </summary>
        /// <param name="newRotation">The new rotation for the orthographic camera</param>
        static private void ApplyOrthoRotation(Quaternion newRotation)
        {
            StorePerspective();
 
            SceneView.lastActiveSceneView.orthographic = true;
 
            if (sShouldTween)
            {
                SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, newRotation);
            }
            else
            {
                SceneView.lastActiveSceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot, newRotation);
            }
 
            SceneView.lastActiveSceneView.Repaint();
        }
 
        [MenuItem("Camera/MoveToUp _8")]
        static void MoveToUpCamera()
        {
            var newRotation = SceneView.lastActiveSceneView.rotation.eulerAngles;
            newRotation.x += 10;
            ApplyRotation(Quaternion.Euler(newRotation));
        }
 
        [MenuItem("Camera/Top _7")]
        static void TopCamera()
        {
            ApplyRotation(Quaternion.Euler(90, 0, 0));
        }
 
        [MenuItem("Camera/MoveToDown _2")]
        static void MoveToDownCamera()
        {
            var newRotation = SceneView.lastActiveSceneView.rotation.eulerAngles;
            newRotation.x -= 10;
            ApplyRotation(Quaternion.Euler(newRotation));
        }
 
        [MenuItem("Camera/Bottom #7")]
        static void BottomCamera()
        {
            ApplyRotation(Quaternion.Euler(-90, 0, 0));
        }
 
 
        [MenuItem("Camera/Left #3")]
        static void LeftCamera()
        {
            ApplyRotation(Quaternion.Euler(0, 90, 0));
        }
 
 
        [MenuItem("Camera/Right _3")]
        static void RightCamera()
        {
            ApplyRotation(Quaternion.Euler(0, -90, 0));
        }
 
 
        [MenuItem("Camera/Front _1")]
        static void FrontCamera()
        {
            ApplyRotation(Quaternion.Euler(0, 180, 0));
        }
 
        [MenuItem("Camera/MoveToRight _6")]
        static void MoveToRightCamera()
        {
            var newRotation = SceneView.lastActiveSceneView.rotation.eulerAngles;
            newRotation.y -= 10;
            ApplyRotation(Quaternion.Euler(newRotation));
        }
 
        [MenuItem("Camera/MoveToLeft _4")]
        static void MoveToLeftCamera()
        {
            var newRotation = SceneView.lastActiveSceneView.rotation.eulerAngles;
            newRotation.y += 10;
            ApplyRotation(Quaternion.Euler(newRotation));
        }
 
        [MenuItem("Camera/Back #1")]
        static void BackCamera()
        {
            ApplyRotation(Quaternion.Euler(0, 0, 0));
        }
 
 
        [MenuItem("Camera/Persp Camera _5")]
        static void PerspCamera()
        {
            var isOrthographic = SceneView.lastActiveSceneView.orthographic;
            SceneView.lastActiveSceneView.orthographic = !isOrthographic;
            SceneView.lastActiveSceneView.Repaint();
        }
 
        private static Tool _previousToolMode;
        [MenuItem("Edit/Toggle custom tool _G", false, 5)]
        static void ToggleCustomTool()
        {
            Debug.Log($"Current tool mode: {Tools.current}");
            if (Tools.current != Tool.Custom)
            {
                _previousToolMode = Tools.current;
                Tools.current = Tool.Custom;
            }
            else
            {
                Tools.current = _previousToolMode;
            }
        }
    }
}
