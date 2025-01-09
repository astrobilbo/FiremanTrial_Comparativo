using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.Manager
{
    public class GameStateChangeOnFocus : MonoBehaviour
    {
        [SerializeField]private GameState lastGameState;
        [SerializeField]private bool loadingScene = true;
        private void Awake()
        {
            lastGameState = GameManager.GetGameState();
        }

        private void Start()
        {
            loadingScene = false;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            switch (hasFocus, loadingScene)
            {
                case (false, false):
                    lastGameState = GameManager.GetGameState();
                    GameManager.SetGameState(GameState.Pause);
                    break;
                case (true, false):
                    GameManager.SetGameState(lastGameState);
                    break;;
            }
        }
    }
}