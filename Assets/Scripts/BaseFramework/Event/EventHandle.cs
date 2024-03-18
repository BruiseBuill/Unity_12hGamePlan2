using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace BF
{
	[CreateAssetMenu(fileName ="EventHandle",menuName = "BF/EventHandle")]
	public class EventHandle : ScriptableObject
	{
		Action onEvent = delegate { };

		public void Invoke()
		{
			onEvent.Invoke();
		}
		public void AddListener(Action action)
		{
			onEvent += action;
        }
		public void RemoveListener(Action action)
		{
            onEvent -= action;
        }
    }
}