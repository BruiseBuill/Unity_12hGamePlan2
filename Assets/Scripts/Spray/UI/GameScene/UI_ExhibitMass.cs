using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spray
{
	public class UI_ExhibitMass : BasePanel
	{
        [SerializeField] Text massText;

        protected void Start()
        {
            var playerControl = FindObjectOfType<PlayerControl>();
            playerControl.onMassChange += ShowMass;
        }
        void ShowMass(int mass)
        {
            massText.text = string.Format("Mass: {0}", mass);
        }
    }
}