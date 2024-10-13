using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Content
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        
        [SerializeField] private float _timeToSpawn;
        [SerializeField] private float _spawnRangeXMax;
        [SerializeField] private float _spawnRangeXMin;
        [SerializeField] private float _spawnRangeYMax;
        [SerializeField] private float _spawnRangeYMin;

        [SerializeField] private List<WaveInfo> waves;
        
        //TODO: Pool
        // private ObjectPool<EnemyComponent> enemyPool;
        private float _timer;
        private float _waveTimer;
        private int currentWave;

        private void Start()
        {
            currentWave = -1;
            GoToNextWave();
        }

        private void Update()
        {
            //TODO: gamemode.gametime?
            _waveTimer -= GameInstance.GameDelta;
            if(_waveTimer <= 0)
            {
                GoToNextWave();
            }

            _timer += GameInstance.GameDelta;
            if (_timer >= _timeToSpawn)
            {
                _timer -= _timeToSpawn;
                int randX =0, randY = 0;
                while (randX == 0 && randY == 0)
                {
                    randX = Random.Range(-1, 2);
                    randY = Random.Range(-1, 2);
                }
                var spawnPos = Vector3.zero;
                spawnPos.x = randX switch
                {
                    >= 1 => _spawnRangeXMax,
                    <= -1 => _spawnRangeXMin,
                    _ => Random.Range(_spawnRangeXMin, _spawnRangeXMax) * 0.5f
                };
                spawnPos.y = randY switch
                {
                    >= 1 => _spawnRangeYMax,
                    <= -1 => _spawnRangeYMin,
                    _ => Random.Range(_spawnRangeYMin, _spawnRangeYMax) * 0.5f
                };
                Instantiate(waves[currentWave].enemyToSpawn, _playerTransform.position + spawnPos, Quaternion.identity).gameObject.SetActive(true);
            }
        }

        private void GoToNextWave()
        {
            currentWave++;

            if(currentWave >= waves.Count)
            {
                currentWave = waves.Count - 1;
            }

            _waveTimer = waves[currentWave].waveLength;
            _timer = waves[currentWave].timeBetweenSpawns;
        }
    }
    
    [System.Serializable]
    public class WaveInfo
    {
        public GameObject enemyToSpawn;
        public float waveLength = 10f;
        public float timeBetweenSpawns = 1f;
    }
}