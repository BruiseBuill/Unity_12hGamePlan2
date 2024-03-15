using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spray
{
    public class Turrent : BaseControl
    {
        public UnityAction<int> onBeHit = delegate { };

        [SerializeField] EnemyType enemyType;
        public EnemyType Type => enemyType;
        [Header("Health")]
        [Tooltip("size=sqrt( health*multipleA / maxHealth +multipleB")]
        [SerializeField] float size;
        [SerializeField] float multipleA;
        [SerializeField] float multipleB;
        [SerializeField] int health;
        [SerializeField] int maxHealth;
        
        [SerializeField] int beBumpDamage;
        [SerializeField] Transform model;
        public Transform Model => model;
        public int Health
        {
            get => health;
            set
            {
                health = value;
                
                if (health <= 0)
                {
                    Close();
                }
                else
                {
                    size = Mathf.Sqrt(health * multipleA / maxHealth) + multipleB;
                    model.localScale = new Vector3(size, size, 1);
                }
            }
        }
        [Header("Attack")]
        Transform aim;
        PlayerControl aimControl;
        LayerMask enemyLayer;

        [SerializeField] bool canAttack;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] float attackRange;
        [SerializeField] float attackBreak;
        float sqrAttackRange;
        WaitForSeconds wait_AttackBreak;


        private void Awake()
        {
            sqrAttackRange = attackRange * attackRange;
            wait_AttackBreak = new WaitForSeconds(attackBreak);
            enemyLayer = LayerMask.NameToLayer("Player");
            data = GetComponentInChildren<BaseShareData>();

            
        }
        public void Open(Vector3 pos)
        {
            IsAlive = true;
            canAttack = true;
            model.position = pos;
            Health = maxHealth;
            gameObject.SetActive(true);

            onBeHit += (damage) => Health -= damage;
        }
        protected void FixedUpdate()
        {
            if (!IsAlive)
            {
                return;
            }
            if (!aim)
            {
                var col = Physics2D.OverlapCircle(model.position, attackRange, 1<<enemyLayer);
                if (col)
                {
                    aimControl = col.GetComponentInParent<PlayerControl>();
                    aim = aimControl.Model;
                }
            }
        }
        private void Update()
        {
            if (!IsAlive)
            {
                return;
            }
            if (canAttack && aimControl) 
            {
                if (aimControl.IsAlive && (model.position - aim.position).sqrMagnitude < sqrAttackRange)
                {
                    Attack();
                }
                else
                {
                    aimControl = null;
                    aim = null;
                }
            }
        }
        void Attack()
        {
            StartCoroutine("CoolDown");
            var bullet = PoolManager.Instance().Release(bulletPrefab.name);
            bullet.GetComponent<Bullet>().Open(model.position, (aim.position - model.position).normalized, gameObject.layer);
        }
        IEnumerator CoolDown()
        {
            canAttack = false;
            yield return wait_AttackBreak;
            canAttack = true;
        }
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.layer == enemyLayer&& collision.collider.GetComponentInParent<PlayerControl>().IsUnmatch)
            {
                Health -= beBumpDamage;
            }
        }
        public override void Close()
        {
            onBeHit = delegate { };

            IsAlive = false;
            PoolManager.Instance().Recycle(gameObject);
        }
    }
}