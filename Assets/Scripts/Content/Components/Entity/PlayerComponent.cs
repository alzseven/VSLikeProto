using System;
using Content.Data;
using Core;
using UnityEngine;

namespace Content.Components.Entity
{
    public class PlayerComponent : EntityComponent
    {
        public event Action OnPlayerDead;
        [SerializeField] private PlayerStat _playerStat;
        //TODO: VFX Manager, Handler, or component etc...
        [SerializeField] private ParticleSystem _bloodSpreadVfx;
        protected override float moveSpeed => _playerStat.moveSpeed;
        protected override float deltaTime => GameInstance.GameDelta;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponentInChildren<Animator>();
            _bloodSpreadVfx.gameObject.SetActive(false);
        }

        protected override bool IsAlive() => _playerStat.currentHealth > 0;

        protected override void AfterMoveToDirection()
        {
            _animator.SetFloat(Speed, lookingDirection == Vector3.zero ? 0 : 1 * _playerStat.moveSpeed);
            base.AfterMoveToDirection();
        }
        
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (IsAlive() == false) return;
            
            if (collision.transform.CompareTag("Enemy"))
            {
                _spriteRenderer.color = Color.red;
                _bloodSpreadVfx.gameObject.SetActive(true);

                if (IsAlive() == false)
                {
                    _spriteRenderer.color = Color.white;
                    _bloodSpreadVfx.gameObject.SetActive(false);
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            //TODO:
            if (other.transform.CompareTag("Enemy"))
            {
                _spriteRenderer.color = Color.white;
                _bloodSpreadVfx.gameObject.SetActive(false);
            }
        }

        
        public override void TakeDamage(float damage, float knockBackForce = 0)
        {
            if(IsAlive() == false) return;
            
            _playerStat.currentHealth -= Mathf.Max(damage - _playerStat.armor * deltaTime, deltaTime);
            _playerStat.OnHealthChanged?.Invoke(_playerStat.currentHealth, _playerStat.maxHealth);
            
            if (IsAlive() == false) OnDead();
        }
        
        private void OnDead()
        {
            _animator.SetBool(Dead, true);
            OnPlayerDead?.Invoke();
        }

    }
}