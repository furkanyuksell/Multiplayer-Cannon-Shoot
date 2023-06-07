using System;
using Constants;
using Game.Balls;
using Inputs;
using Manager;
using UnityEngine;

namespace Game.Mortar
{
    public class MortarController : MonoBehaviour
    {
        private const float MuzzleRotHorzMaxLimit = 60f;
        private const float MuzzleRotHorzMinLimit = 360f - MuzzleRotHorzMaxLimit;
        private const float MuzzleRotVertMaxLimit = 360f - MuzzleRotVertMinLimit;
        private const float MuzzleRotVertMinLimit = 30f;
        [SerializeField] Transform rotationPoint;
        private void ProcessInput(Vector2 rotation)
        {
            Vector3 muzzleEuler = rotationPoint.localEulerAngles;
            float y = muzzleEuler.y + rotation.y;
            if (y is > MuzzleRotHorzMaxLimit and < 180f)
                y = MuzzleRotHorzMaxLimit;
            else if (y is < MuzzleRotHorzMinLimit and > 180f)
                y = MuzzleRotHorzMinLimit;
            muzzleEuler.y = Mathf.Lerp(muzzleEuler.y, y, Time.deltaTime * 10f);
        
            float x = muzzleEuler.x + rotation.x;
            if (x is > MuzzleRotVertMinLimit and < 180f)
                x = MuzzleRotVertMinLimit;
            else if (x is < MuzzleRotVertMaxLimit and > 180f)
                x = MuzzleRotVertMaxLimit;
            muzzleEuler.x = Mathf.Lerp(muzzleEuler.x, x, Time.deltaTime * 10f);
            rotationPoint.localEulerAngles = muzzleEuler;
        }

        public static Action<Ball> SetBall;
        private BallsDatabase _ballsDatabase; 
        private void Start()
        {
            _ballsDatabase = ServiceProvider.GetDataManager.ballsDatabase;
            _ballsDatabase.InitDictionary();
            SetBall?.Invoke(_ballsDatabase.GetBall(Constant.BaseBall));
        }

        private void OnEnable()
        {
            InputBase.OnRotateInput += ProcessInput;
        }

        private void OnDisable()
        {
            InputBase.OnRotateInput -= ProcessInput;
        }
    }
}
