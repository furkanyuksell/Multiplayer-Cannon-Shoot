using UnityEngine;
using UnityEngine.UI;

namespace Game.Mortar
{
    [CreateAssetMenu(fileName = "PositionDatas", menuName = "PositionDatas")]
    public class PositionDatas : ScriptableObject
    {
        public Vector3 cameraRotation;
        public Vector3 cameraOffset;
        public Vector3 playerStartRotation;
        public Vector3 playerStartPosition;
    }
}
