using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
	public class UI_Teach : BasePanel
	{
        private void Start()
        {
            if (ObjectGenerator.Instance().Index != 0)
            {
                Close();
            }
        }
    }
}