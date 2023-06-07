using System;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static Action OnGameStart;
        public static Action<bool> OnGameEndState;
        public static Action OnGameEnd;
        public static Action OnGameRestart;
    }
}
