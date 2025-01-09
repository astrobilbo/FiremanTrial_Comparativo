using System;
using UnityEngine;

namespace FiremanTrial.Manager
{
    public static class GameManager
    {
        public static Action<GameState> GameStateChanged;
        private static GameState _gameState = GameState.MainMenu;

        public static void SetGameState(GameState newGameState)
        {
            if (AlreadInState(newGameState)) return;
            
            Debug.Log($"GameManager: Game state changed to {newGameState}");
            _gameState = newGameState;
            GameStateChanged?.Invoke(_gameState);
        }

        public static bool AlreadInState(GameState newGameState) => _gameState.Equals(newGameState);

        public static GameState GetGameState() => _gameState;
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        Pause,
        Win
    }
}
