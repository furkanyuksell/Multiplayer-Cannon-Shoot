using Game.Balls;
using Interfaces;
using UnityEngine;

namespace Manager
{
    public class DataManager : MonoBehaviour, IProvidable
    {
        public BallsDatabase ballsDatabase;
        private void Awake()
        {
            ServiceProvider.Register(this);
        }
    }
}
