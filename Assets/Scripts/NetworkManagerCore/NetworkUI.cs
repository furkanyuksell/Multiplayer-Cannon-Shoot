using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace NetworkManagerCore
{
    public class NetworkUI : MonoBehaviour
    {
        [SerializeField] private Button serverBtn;
        [SerializeField] private Button hostBtn;
        [SerializeField] private Button clientBtn;

        private void Awake()
        {
            serverBtn.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartServer();
                gameObject.SetActive(false);
            });
            hostBtn.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
                gameObject.SetActive(false);
            });
            clientBtn.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
                gameObject.SetActive(false);
            });
        }
    }
}
