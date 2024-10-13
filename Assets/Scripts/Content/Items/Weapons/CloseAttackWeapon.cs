using Content.Data;
using Content.Items.Weapons.Damagers;
using Core;
using UnityEngine;

namespace Content.Items.Weapons
{
    public class CloseAttackWeapon : WeaponItem
    {
        public EnemyDamager damager;

        private float attackCounter, direction;

        // Start is called before the first frame update
        void Start()
        {
            // Apply();
        }

        // Update is called once per frame
        void Update()
        {
            // if (statsUpdated == true)
            // {
            //     statsUpdated = false;
            //
            //     Apply();
            // }

            attackCounter -= GameInstance.GameDelta;
            if(attackCounter <= 0)
            {
                attackCounter = stats[itemLevel].timeBetweenAttacks;

                //TODO: player's looking direction? how?
                direction = Input.GetAxisRaw("Horizontal");

                if (direction != 0)
                {
                    damager.transform.rotation = direction > 0 ? Quaternion.identity : Quaternion.Euler(0f, 0f, 180f);
                }

                Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);

                for (int i = 1; i < stats[itemLevel].amount; i++)
                {
                    float rot = (360f / stats[itemLevel].amount) * i;

                    Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.rotation.eulerAngles.z + rot), transform).gameObject.SetActive(true);

                }

                // SFXManager.instance.PlaySFXPitched(9);
            }
        }

        public override void Apply(PlayerStat stat)
        {
            base.Apply(stat);
            damager.damageAmount = stats[itemLevel].damage;
            damager.lifeTime = stats[itemLevel].duration;
            damager.transform.localScale = Vector3.one * stats[itemLevel].range;
            attackCounter = 0f;
        }
    }
}