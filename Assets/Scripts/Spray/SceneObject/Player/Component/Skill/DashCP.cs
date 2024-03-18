using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spray
{
	public class DashCP : SkillCP
	{
        public UnityAction<float, float> onSpeedChange = delegate { }; 

        [SerializeField] float accelerateTime;
        [SerializeField] float maxSpeedMultiple;
        [SerializeField] float duration;
        [SerializeField] float slowDownTime;

        [SerializeField] float remnentMassMultiple;
        [SerializeField] GameObject foodBulletPrefab;

        WaitForSeconds wait_accelerate;
        WaitForSeconds wait_duration;
        WaitForSeconds wait_slowDown;

        protected override void Awake()
        {
            base.Awake();
            wait_accelerate=new WaitForSeconds(accelerateTime);
            wait_duration=new WaitForSeconds(duration);
            wait_slowDown = new WaitForSeconds(slowDownTime);
        }
        public override void OnMassChange(int mass)
        {
            CanUseSkill = mass >= massLimit;
        }

        public override void UseSkill(Vector3 vector)
        {
            if (CanUseSkill)
                StartCoroutine("Dashing");
            else
            {
                if ((data as PlayerData).mass.Value < massLimit)
                    (UIManager.Instance().GetPanel("Condition") as UI_ExhibitMass).ShowShortage();
                this.vector = vector;
                skillLastInvokeTime = Time.time;
            }
        }
        IEnumerator Dashing()
        {
            CursorManager.Instance().StopInput();
            onSpeedChange.Invoke(maxSpeedMultiple, accelerateTime);
            (data as PlayerData).isUnmatched.Value = true;
            yield return wait_accelerate;
            var playerData = data as PlayerData;
            var bullet = PoolManager.Instance().Release(foodBulletPrefab.name);
            bullet.GetComponent<FoodBullet>().Open(playerData.model.position, vector, (int)(remnentMassMultiple * playerData.mass.Value), -0.1f, 0.1f);
            playerData.mass.Value -= (int)(remnentMassMultiple * playerData.mass.Value);
            yield return wait_duration;
            onSpeedChange.Invoke(0, slowDownTime);
            yield return wait_slowDown;
            (data as PlayerData).isUnmatched.Value = false;
            CursorManager.Instance().ResumeInput();
        }
        public override void Close()
        {

        }

    }
}