using BF.UI;
using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spray
{
	public class UI_SettingPanel : BasePanel
	{
        [SerializeField] Button openSettingBtn;
        [SerializeField] Button backBtn;
        [SerializeField] Button menuBtn;
        [SerializeField] Button restartBtn;

        protected void Start()
        {
            openSettingBtn.onClick.AddListener(() =>
            {
                Open();
            });
            backBtn.onClick.AddListener(() =>
            {
                Close();
            });
            menuBtn.onClick.AddListener(() =>
            {
                Close();
                GameManager.Instance().IsGame = false;
                TransitManager.Instance().TransitScene("Scene1", "Scene0");
            });
            restartBtn.onClick.AddListener(() =>
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
            Time.timeScale = 0;
            InputManager.Instance().CanInput = false;
        }
        public override void Close()
        {
            base.Close();
            Time.timeScale = 1;
            InputManager.Instance().CanInput = true;
        }
    }
}