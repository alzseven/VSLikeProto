using Content.Data;
using Content.Items.Weapons.Damagers;
using Core;
using UnityEngine;

namespace Content.Items.Weapons
{
    public class ProjectileWeapon : WeaponItem
    {
        public EnemyDamager damager;
        public Projectile projectile;

        private float shotCounter;

        public float weaponRange;
        public LayerMask whatIsEnemy;

        // Start is called before the first frame update
        // void Start()
        // {
        //     SetStats();
        // }

        // Update is called once per frame
        void Update()
        {
            // if (statsUpdated == true)
            // {
            //     statsUpdated = false;
            //
            //     SetStats();
            // }

            shotCounter -= GameInstance.GameDelta;
            if(shotCounter <= 0)
            {
                shotCounter = stats[itemLevel].timeBetweenAttacks;

                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[itemLevel].range, whatIsEnemy);
                if(enemies.Length > 0)
                {
                    for(int i = 0; i < stats[itemLevel].amount; i++)
                    {
                        Vector3 closest = Vector3.positiveInfinity;
                        foreach (var enemy in enemies)
                        {
                            if (Vector3.Distance(transform.position, closest) >
                                Vector3.Distance(transform.position, enemy.transform.position))
                            {
                                closest = enemy.transform.position;
                            }
                        }
                        // Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                        // Vector3 direction = targetPosition - transform.position;
                        
                        Vector3 direction = closest - transform.position;
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        angle -= 90;
                        // projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        Instantiate(projectile, projectile.transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
                    }

                    // SFXManager.instance.PlaySFXPitched(6);
                }
            }
        }

        public override void Apply(PlayerStat stat)
        {
            damager.damageAmount = stats[itemLevel].damage;
            damager.lifeTime = stats[itemLevel].duration;

            damager.transform.localScale = Vector3.one * stats[itemLevel].range;

            shotCounter = 0f;

            projectile.moveSpeed = stats[itemLevel].speed;
        }
    }
}