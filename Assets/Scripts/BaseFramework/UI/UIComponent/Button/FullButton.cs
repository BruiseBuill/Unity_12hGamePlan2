using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BF.UI
{
	public class FullButton : Button,IPointerDownHandler,IPointerUpHandler
	{
        public UnityAction onPointerDown = delegate { };
        public UnityAction onPointerUp = delegate { };

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            onPointerDown.Invoke();
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            onPointerUp.Invoke();
        }
    }
}