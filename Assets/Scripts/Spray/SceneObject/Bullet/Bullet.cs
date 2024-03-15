using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    public class Bullet : BaseControl
    {
        [SerializeField] protected float lifeTime;
        WaitForSeconds wait_LifeTime;

        [SerializeField] protected int damage;
        [SerializeField] protected float speed;
        protected bool isPlayerBullet;
        protected Vector3 orient;
        [SerializeField] protected Transform model;

        [SerializeField] bool isAlive;

        private void Awake()
        {
            data = GetComponentInChildren<BaseShareData>();
            wait_LifeTime = new WaitForSeconds(lifeTime);
        }
        public void Open(Vector3 pos, Vector3 orient, LayerMask layer)
        {
            if (layer == LayerMask.NameToLayer("Player"))
            {
                isPlayerBullet = true;
                gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
            }
            else
            {
                isPlayerBullet = false;
                gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
            }
            model.position = pos;
            model.up = orient;
            this.orient = orient;
            isAlive= IsAlive = true;

            gameObject.SetActive(true);
            StartCoroutine("WaitDie");
        }
        private void FixedUpdate()
        {
            if (!IsAlive)
            {
                return;
            }
            model.position += orient * speed * Time.fixedDeltaTime;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsAlive)
                return;
            Close();
            if (isPlayerBullet)
            {
                if (collision.transform.GetComponent<Turrent>())
                {
                    collision.transform.GetComponent<Turrent>().onBeHit.Invoke(damage);
                    
                }
            }
            else
            {
                if (collision.transform.GetComponentInParent<PlayerControl>())
                {
                    collision.transform.GetComponentInParent<PlayerControl>().onBeHit.Invoke(damage);
                }
            }
        }
        IEnumerator WaitDie()
        {
            yield return wait_LifeTime;
            if (IsAlive)
            {
                Close();
            }
        }
        public override void Close()
        {
            isAlive = IsAlive = false;
            PoolManager.Instance().Recycle(gameObject);
        }
    }
}