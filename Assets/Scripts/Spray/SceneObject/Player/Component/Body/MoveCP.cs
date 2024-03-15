using BF;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Spray
{
    public class MoveCP : PlayerComponent
    {
        //speed=a/(mass+b)
        [SerializeField] float speed;
        [SerializeField] float multipleA = 200f;
        [SerializeField] float multipleB = 100f;
        [SerializeField] Vector3 orient;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] float speedMultiple;
        float Speed => speed * (1 + speedMultiple);
        Coroutine coroutine;
        [SerializeField] int bumpDamage;

        [SerializeField] float shakeDuration;

        public void Open()
        {
            orient = Vector2.up;
            speedMultiple = 0;
        }
        public override void OnMassChange(int mass)
        {
            speed = multipleA / (mass + multipleB);
            rb.velocity = orient * Speed;
        }
        public void Move(Vector3 aimPos)
        {
            orient = (aimPos - (data as PlayerData).model.position).normalized;
            rb.velocity = orient * Speed;
        }
        public void SpeedChange(float multiple,float time)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(Changing(multiple, time));
        }
        IEnumerator Changing(float multiple, float time)
        {
            float start = Time.time;
            float accelerate = (multiple - speedMultiple) / time;
            while (Time.time - start < time)
            {
                speedMultiple += accelerate * Time.deltaTime;
                rb.velocity = orient * Speed;
                yield return null;
            }
            speedMultiple = multiple;
            rb.velocity = orient * Speed;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if((data as PlayerData).isUnmatched.Value)
            {
                Camera.main.DOShakePosition(shakeDuration);
            }
            if(!(data as PlayerData).isUnmatched.Value && collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                (data as PlayerData).mass.Value -= bumpDamage;
            }
            if(Vector3.Dot(orient, collision.contacts[0].normal) < 0)
            {
                orient = Vector3.Reflect(orient, collision.contacts[0].normal);
                rb.velocity = orient * Speed;
            }
        }
        public override void Close()
        {
            
        }
    }
}