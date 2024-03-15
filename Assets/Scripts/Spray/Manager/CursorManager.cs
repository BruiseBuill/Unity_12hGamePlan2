using BF;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spray
{
	public class CursorManager : Single<CursorManager>
	{
		const int skillCount = 3;
		public UnityAction<Vector3> onDash = delegate { };
		public UnityAction<Vector3> onShoot = delegate { };
		//public UnityAction onResetShoot = delegate { };
		public UnityAction<Vector3> onSpray = delegate { };
		//public UnityAction onResetSpray = delegate { };
		public UnityAction<Vector3> onMove = delegate { };
        public UnityAction<Vector3> onAim = delegate { };
        public UnityAction onAimCancel = delegate { };
		new Camera camera;

        private void Awake()
        {
			camera = Camera.main;
			
        }
        private void Start()
        {
            InputManager.onClick += (screenPos) =>
            {
				var pos = camera.Screen2WorldProject(screenPos);
				onMove.Invoke(pos);
            };
			InputManager.onDoubleClick += (screenPos) =>
			{
                var pos = camera.Screen2WorldProject(screenPos);
                onDash.Invoke(pos);
            };
            InputManager.onDragCancel += (Vector) =>
            {
                onAimCancel.Invoke();
            };
			InputManager.onDrag += (start, end) =>
			{
                var startPos = camera.Screen2WorldProject(start);
                var endPos = camera.Screen2WorldProject(end);

                onAim.Invoke(endPos - startPos);
            };
            InputManager.onDragEnd += (start, end) =>
            {
                var startPos = camera.Screen2WorldProject(start);
                var endPos = camera.Screen2WorldProject(end);

                onShoot.Invoke(endPos - startPos);
            };

            InputManager.onLongPress+= (screenPos,time) =>
            {
                
            };
			InputManager.onLongPressEnd += (screenPos, time) =>
            {
                var pos = camera.Screen2WorldProject(screenPos);
                onSpray.Invoke(pos);
            };
        }
        private void OnDestroy()
        {
            InputManager.onClick = delegate { };
            InputManager.onDoubleClick = delegate { };
            InputManager.onDrag = delegate { };
            InputManager.onDragEnd = delegate { };
            InputManager.onDragCancel = delegate { };
            InputManager.onLongPress = delegate { };
            InputManager.onLongPressEnd = delegate { };
        }
        public void StopInput()
		{
			InputManager.Instance().CanInput = false;	
		}
		public void ResumeInput()
		{
			InputManager.Instance().CanInput = true;
		}

	}
}