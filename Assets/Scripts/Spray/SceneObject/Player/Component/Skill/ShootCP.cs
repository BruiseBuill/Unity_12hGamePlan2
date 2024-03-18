using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    public class ShootCP : SkillCP
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] float lineLength;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] int consumePerBullet;

        protected override void Awake()
        {
            base.Awake();
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }
        private void OnEnable()
        {
            lineRenderer.enabled = false;
        }
        public void Aim(Vector3 orient)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, (data as PlayerData).model.position);
            lineRenderer.SetPosition(1, (data as PlayerData).model.position + orient.normalized * lineLength);
        }
        public void AimCancel()
        {
            lineRenderer.enabled = false;
        }
        public override void Close()
        {
            
        }

        public override void OnMassChange(int mass)
        {
            CanUseSkill = mass >= massLimit;
        }

        public override void UseSkill(Vector3 vector)
        {
            lineRenderer.enabled = false;
            if (CanUseSkill)
            {
                var bullet = PoolManager.Instance().Release(bulletPrefab.name);
                bullet.GetComponent<Bullet>().Open((data as PlayerData).model.position, vector.normalized * Mathf.Sqrt(vector.magnitude), gameObject.layer);
                (data as PlayerData).mass.Value -= consumePerBullet;
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