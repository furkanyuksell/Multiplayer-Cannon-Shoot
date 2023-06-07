using System;
using UnityEngine;

namespace Inputs
{
    public class InputBase : MonoBehaviour
    {
        public static Action<Vector2> OnRotateInput;
        public static Action<bool> ShowTrajectory;
        public static Action OnShootInput;
    }
}
