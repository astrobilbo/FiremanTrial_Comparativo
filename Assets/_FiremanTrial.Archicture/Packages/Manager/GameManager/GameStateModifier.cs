using UnityEngine;

namespace FiremanTrial.Manager
{
    public class GameStateModifier : MonoBehaviour
    {
        public GameState gameState;

        public void Change()
        {
            GameManager.SetGameState(gameState);
        }
    }
}