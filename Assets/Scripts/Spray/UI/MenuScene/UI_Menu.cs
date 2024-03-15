using BF;
using BF.UI;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spray
{
	public class UI_Menu : MonoBehaviour
	{
		[SerializeField] Button exitBtn;
		[SerializeField] List<GameObject> missionSelectBtnList;
        [SerializeField] MissionSo mission;

        private void Start()
        {
            JsonHandle.Load(mission, "Spray", "Mission");
            //for()
            for (int i = mission.unlockMission + 1; i <= mission.maxMission; i++) 
            {
                missionSelectBtnList[i].SetActive(false);
            }
            for(int i = 0; i < missionSelectBtnList.Count; i++)
            {
                int j = i;
                missionSelectBtnList[i].GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    TransitManager.Instance().TransitScene("Scene0", "Scene1");
                    mission.presentMission = j;
                    JsonHandle.Save(mission, "Spray", "Mission");
                    GameManager.Instance().IsGame = true;
                });
            }
            exitBtn.onClick.AddListener(GameManager.ExitGame);
        }
    }
}