using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spray
{
    public class PlayerControl : BaseControl
    {
        public UnityAction<int> onGetFood = delegate { };
        public UnityAction<int> onBeHit = delegate { };
        public UnityAction<int> onMassChange = delegate { };

        public Transform Model => (data as PlayerData).model;
        public bool IsUnmatch => (data as PlayerData).isUnmatched.Value;
        public int Mass => (data as PlayerData).mass.Value;

        HealthCP health;
        MoveCP move;
        BodyCP body;
        DashCP dash;
        SprayCP spray;
        ShootCP shoot;
        List<PlayerComponent> components = new List<PlayerComponent>();

        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            data = GetComponentInChildren<BaseShareData>();
            health = GetComponentInChildren<HealthCP>();
            move = GetComponentInChildren<MoveCP>();
            body = GetComponentInChildren<BodyCP>();
            dash = GetComponentInChildren<DashCP>();
            spray = GetComponentInChildren<SprayCP>();
            shoot = GetComponentInChildren<ShootCP>();

            components.Add(health);
            components.Add(move);
            components.Add(body);
            components.Add(dash);
            components.Add(spray);
            components.Add(shoot);
        }
        private void Start()
        {
            Open(Vector3.zero);
        }
        public void Open(Vector3 pos)
        {
            PlayerData playerData = data as PlayerData;
            for (int i = 0; i < components.Count; i++)
            {
                playerData.mass.onValueChange += components[i].OnMassChange;
            }
            playerData.mass.onValueChange += (mass) => onMassChange.Invoke(mass);

            playerData.isAlive.onValueChange += (isAlive) =>
            {
                if (!isAlive)
                {
                    Close();
                }
            };
            playerData.isUnmatched.onValueChange += body.OnBeUnmatched;
            dash.onSpeedChange += move.SpeedChange;

            CursorManager.Instance().onMove += move.Move;
            CursorManager.Instance().onDash += dash.UseSkill;
            CursorManager.Instance().onShoot += shoot.UseSkill;
            CursorManager.Instance().onSpray += spray.UseSkill;
            CursorManager.Instance().onAim += shoot.Aim;
            CursorManager.Instance().onAimCancel += shoot.AimCancel;

            onGetFood += (mass) => playerData.mass.Value += mass;
            onBeHit += health.BeHit;

            playerData.model.position = pos;
            gameObject.SetActive(true);
            playerData.mass.Value = playerData.mass.Value;
        }
        public override void Close()
        {
            onGetFood = delegate { };
            onBeHit = delegate { };
            onMassChange = delegate { };

            PlayerData playerData = data as PlayerData;
            playerData.mass.onValueChange = delegate { };
            playerData.isAlive.onValueChange = delegate { };
            playerData.isUnmatched.onValueChange = delegate { };
            dash.onSpeedChange = delegate { };


            foreach (PlayerComponent playerComponent in components)
            {
                playerComponent.Close();
            }

            CursorManager.Instance().onMove -= move.Move;
            CursorManager.Instance().onDash -= dash.UseSkill;
            CursorManager.Instance().onShoot -= shoot.UseSkill;
            CursorManager.Instance().onSpray -= spray.UseSkill;
            CursorManager.Instance().onAim -= shoot.Aim;
            CursorManager.Instance().onAimCancel -= shoot.AimCancel;

            PoolManager.Instance().Recycle(gameObject);

            GameControl.Instance().Lose();
        }
    }
}