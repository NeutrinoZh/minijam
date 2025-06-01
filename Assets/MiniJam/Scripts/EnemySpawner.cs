using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [Serializable]
        public struct Wave
        {
            [Serializable]
            public struct EnemySpawnParameters
            {
                public Enemy EnemyPrefab;
                public int Amount;
            }

            public List<EnemySpawnParameters> Enemies;
            public int Duration;
        }
        
        [SerializeField] private List<Wave> _waves;
        [SerializeField] private Vector2 _range;

        private float _spawnDelay;
        private DiContainer _container;
        private Entity _player;

        [Inject]
        public void Construct(DiContainer container, Player player)
        {
            _container = container;
            _player = player.GetComponent<Entity>();
        }

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SpawnRoutine()
        {
            if (_waves.Count == 0)
                yield break;

            var currentWaveId = 0;
            
            while (_player.IsAlive && currentWaveId < _waves.Count)
            {
                var currentWave = _waves[currentWaveId];
                
                foreach (var enemy  in currentWave.Enemies)
                    StartCoroutine(SpawnEnemy(currentWave.Duration, enemy));

                yield return new WaitForSeconds(currentWave.Duration);
                currentWaveId++;
            }
        }

        private IEnumerator SpawnEnemy(float duration,  Wave.EnemySpawnParameters parameters)
        {
            var delay = duration / parameters.Amount;
            var spawnedNumber = 0;

            while (spawnedNumber < parameters.Amount)
            {
                var position = Random.insideUnitCircle.normalized * Random.Range(_range.x, _range.y);

                var clone = _container.InstantiatePrefabForComponent<Enemy>(parameters.EnemyPrefab);
                clone.transform.position = _player.transform.position + new Vector3(position.x, 0, position.y);

                spawnedNumber += 1;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}