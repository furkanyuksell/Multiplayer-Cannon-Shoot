using Game.Balls;
using Game.Mortar;
using Interfaces;
using UnityEngine;

namespace Manager
{
    public class DataManager : MonoBehaviour, IProvidable
    {
        public BallsDatabase ballsDatabase;
        public PositionDatas[] positionDatas;
        private void Awake()
        {
            ServiceProvider.Register(this);
        }
    }
}
