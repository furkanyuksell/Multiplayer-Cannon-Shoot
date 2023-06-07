using System.Collections.Generic;
using UnityEngine;

namespace Game.Balls
{
    [CreateAssetMenu]
    public class BallsDatabase : ScriptableObject
    {
        [SerializeField]
        Ball[] balls;
    
        private Dictionary<string, Ball> _ballsDictionary = new Dictionary<string, Ball>();
    
        public void InitDictionary()
        {
            _ballsDictionary.Clear();
            foreach (Ball ball in balls)
            {
                _ballsDictionary.Add(ball.name, ball);
            }
        }
    
        public Ball GetBall(string name) => _ballsDictionary[name];
    
    
    }
}
