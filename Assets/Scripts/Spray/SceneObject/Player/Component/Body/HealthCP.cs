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
            if (mass < minMass)
            {
                data.isAlive.Value = false;
            }
        }
        public void BeHit(int damage)
        {
            if(!(data as PlayerData).isUnmatched.Value)
            {
                (data as PlayerData).mass.Value -= damage;
            }
        }
        public override void Close()
        {

        }
        
    }
}