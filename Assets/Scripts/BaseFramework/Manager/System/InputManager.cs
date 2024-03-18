using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BF
{
    [Serializable]
    public class KeyEvent
    {
        public UnityAction onKey = delegate { };
        [SerializeField] KeyCode keyCode;
        [SerializeField] KeyCondition condition;
        public string Name => keyCode.ToString() + condition.ToString();
        public KeyEvent(KeyCode keyCode, KeyCondition condition)
        {
            onKey = delegate { };
            this.keyCode = keyCode;
            this.condition = condition;
        }

        public void Update()
        {
            switch (condition)
            {
                case KeyCondition.Down:
                    if (Input.GetKeyDown(keyCode))
                        onKey.Invoke();
                    break;
                case KeyCondition.Up:
                    if (Input.GetKeyUp(keyCode))
                        onKey.Invoke();
                    break;
            }
        }
    }

    public class InputManager : Single<InputManager>
    {
        [SerializeField] bool canMouseInput = true;
        public bool CanInput
        {
            get => canMouseInput;
            set
            {
                canMouseInput = value;
                lastPressPoint = Input.mousePosition;
            }
        }

        // Position in these delegates is physical screen position. When screen become bigger, the same start and the end of ONDRAG will represent shorter physical distance   
        public static UnityAction<Vector3, Vector3> onDrag = delegate { };
        public static UnityAction<Vector3, Vector3> onDragEnd = delegate { };
        public static UnityAction<Vector3> onDragCancel = delegate { };
        public static UnityAction<Vector3> onClick = delegate { };
        public static UnityAction<Vector3> onDoubleClick = delegate { };
        public static UnityAction<Vector3, float> onLongPress = delegate { };
        public static UnityAction<Vector3, float> onLongPressEnd = delegate { };
        public static UnityAction onClickRight = delegate { };

        [SerializeField] Vector3 minTouchPort = new Vector3(0, 0, 0);
        [SerializeField] Vector3 maxTouchPort = new Vector3(1920, 1080, 0);
        [SerializeField] Vector3 referenceTouchResolution = new Vector3(1920, 1080, 0);
        public Vector3 ReferenceResolution => referenceTouchResolution;

        Vector3 minScreenPort;
        Vector3 maxScreenPort;
        //
        [SerializeField] float dragOffset = 20f;
        [SerializeField] float minLongPressTime = 0.25f;
        [SerializeField] float doubleClickTime = 0.1f;
        float sqrDragOffset;
        ///WaitForSeconds wait_DoubleClickTime;
        float lastPressTime;
        Vector3 lastPressPoint;
        //
        bool isPressValid;
        //When mouse down, you can switch LONGPRESS to DRAG. But you can not switch DRAG to LONGPRESS
        [SerializeField] bool isDrag;
        bool isLongPress;
        //
        bool isSingleClickLast;
        //
        Dictionary<string, KeyEvent> keyEventDic = new Dictionary<string, KeyEvent>();

        public KeyEvent GetKeyEvent(KeyCode keyCode, KeyCondition keyCondition)
        {
            if(!keyEventDic.ContainsKey(keyCode.ToString() + keyCondition.ToString()))
            {
                keyEventDic.Add(keyCode.ToString() + keyCondition.ToString(), new KeyEvent(keyCode, keyCondition));
            }
            return keyEventDic[keyCode.ToString() + keyCondition.ToString()];
        }

        private void Awake()
        {
            Vector3 scale = new Vector3(Screen.width / referenceTouchResolution.x, Screen.height / referenceTouchResolution.y, 0);

            sqrDragOffset = dragOffset * dragOffset * scale.x * scale.y;
            minScreenPort = new Vector3(minTouchPort.x * scale.x, minTouchPort.y * scale.y, 0);
            maxScreenPort = new Vector3(maxTouchPort.x * scale.x, maxTouchPort.y * scale.y, 0);

#if UNITY_ANDROID

#endif
#if UNITY_STANDALONE_WIN

#endif
        }
        private void Update()
        {
            if (!canMouseInput)
            {
                return;
            }
            MouseCheck();
#if UNITY_STANDALONE_WIN
            KeyboardCheck();
#endif
        }
        void MouseCheck()
        {
            if (Input.GetMouseButtonUp(1))
            {
                onClickRight.Invoke();
            }

            if (!TouchPortCheck(Input.mousePosition))
                return;

            if (Input.GetMouseButtonDown(0))
            {
                //DoubleClick
                isPressValid = true;
                isDrag = false;
                if (!isSingleClickLast || (isSingleClickLast && Time.time - lastPressTime > doubleClickTime) || (lastPressPoint - Input.mousePosition).sqrMagnitude > sqrDragOffset)
                {
                    isSingleClickLast = false;
                    lastPressTime = Time.time;
                    lastPressPoint = Input.mousePosition;
                }
            }
            else if (Input.GetMouseButtonUp(0) && isPressValid)
            {
                isPressValid = false;
                
                if ((lastPressPoint - Input.mousePosition).sqrMagnitude < sqrDragOffset)
                {
                    if (isDrag)
                    {
                        onDragCancel.Invoke(Input.mousePosition);
                    }
                    else if (Time.time - lastPressTime < doubleClickTime)
                    {
                        if (isSingleClickLast)
                        {
                            isSingleClickLast = false;
                            onDoubleClick.Invoke(Input.mousePosition);
                        }
                        else
                        {
                            isSingleClickLast = true;
                            onClick.Invoke(Input.mousePosition);
                        }
                    }
                    else if (Time.time - lastPressTime < minLongPressTime)
                    {
                        isSingleClickLast = true;
                        onClick.Invoke(Input.mousePosition);
                    }
                    else //if (Time.time - lastPressTime > minLongPressTime)
                    {
                        onLongPressEnd.Invoke(Input.mousePosition, Time.time - lastPressTime);
                        isSingleClickLast = false;  
                    }
                }
                else
                {
                    onDragEnd.Invoke(lastPressPoint, Input.mousePosition);
                    isSingleClickLast = false;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if ((lastPressPoint - Input.mousePosition).sqrMagnitude > sqrDragOffset)
                {
                    onDrag.Invoke(lastPressPoint, Input.mousePosition);
                    isDrag = true;
                    if (isLongPress)
                    {
                        isLongPress = false;
                    }
                }
                else if (Time.time - lastPressTime > minLongPressTime && !isDrag)
                {
                    isLongPress = true;
                    onLongPress.Invoke(Input.mousePosition, Time.time - lastPressTime);
                }
            }
        }
        void KeyboardCheck()
        {
            foreach(var pair in keyEventDic)
            {
                pair.Value.Update();
            }
        }
        bool TouchPortCheck(Vector3 mousePos)
        {
            return mousePos.x > minScreenPort.x && mousePos.x < maxScreenPort.x && mousePos.y > minScreenPort.y && mousePos.y < maxScreenPort.y;
        }
    }
}