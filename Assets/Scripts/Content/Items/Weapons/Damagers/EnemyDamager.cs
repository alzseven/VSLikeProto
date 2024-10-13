using Content.Components.Entity;
using Core;
using UnityEngine;

namespace Content.Items.Weapons.Damagers
{
    public class EnemyDamager : MonoBehaviour
    {
        public float damageAmount;

        public float lifeTime, growSpeed = 5f;
        private Vector3 targetSize;
    
        public float knockBackPower;
    
        public bool destroyParent;
    
        public bool destroyOnImpact;

        // Start is called before the first frame update
        void Start()
        {
            //Destroy(gameObject, lifeTime);

            targetSize = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            transform.localScale = 
                Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * GameInstance.GameDelta);

            lifeTime -= GameInstance.GameDelta;

            if(lifeTime <= 0)
            {
                targetSize = Vector3.zero;

                if (transform.localScale.x == 0f)
                {
                    Destroy(gameObject);

                    if(destroyParent)
                    {
                        Destroy(transform.parent.gameObject);
                    }
                }
            }

        
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyComponent>().TakeDamage(damageAmount, knockBackPower);

                if(destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        }

    
    }
}