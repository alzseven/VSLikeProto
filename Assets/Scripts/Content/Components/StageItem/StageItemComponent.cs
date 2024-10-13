using Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace Content.Components.StageItem
{
    public class StageItemComponent : MonoBehaviour
    {
        public float _moveSpeed;
        public bool isMovingToPlayer;
        private Transform _player;

        private void Awake()
        {
            isMovingToPlayer = false;
            //TODO:
            _player = GameObject.FindWithTag("Player").transform;
            Assert.IsNotNull(_player);
        }
        

        // Update is called once per frame
        void Update()
        {
            if (isMovingToPlayer)
            {
                //TODO: Accelerate by time
                transform.position = Vector3.MoveTowards(transform.position,
                    _player.position, Vector3.Distance(transform.position, _player.position) * (_moveSpeed * GameInstance.GameDelta));
            }
        }

        protected virtual void Use(){}
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Use();
                // TODO: pool
                Destroy(gameObject);
            }
        }
    }
}