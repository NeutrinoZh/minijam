using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Vector2 _range;
        [SerializeField] private float _startSpawnDelay;
        [SerializeField] private float _multiplierFactor;
        
        private float _spawnDelay;
        private DiContainer _container;
        private Player _player;
        
        [Inject]
        public void Construct(DiContainer container, Player player)
        {
            _container = container;
            _player = player;
        }
        
        private void Start()
        {
            _spawnDelay = _startSpawnDelay;
            
            StartCoroutine(DecreaseDelayRoutine());
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator DecreaseDelayRoutine()
        {
            while (true)
            {
                _spawnDelay *= _multiplierFactor;
                yield return new WaitForSeconds(1);
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                var position = Random.insideUnitCircle * Random.Range(_range.x, _range.y);

                var clone = _container.InstantiatePrefabForComponent<Enemy>(_enemyPrefab);
                clone.transform.position = _player.transform.position + new Vector3(position.x, 0, position.y);
                
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}