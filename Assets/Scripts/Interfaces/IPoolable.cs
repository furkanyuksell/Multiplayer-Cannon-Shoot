using UnityEngine.Pool;

namespace Interfaces
{
    public interface IPoolable<T> where T : class
    {
        void Initialize(ObjectPool<T> objPool);
        void ReturnToPool();
    }
}