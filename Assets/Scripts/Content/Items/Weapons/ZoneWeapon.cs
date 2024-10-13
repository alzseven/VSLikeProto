using Content.Data;
using Content.Items.Weapons.Damagers;
using Core;
using UnityEngine;

namespace Content.Items.Weapons
{
    public class ZoneWeapon : WeaponItem
    {
        public DotDamager damager;

        private float spawnTime, spawnCounter;

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

            spawnCounter -= GameInstance.GameDelta;
            if(spawnCounter <= 0f)
            {
                spawnCounter = spawnTime;

                Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);

                // SFXManager.instance.PlaySFXPitched(10);
            }
        }

        public override void Apply(PlayerStat stat)
        {
            damager.damageAmount = stats[itemLevel].damage;
            damager.lifeTime = stats[itemLevel].duration;

            damager.timeBetweenDamage = stats[itemLevel].speed;

            damager.transform.localScale = Vector3.one * stats[itemLevel].range;

            spawnTime = stats[itemLevel].timeBetweenAttacks;

            spawnCounter = 0f;
        }
    }
}