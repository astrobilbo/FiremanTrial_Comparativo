using System;
using System.Collections;
using System.Collections.Generic;

namespace FiremanTrial.Manager
{
    public static class GameManager
    {
        public static Action<GameState> GameStateChanged;
        private static GameState _gameState;

        public static void SetGameState(GameState newGameState)
        {
            if (_gameState.Equals(newGameState)) return;
            
            _gameState = newGameState;
            GameStateChanged?.Invoke(_gameState);
        }
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        Pause,
        Win
    }
}
