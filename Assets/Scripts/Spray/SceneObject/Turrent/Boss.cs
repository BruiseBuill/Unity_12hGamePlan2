using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
	public class Boss : Turrent
	{
        public override void Close()
        {
            base.Close();
           
            GameControl.Instance().Win();
        }
    }
}