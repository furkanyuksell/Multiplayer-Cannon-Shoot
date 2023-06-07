using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Manager;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MortarInGame : NetworkBehaviour, IDamageable
{
    [SerializeField] private Slider[] healthSliders;
    [SerializeField] Slider healthSlider;
    
    
    [SerializeField] private int health = 100; 
    private int Health { get; set; }
    private void Start()
    {
        Debug.Log("MortarInGame Start " + OwnerClientId);
        healthSlider = healthSliders[OwnerClientId];
        healthSlider.gameObject.SetActive(true);
        SetHealth();
    }
    private void SetHealth()
    {
        Health = health;
        healthSlider.maxValue = Health;
        healthSlider.value = Health;
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthSlider.value = Health;
        if (Health <= 0)
        {
            GameManager.OnGameEnd?.Invoke();
            GameManager.OnGameEndState?.Invoke(OwnerClientId == NetworkManager.Singleton.LocalClientId);
            enabled = false;
        }   
    }
}
