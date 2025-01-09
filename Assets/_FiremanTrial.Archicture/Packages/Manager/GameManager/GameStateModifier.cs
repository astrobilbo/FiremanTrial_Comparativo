using UnityEngine;

namespace FiremanTrial.Manager
{
    public class GameStateModifier : MonoBehaviour
    {
        public GameState gameState;

        public void Change()
        {
            if (GameManager.AlreadInState(gameState)) return; 
            GameManager.SetGameState(gameState);
        }
    }
}