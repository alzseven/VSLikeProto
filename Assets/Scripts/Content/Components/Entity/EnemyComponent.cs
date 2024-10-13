using System.Collections;
using System.Collections.Generic;
using Content.Components.StageItem;
using Content.Data;
using Core;
using UnityEngine;

namespace Content.Components.Entity
{
    public class EnemyComponent : EntityComponent
    {
        // TODO: EnemySpawner.GetEnabledEnemies?
        public static List<EnemyComponent> EnabledEnemies = new();
        
        [Header("# Stats")] 
        [SerializeField] private EnemyStat _stat;
        [Header("# knockBack")]
        [SerializeField] private float knockBackTime = .5f;
        [Header("# Drop")] 
        [SerializeField] private ExpPickup _expPickup;
        [SerializeField] private int expToGive = 1;
        // TODO: Coin Drop Rate
        // [SerializeField] private int coinValue = 1;
        // [SerializeField] private float coinDropRate = .5f;
        protected override float moveSpeed => _stat.moveSpeed;
        protected override float deltaTime => GameInstance.GameDelta;
        private float _knockBackTimer;
        private Vector3 _knockBackDirection;
        private float _knockBackForce;
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Dead = Animator.StringToHash("Dead");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

        }

        private void OnEnable() => EnabledEnemies.Add(this);

        private void OnDisable() => EnabledEnemies.Remove(this);

        private void Update()
        {
            if (IsAlive() == false) return;
            
            if (_knockBackTimer > 0)
            {
                _knockBackTimer -= deltaTime;
                
                transform.position += _knockBackDirection * (_knockBackForce * deltaTime);
                _knockBackForce *= _knockBackTimer / knockBackTime;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (IsAlive() == false) return;
            
            if (other.transform.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent<PlayerComponent>(out var playerComponent))
                {
                    playerComponent.TakeDamage(_stat.damage * deltaTime);
                }
            }
        }
        
        
        public override void TakeDamage(float damage, float knockBackForce = 0f)
        {
            if (IsAlive() == false) return;
            _stat.health -= damage;
            _animator.SetTrigger(Hit);
            if (IsAlive() == false)
            {
                OnDead();
                // TODO: Coin drop (+ Sfx)
                // TODO: Box drop
            }
            // TODO: Show Damage UIText
            if (_knockBackForce > 0)
            {
                _knockBackDirection = lookingDirection * -1f;
                _knockBackTimer = knockBackTime;
                _knockBackForce = knockBackForce * 2f;
            }
        }

        // TODO: DeActivate collision
        private void OnDead()
        {
            _animator.SetBool(Dead, true);
            StartCoroutine(DeActivate());
        }
        

        private IEnumerator DeActivate()
        {
            // TODO: Should affected by GameDelta
            yield return new WaitForSeconds(0.5f);
            Instantiate(_expPickup, transform.position, Quaternion.identity).expValue = expToGive;
            VSLikeProtoGameMode.KillCount++;
            //TODO: Return to pool?
            Destroy(gameObject);
            yield return null;
        }
        
        protected override bool IsAlive() => _stat.health > 0;
    }
}