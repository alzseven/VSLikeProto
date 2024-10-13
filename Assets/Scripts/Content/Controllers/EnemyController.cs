using Content.Components.Entity;
using UnityEngine;

namespace Content.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        // private List<EnemyComponent> _enemyComponents = new();
        //TODO: Only transform?
        private PlayerComponent _playerComponent;

        private void Awake()
        {
            _playerComponent = FindObjectOfType<PlayerComponent>();
        }

        private void Update()
        {
            foreach (var enemy in EnemyComponent.EnabledEnemies)
            {
                Vector3 direction = Vector3.Normalize(_playerComponent.transform.position - enemy.transform.position);
                enemy.MoveToDirection(direction);
            }
        }
    }
}