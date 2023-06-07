using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    private Transform _target;
    private Vector3 _offset;
    private bool _istargetNull;

    private void Start()
    {
        _istargetNull = _target == null;
    }

    private void Awake()
    {
        Instance = this;
    }
    void LateUpdate()
    {
        if (_istargetNull)
            return;
        transform.position = _target.position + _offset;
    }
    public void SetTarget(Transform target)
    {
        _istargetNull = false;
        _target = target;
    }
    public void SetCamera(Vector3 rotation, Vector3 offset)
    {
        transform.rotation = Quaternion.Euler(rotation);
        _offset = offset;
    }
    
    private void OnGameEnd()
    {
        _istargetNull = true;
        _target = null;
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
