using Interfaces;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Mortar
{
    public class MortarInGame : MonoBehaviour, IDamageable
    {
        [SerializeField] private Slider[] healthSliders;
        [SerializeField] Slider healthSlider;
    
    
        [SerializeField] private int health = 100; 
        private int Health { get; set; }
        private void Start()
        {
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
             TakeDamageClientRpc(damage);
        }
        
        //[ClientRpc]
        private void TakeDamageClientRpc(int damage)
        {
            Health -= damage;
            healthSlider.value = Health;
            if (Health <= 0)
            {
                GameManager.OnGameEnd?.Invoke();
                //GameManager.OnGameEndState?.Invoke(OwnerClientId == NetworkManager.Singleton.LocalClientId);
                enabled = false;
            }  
        }
    }
}
