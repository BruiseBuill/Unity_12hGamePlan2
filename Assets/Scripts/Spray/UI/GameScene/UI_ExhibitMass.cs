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
        [SerializeField] Text shortageText;
        [SerializeField] float showTime;
        WaitForSeconds wait_Show;

        protected void Start()
        {
            var playerControl = FindObjectOfType<PlayerControl>();
            playerControl.onMassChange += ShowMass;
            wait_Show = new WaitForSeconds(showTime);
        }
        void ShowMass(int mass)
        {
            massText.text = string.Format("Mass: {0}", mass);
        }
        public void ShowShortage()
        {
            
            StartCoroutine("Showing");
        }
        IEnumerator Showing()
        {
            shortageText.enabled = true;
            yield return wait_Show;
            shortageText.enabled = false;
        }
    }
}