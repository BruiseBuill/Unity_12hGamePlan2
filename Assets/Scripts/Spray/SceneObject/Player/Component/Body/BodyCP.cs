using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    public class BodyCP :PlayerComponent
    {
        [Tooltip("size=sqrt(mass+multipleA)*multipleB+multipleC")]
        [SerializeField] float multipleA;
        [SerializeField] float multipleB;
        [SerializeField] float multipleC;
        [SerializeField] int mass;

        [SerializeField] Color32 unmathchedColor;

        public void Open()
        {
            (data as PlayerData).model.GetComponent<SpriteRenderer>().color = Color.white;
        }
        public override void Close()
        {
            
        }
        public override void OnMassChange(int mass)
        {
            this.mass = mass;
            var size = Mathf.Sqrt(mass + multipleA) * multipleB + multipleC;
            (data as PlayerData).model.localScale = new Vector3(size, size, 0);
        }
        public void OnBeUnmatched(bool isUnmatched)
        {
            if (isUnmatched)
                (data as PlayerData).model.GetComponent<SpriteRenderer>().color = unmathchedColor;
            else
                (data as PlayerData).model.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
}