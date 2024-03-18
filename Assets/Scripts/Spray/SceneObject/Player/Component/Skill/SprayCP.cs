using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    public class SprayCP : SkillCP
    {
        [SerializeField] GameObject foodBulletPrefab;
        [SerializeField] float sprayFoodMultiple;
        [SerializeField] float sprayFoodSpeed;
        [SerializeField] float sprayBulletTime;

        public override void Close()
        {
            
        }

        public override void OnMassChange(int mass)
        {
            CanUseSkill = mass >= massLimit;
        }

        public override void UseSkill(Vector3 pos)
        {
            if (CanUseSkill)
            {
                var playerData = data as PlayerData;
                var bullet = PoolManager.Instance().Release(foodBulletPrefab.name);
                bullet.GetComponent<FoodBullet>().Open(playerData.model.position, (pos - playerData.model.position).normalized, (int)(sprayFoodMultiple * playerData.mass.Value), sprayFoodSpeed, sprayBulletTime);
                playerData.mass.Value -= (int)(sprayFoodMultiple * playerData.mass.Value);
            }
            else
            {
                if ((data as PlayerData).mass.Value < massLimit)
                    (UIManager.Instance().GetPanel("Condition") as UI_ExhibitMass).ShowShortage();
                this.vector = vector;
                skillLastInvokeTime = Time.time;
            }
        }
    }
}