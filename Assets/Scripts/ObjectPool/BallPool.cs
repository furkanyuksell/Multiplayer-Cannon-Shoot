using System.Collections.Generic;
using Game.Balls;
using Interfaces;
using Manager;
using UnityEngine;

namespace ObjectPool
{
    public class BallPool : MonoBehaviour, IProvidable
    {
        private readonly Dictionary<Ball, ObjectPooler<Ball>> _pools = new();

        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        public void InitBallPools(Ball ball)
        {
            if (!_pools.ContainsKey(ball))
                _pools.Add(ball, new ObjectPooler<Ball>(ball.gameObject, transform));
        }

        public Ball GetBall(Ball ball)
        {
            if (_pools.TryGetValue(ball, out ObjectPooler<Ball> pool))
            {
                return pool.Pool.Get();
            }
            return null;
        }

    
    }
}
