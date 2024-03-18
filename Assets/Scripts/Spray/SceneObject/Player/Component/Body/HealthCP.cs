using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    public class HealthCP : PlayerComponent
    {
        [SerializeField] int minMass;

        public void Open()
        {

        }
        public override void OnMassChange(int mass)
        {
            if (mass <= minMass)
            {
                data.isAlive.Value = false;
            }
        }
        public void BeHit(int damage)
        {
            var playerData = (data as PlayerData);
            if (!playerData.isUnmatched.Value)
            {
                playerData.mass.Value = Mathf.Max(playerData.mass.Value - damage, 0);
            }
        }
        public override void Close()
        {

        }
        
    }
}