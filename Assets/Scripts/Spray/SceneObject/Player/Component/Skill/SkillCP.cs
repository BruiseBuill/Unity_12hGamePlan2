using BF;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Spray
{
    public abstract class SkillCP : PlayerComponent
    {
        protected float skillLastInvokeTime=-1f;
        protected const float cacheTime = 0.1f;

        [SerializeField] protected int massLimit;
        protected Vector3 vector;

        [SerializeField] bool canUseSkill;
        public bool CanUseSkill
        {
            get => canUseSkill;
            set
            {
                if (canUseSkill == value)
                    return;
                if (value)
                {
                    canUseSkill = true;
                    if (Time.time - skillLastInvokeTime < cacheTime)
                    {
                        UseSkill(vector);
                    }
                    else
                        return;
                }
                else
                    canUseSkill = false;
            }
        }

        public abstract void UseSkill(Vector3 vector);
    }
}