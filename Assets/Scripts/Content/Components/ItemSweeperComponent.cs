using Content.Components.StageItem;
using UnityEngine;

namespace Content.Components
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ItemSweeperComponent : MonoBehaviour
    {
        //TODO: should modified by player's stat
        public float _size;

        private void Awake() => transform.GetComponent<CircleCollider2D>().radius = _size;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("StageItem"))
            {
                if (other.TryGetComponent<StageItemComponent>(out var stageItem))
                {
                    stageItem.isMovingToPlayer = true;
                }
            }  
        }
    }
}