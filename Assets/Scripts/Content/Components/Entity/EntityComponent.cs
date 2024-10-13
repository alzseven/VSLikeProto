using Core;
using UnityEngine;

namespace Content.Components.Entity
{
    public abstract class EntityComponent : MonoBehaviour
    {
        [HideInInspector] public Vector3 lookingDirection;
        protected SpriteRenderer _spriteRenderer;
        protected Animator _animator;
        protected abstract float moveSpeed { get; }
        protected abstract float deltaTime { get; }
        
        protected abstract bool IsAlive();

        public virtual void MoveToDirection(Vector3 direction)
        {
            if (IsAlive() == false) return;
            
            lookingDirection = direction;
            transform.position += direction * (moveSpeed * deltaTime);
            
            AfterMoveToDirection();
        }

        protected virtual void AfterMoveToDirection()
        {
            _spriteRenderer.flipX = lookingDirection.x switch
            {
                > 0 => false,
                < 0 => true,
                _ => _spriteRenderer.flipX
            };
        }
        
        public abstract void TakeDamage(float damage, float knockBackForce = 0);

    }
}