using Game.Balls;
using Inputs;
using Manager;
using ObjectPool;
using UnityEngine;

namespace Game.Mortar
{
    public class Shooter : MonoBehaviour
    {
        BallPool _ballPool;
        private Ball _activeBall;

        private void OnShoot()
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
