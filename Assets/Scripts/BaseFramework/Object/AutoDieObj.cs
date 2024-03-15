using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
	public class AutoDieObj : BaseControl
	{
		[SerializeField] float lifeTime;
		WaitForSeconds wait_Die;

        private void Awake()
        {
            wait_Die = new WaitForSeconds(lifeTime);
        }
        public void Open(Vector3 pos)
        {
            IsAlive = true;
            transform.position = pos;
            gameObject.SetActive(true);
            StartCoroutine("AutoDying");
        }
        IEnumerator AutoDying()
        {
            yield return wait_Die;
            Close();
        }
        public override void Close()
        {
            IsAlive = false;
            PoolManager.Instance().Recycle(gameObject);
        }
    }
}