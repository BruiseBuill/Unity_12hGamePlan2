using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
	public class Wall : BaseControl
	{
		public Vector3 Pos => transform.position;
		public Vector3 Scale => transform.localScale;
		public Vector3 Orient => transform.up;

        public override void Close()
        {
			PoolManager.Instance().Recycle(gameObject);
        }

        public void Open(Vector3 pos,Vector3 orient,Vector3 scale)
		{
			transform.position = pos;
			transform.up = orient;
			transform.localScale = scale;
			gameObject.SetActive(true);
		}
	}
}