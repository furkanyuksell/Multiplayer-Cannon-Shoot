using UnityEngine;

namespace Game.Balls
{
    [CreateAssetMenu (fileName = "BallInfo", menuName = "NewBall")]
    public class BallInfo : ScriptableObject
    {
        public int damage;
        public float force;
        public float mass;
    }
}
