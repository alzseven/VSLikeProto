using System.Collections.Generic;
using Content.Components.Entity;
using Core;
using UnityEngine;

namespace Content.Items.Weapons.Damagers
{
    public class DotDamager : EnemyDamager
    {
        [Header("DOT")]
        public float timeBetweenDamage;
        private float _damageTimer;

        private List<EnemyComponent> _enemiesInRange = new ();
    
        protected override void Update()
        {
            base.Update();
        
            _damageTimer -= GameInstance.GameDelta;

            if(_damageTimer <= 0)
            {
                _damageTimer = timeBetweenDamage;

                for(int i = _enemiesInRange.Count - 1; i >= 0; i--)
                {
                    //TODO:
                    if (_enemiesInRange[i] != null)
                        _enemiesInRange[i].TakeDamage(damageAmount, knockBackPower);
                    else
                        _enemiesInRange.RemoveAt(i);
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) _enemiesInRange.Add(collision.GetComponent<EnemyComponent>());
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy")) _enemiesInRange.Remove(collision.GetComponent<EnemyComponent>());
        }
    }
}