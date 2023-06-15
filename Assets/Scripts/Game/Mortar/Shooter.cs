using Game.Balls;
using Inputs;
using Manager;
using Mirror;
using ObjectPool;
using UnityEngine;

namespace Game.Mortar
{
    public class Shooter : NetworkBehaviour
    {
        BallPool _ballPool;
        private Ball _activeBall;

        
        [Client]
        private void OnShoot()
        {
            if (!isLocalPlayer)
                return;
            CmdShoot();   
            /*
            if (isClientOnly)
            {
                
            }
            else
            {
                ShootOnServerRpc(transform.position, transform.forward, Quaternion.identity);
                Spawner(transform.position, transform.forward, Quaternion.identity);
            } 
            var shooterTransform = transform;
            Spawner(shooterTransform.position, shooterTransform.forward, Quaternion.identity);*/
        }
        
        [Command]
        private void CmdShoot()
        {
            RpcSpawner();
        }
        /*
        [Client]
        private void ShootOnServerRpc(Vector3 position, Vector3 forward, Quaternion rotation)
        {
            Spawner(position, forward, rotation);
        }
*/
        [ClientRpc]
        private void RpcSpawner()
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
