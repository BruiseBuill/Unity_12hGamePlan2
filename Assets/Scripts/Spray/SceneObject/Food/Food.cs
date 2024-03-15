using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
    public class Food : BaseControl
    {
        [SerializeField] int mass;
        [SerializeField] float cannotAcquireTime = 1f;

        [SerializeField] Transform model;
        [SerializeField] float minSize;
        [SerializeField] float scale;

        Collider2D collider;
        WaitForSeconds wait_CannotAcquire;
        LayerMask playerLayer;

        public Vector3 Pos => transform.position;
        public int Mass => mass;

        private void Awake()
        {
            playerLayer = LayerMask.NameToLayer("Player");
            wait_CannotAcquire = new WaitForSeconds(cannotAcquireTime);
            collider = GetComponentInChildren<Collider2D>();
            data = GetComponentInChildren<BaseShareData>();
        }
        public void Open(Vector3 pos, int mass)
        {
            model.position = pos;
            var size = minSize + mass * scale;
            model.localScale = new Vector3(size, size, 1);
            this.mass = mass;
            collider.enabled = false;
            IsAlive = true;

            gameObject.SetActive(true);
            StartCoroutine("Wait");

        }
        IEnumerator Wait()
        {
            yield return wait_CannotAcquire;
            collider.enabled = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsAlive)
            {
                return;
            }
            if (collision.gameObject.layer == playerLayer)
            {
                collision.gameObject.GetComponentInParent<PlayerControl>().onGetFood.Invoke(mass);
                Close();
            }
        }
        public override void Close()
        {
            IsAlive = false;
            PoolManager.Instance().Recycle(gameObject);

        }
    }
}