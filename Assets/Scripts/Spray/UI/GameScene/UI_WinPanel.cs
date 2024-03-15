using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spray
{
	public class UI_WinPanel : BasePanel
	{
        [SerializeField] Button menuBtn;
        [SerializeField] Button nextMissionBtn;

        protected void Start()
        {
            menuBtn.onClick.AddListener(() =>
            {
                Close();
                GameManager.Instance().IsGame = false;
                TransitManager.Instance().TransitScene("Scene1", "Scene0");
            });
            nextMissionBtn.onClick.AddListener(() =>
            {
                Close();
                GameManager.Instance().IsGame = false;
                TransitManager.Instance().TransitScene("Scene1", "Scene1");
            });
            Close();
        }
        public override void Open()
        {
            base.Open();
            InputManager.Instance().CanInput = false;
        }
        public override void Close()
        {
            base.Close();
            InputManager.Instance().CanInput = true;
        }

    }
}