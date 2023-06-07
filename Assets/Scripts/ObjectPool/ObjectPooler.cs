using Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPool
{
    public class ObjectPooler<T> where T : MonoBehaviour, IPoolable<T>
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public readonly ObjectPool<T> Pool;

        public ObjectPooler(GameObject prefab, Transform parent)
        {
            this._prefab = prefab;
            var parentObject = new GameObject(prefab.name + " Pool");
            parentObject.transform.parent = parent;
            _parent = parentObject.transform;
            Pool = new ObjectPool<T>(CreateObject, OnGetFromPool, OnReturnToPool);
        }

        private void OnReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnGetFromPool(T obj)
        {
            obj.gameObject.SetActive(true);
        }
    
        private T CreateObject()
        {
            T t;
            t = GameObject.Instantiate(_prefab, _parent).GetComponent<T>();
            t.gameObject.SetActive(true);
            t.Initialize(Pool);
            return t;
        }
    }
}
