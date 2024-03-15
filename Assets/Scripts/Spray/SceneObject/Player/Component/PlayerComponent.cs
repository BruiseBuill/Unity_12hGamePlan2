using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
	public abstract class PlayerComponent :BaseComponent
	{
		protected virtual void Awake()
		{
			data = GetComponentInChildren<BaseShareData>();
		}
		public abstract void OnMassChange(int mass);
	}
}