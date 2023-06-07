using TMPro;
using UnityEngine;

namespace Manager
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject gameEndPanel;
        [SerializeField] private TextMeshProUGUI endGameText;
    
        private void OnGameEnd()
        {
            gameEndPanel.SetActive(true);
        }
    
        private void OnGameEndState(bool isWin)
        {
            endGameText.text = isWin ? "You Lose" : "You Win";
        }
        private void OnEnable()
        {
            GameManager.OnGameEnd += OnGameEnd;
            GameManager.OnGameEndState += OnGameEndState;
        }
    
        private void OnDisable()
        {
            GameManager.OnGameEnd -= OnGameEnd;
            GameManager.OnGameEndState -= OnGameEndState;
        }
    }
}
