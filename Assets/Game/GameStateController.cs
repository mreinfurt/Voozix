#region Namespaces

using Events;
using UnityEngine;

#endregion

namespace Game
{
    public enum GameState
    {
        PreGame,
        Paused,
        InGame,
        PostGame
    }

    public class GameStateController : MonoBehaviour
    {
        #region Public

        public static GameState GameState;

        #endregion

        #region Methods

        private void Start()
        {
            GameState = GameState.InGame;

            Player.OnDeathBegin += (data, position) =>
            {
                const GameState newGameState = GameState.PostGame;
                Global.OnGameStateChanged(GameState, newGameState);
                GameState = newGameState;
            };

            Global.OnReset += () =>
            {
                const GameState newGameState = GameState.InGame;
                Global.OnGameStateChanged(GameState, newGameState);
                GameState = newGameState;
            };
        }

        private void Update()
        {
        }

        #endregion
    }
}