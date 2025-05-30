using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game
{
    public class PoolOfObject<T> : ListOfObject<T> where T : MonoBehaviour
    {
        private readonly IObjectPool<T> _pool;
        
        public PoolOfObject(DiContainer container, T prefab)
        {
            _pool = new ObjectPool<T>(
                () =>  container.InstantiatePrefabForComponent<T>(prefab),
                Get,
                Release,
                obj => Object.Destroy(obj.gameObject),
                defaultCapacity: 20
            );
        }
        
        private void Get(T obj)
        {
            Objects.Add(obj);
            obj.gameObject.SetActive(true);
        }

        private void Release(T obj)
        {
            Objects.Remove(obj);
            obj.gameObject.SetActive(false);
        }
        
        public T Instantiate()
        {
            return _pool.Get();
        }

        public void Destroy(T obj)
        {
            _pool.Release(obj);
        }
    }
}