using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
	public abstract class BaseControl : MonoBehaviour
	{
		protected BaseShareData data;
		public bool IsAlive
		{
			protected set => data.isAlive.Value=value;
            get => data.isAlive.Value;
		}

        public abstract void Close();
	}
}