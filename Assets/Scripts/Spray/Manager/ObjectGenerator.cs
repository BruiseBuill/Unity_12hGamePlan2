using BF;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Spray
{
	public class ObjectGenerator : Single<ObjectGenerator>
	{
        [SerializeField] MissionSo mission;
        [SerializeField] List<TurrentSO> sceneSoList = new List<TurrentSO>();
        [SerializeField] int index;
        public int Index => index;

        void Awake()
        {
            JsonHandle.Load(mission, "Spray", "Mission");
            index = mission.presentMission;
        }
        private void Start()
        {
            Create();
        }
        public void Create()
        {
            var so = sceneSoList[index];

            for (int i = 0; i < so.enemyPosList.Count; i++)
            {
                var tower = PoolManager.Instance().Release(so.enemyTypes[i].ToString());
                tower.GetComponent<Turrent>().Open(so.enemyPosList[i]);
            }
            for(int i = 0; i < so.wallPosList.Count; i++)
            {
                var wall = PoolManager.Instance().Release("Wall");
                wall.GetComponent<Wall>().Open(so.wallPosList[i], so.wallOrientList[i], so.wallScaleList[i]);
            }
            for(int i = 0; i < so.foodPos.Count; i++)
            {
                var food = PoolManager.Instance().Release("Food");
                food.GetComponent<Food>().Open(so.foodPos[i], so.foodMass[i]);
            }
        }
        public void Win()
        {
            if (index + 1 > mission.unlockMission)
            {
                mission.unlockMission = index + 1;
                JsonHandle.Save(mission, "Spray", "Mission");
            }
                
        }
    }
}