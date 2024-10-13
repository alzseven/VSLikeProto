using Content.Data;
using Content.Items.Weapons.Damagers;
using Core;
using UnityEngine;

namespace Content.Items.Weapons
{
    public class SpinWeapon : WeaponItem
    {
        public float rotateSpeed;

        public Transform holder, fireballToSpawn;

        public float timeBetweenSpawn;
        private float spawnCounter;

        public EnemyDamager damager;

        // Start is called before the first frame update
        // void Start()
        // {
        //     SetStats();
        //
        //     //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(this);
        // }

        // Update is called once per frame
        void Update()
        {
            holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[itemLevel].speed));


            spawnCounter -= GameInstance.GameDelta;
            if(spawnCounter <= 0)
            {
                spawnCounter = timeBetweenSpawn;

                for(int i = 0; i < stats[itemLevel].amount; i++)
                {
                    float rot = (360f / stats[itemLevel].amount) * i;

                    Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);

                    // SFXManager.instance.PlaySFX(8);
                }
            }

            // if(statsUpdated == true)
            // {
            //     statsUpdated = false;
            //
            //     SetStats();
            // }
        }

        public override void Apply(PlayerStat stat)
        {
            damager.damageAmount = stats[itemLevel].damage;

            transform.localScale = Vector3.one * stats[itemLevel].range;

            timeBetweenSpawn = stats[itemLevel].timeBetweenAttacks;

            damager.lifeTime = stats[itemLevel].duration;

            spawnCounter = 0f;
        }
    }
}