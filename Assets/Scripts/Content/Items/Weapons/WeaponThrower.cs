using Content.Data;
using Content.Items.Weapons.Damagers;
using Core;
using UnityEngine;

namespace Content.Items.Weapons
{
    public class WeaponThrower : WeaponItem
    {
        public EnemyDamager damager;

        private float throwCounter;

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

            throwCounter -= GameInstance.GameDelta;
            if (throwCounter <= 0)
            {
                throwCounter = stats[itemLevel].timeBetweenAttacks;

                for(int i = 0; i < stats[itemLevel].amount; i++)
                {
                    Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
                }

                // SFXManager.instance.PlaySFXPitched(4);
            }
        }

        public override void Apply(PlayerStat stat)
        {
            damager.damageAmount = stats[itemLevel].damage;
            damager.lifeTime = stats[itemLevel].duration;

            damager.transform.localScale = Vector3.one * stats[itemLevel].range;

            throwCounter = 0f;
        }
    }
}