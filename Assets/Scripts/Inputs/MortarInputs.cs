using System;
using Manager;
using UnityEngine;

namespace Inputs
{
    public class MortarInputs : InputBase
    {
        private float RotationSpeed { get; }
    
        public MortarInputs()
        {
            RotationSpeed = 10f;
        }

        private Vector3 UpdateRotation()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            return new Vector2(vertical * RotationSpeed, horizontal * RotationSpeed);
        }
        
        void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                OnRotateInput?.Invoke(UpdateRotation());
                ShowTrajectory?.Invoke(true);
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                ShowTrajectory?.Invoke(false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootInput?.Invoke();
            }
        }
        private void OnGameEnd()
        {
            enabled = false;
        }
        private void OnEnable()
        {
            GameManager.OnGameEnd += OnGameEnd;
        }
        
        private void OnDisable()
        {
            GameManager.OnGameEnd -= OnGameEnd;   
        }
    }
}
