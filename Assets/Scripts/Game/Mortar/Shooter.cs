using Game.Balls;
using Inputs;
using Manager;
using ObjectPool;
using Unity.Netcode;
using UnityEngine;

namespace Game.Mortar
{
    public class Shooter : NetworkBehaviour
    {
        BallPool _ballPool;
        private Ball _activeBall;

        private void OnShoot()
        {
            if (!IsOwner)
                return;
            
            if (IsHost)
            {
                ShootOnClientRpc(transform.position, transform.forward, Quaternion.identity);   
            }
            else
            {
                ShootOnServerRpc(transform.position, transform.forward, Quaternion.identity);
                Spawner(transform.position, transform.forward, Quaternion.identity);
            } 
        }
        
        [ClientRpc]
        private void ShootOnClientRpc(Vector3 position, Vector3 forward, Quaternion rotation)
        {
            Spawner(position, forward, rotation);
        }
        
        [ServerRpc]
        private void ShootOnServerRpc(Vector3 position, Vector3 forward, Quaternion rotation)
        {
            Spawner(position, forward, rotation);
        }

        private void Spawner(Vector3 position, Vector3 forward, Quaternion rotation)
        {
            var ball = _ballPool.GetBall(_activeBall);
            ball.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            ball.Fire(transform.forward);
        }
        
        

        public void SetNewBall(Ball ball)
        {
            _activeBall = ball;
            _ballPool = ServiceProvider.GetBallPool;
            _ballPool.InitBallPools(_activeBall);
        }
    
        private void OnEnable()
        {
            InputBase.OnShootInput += OnShoot;
            MortarController.SetBall += SetNewBall;
        }

        private void OnDisable()
        {
            InputBase.OnShootInput -= OnShoot;
            MortarController.SetBall -= SetNewBall;
        }
    }
}
