using System;
using Constants;
using Game.Balls;
using Inputs;
using Manager;
using Unity.Netcode;
using UnityEngine;

namespace Game.Mortar
{
    public class MortarController : NetworkBehaviour
    {
        private const float MuzzleRotHorzMaxLimit = 60f;
        private const float MuzzleRotHorzMinLimit = 360f - MuzzleRotHorzMaxLimit;
        private const float MuzzleRotVertMaxLimit = 360f - MuzzleRotVertMinLimit;
        private const float MuzzleRotVertMinLimit = 30f;
        [SerializeField] Transform rotationPoint;
        private void ProcessInput(Vector2 rotation)
        {
            if (!IsOwner)
                return;
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
            
            
            if (!IsOwner)
                return;
            PositionSet();
        
        }

        private void PositionSet()
        {
            CameraFollow.Instance.SetTarget(transform);
            
            PositionDatas positionData = ServiceProvider.GetDataManager.positionDatas[OwnerClientId];
            CameraFollow.Instance.SetCamera(positionData.cameraRotation , positionData.cameraOffset);
            transform.position = positionData.playerStartPosition;
            transform.rotation = Quaternion.Euler(positionData.playerStartRotation);
        }

        
        private void Update()
        {
            if (!IsOwner) return;

            Vector3 moveDir = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.LeftArrow)) moveDir += Vector3.left;
            if (Input.GetKey(KeyCode.RightArrow)) moveDir += Vector3.right;
        
            transform.position += moveDir * (Time.deltaTime * 10f);
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
