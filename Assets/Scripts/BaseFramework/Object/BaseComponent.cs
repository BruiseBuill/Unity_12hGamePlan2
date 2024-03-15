using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
	public abstract class BaseComponent : MonoBehaviour
	{
        protected BaseShareData data;
        public abstract void Close();
    }
}