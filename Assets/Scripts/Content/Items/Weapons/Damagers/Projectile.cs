using Core;
using UnityEngine;

namespace Content.Items.Weapons.Damagers
{
    //TODO: Inherit EnemyDamager?
    public class Projectile : MonoBehaviour
    {
        public float moveSpeed;

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.up * (moveSpeed * GameInstance.GameDelta);
        }
    }
}