using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blind {
    public class ParasiteMonster : CrowdEnemyCharacter
    {
        private Coroutine Co_attack;
        private Coroutine Co_hitted;
        private Coroutine Co_stun;
        private Coroutine Co_die;

        public int StunGauge;
        public int maxStunGauge;

        protected void Awake()
        {
            base.Awake();

            Data.sensingRange = new Vector2(12f, 8f);
            Data.speed = 0.1f;
            Data.runSpeed = 0.07f;
            Data.attackCoolTime = 0.5f;
            Data.attackSpeed = 0.3f;
            Data.attackRange = new Vector2(9f, 8f);
            Data.stunTime = 1f;
            _patrolTime = 2;
        }

        private void Start()
        {
            startingPosition = gameObject.transform;
            _attack.Init(13, 10);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void updateAttack()
        {
            if (_anim.GetBool("Basic Attack") == false 
                && _anim.GetBool("Skill Attack") == false 
                && _anim.GetBool("Grab Attack") == false)
            {
                
                if (!createAttackHitBox)
                {
                    AttackHitBox();
                    createAttackHitBox = true;
                }
                
                float r = Random.Range(0, 100);
                if (r > 50)
                {
                    _anim.SetBool("Basic Attack", true);
                }
                else if (r <= 10)
                {
                    _anim.SetBool("Grab Attack", true);
                }
                else
                {
                    _anim.SetBool("Skill Attack", true);
                }
            }
        }

        public void AniGrab()
        {
            if (Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(11, 3, 0), 3f, 13))
            {
                Debug.Log("Grab Success");
                _anim.SetBool("Success", true);
            }
            else
            {
                Debug.Log("Grab Fail");
                _anim.SetBool("Fail", true);
            }
            _anim.SetBool("Grab Attack", false);
        }

        public override void AniAfterAttack()
        {
            base.AniAfterAttack();

            _anim.SetBool("Fail", false);
            _anim.SetBool("Success", false);
        }
        
        public void AttackHitBox()
        {
            col = gameObject.AddComponent<BoxCollider2D>();
            col.offset = new Vector2(_col.offset.x +3.5f, _col.offset.y);
            col.size = new Vector2(13, 10);
        }

        public override IEnumerator CoStun()
        {
            _anim.SetBool("Stun", true);
            _anim.SetBool("Basic Attack", false);
            _anim.SetBool("Skill Attack", false);
            _anim.SetBool("Grab Attack", false);

            yield return new WaitForSeconds(Data.stunTime);
            _anim.SetBool("Stun", false);
            NextAction();

            co_stun = null;
        }
    }
}
