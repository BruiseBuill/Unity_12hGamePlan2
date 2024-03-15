using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace BF
{
	public class BaseShareData : MonoBehaviour
	{
        public DataWithEvent<bool> isAlive;

		public void Awake()
		{
			isAlive = new DataWithEvent<bool>();
		}
        public class DataWithEvent<T> where T:struct
		{
			public UnityAction<T> onValueChange = delegate { };
			T data;
			public T Value 
            {
				get => data;
				set
				{
                    data = value;
					onValueChange.Invoke(data);
				}
			}
			public DataWithEvent()
			{
				onValueChange = delegate { };
			}
        }

	}
}