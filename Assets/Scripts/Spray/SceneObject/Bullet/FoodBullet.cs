using BF;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Spray
{
	public class FoodBullet : Bullet
	{
        WaitForSeconds wait_LifeTime;

        [SerializeField] GameObject foodPrefab;

        private void Awake()
        {
            data = GetComponentInChildren<BaseShareData>();
        }
        public void Open(Vector3 pos, Vector3 orient, int mass,float speed,float lifeTime)
		{
            IsAlive = true;
            model.position = pos;
            model.up = orient;
            this.orient = orient;
            damage = mass;
            this.speed = speed;
            wait_LifeTime = new WaitForSeconds(lifeTime);

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
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsAlive)
                return;
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Close();
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
            base.Close();
            var food = PoolManager.Instance().Release(foodPrefab.name);
            food.GetComponent<Food>().Open(model.position, damage);
        }
    }
}