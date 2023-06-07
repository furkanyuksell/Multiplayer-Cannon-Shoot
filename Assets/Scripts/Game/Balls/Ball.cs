using System;
using Game.Mortar;
using Interfaces;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Balls
{
    public abstract class Ball : NetworkBehaviour, IPoolable<Ball>
    {
    
        public BallInfo ballInfo;
    
        private ObjectPool<Ball> _ballPool;
        private Rigidbody _rigidbody;
        private void Awake()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
                _rigidbody.mass = ballInfo.mass;
            }
        }

        private float timercount = 0;
        private void Update()
        {
            timercount += Time.deltaTime;
            if (timercount > 5)
            {
                ReturnToPool();
                timercount = 0;
            }
        }

        public void Initialize(ObjectPool<Ball> objPool)
        {
            _ballPool = objPool;
        }

        public void Fire(Vector3 direction)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction * ballInfo.force, ForceMode.Impulse);
        }

        public void ReturnToPool()
        {
            _ballPool.Release(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out MortarInGame mortarInGame))
            {
                if (mortarInGame.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(ballInfo.damage);
                }
            }
        }
    }
}
