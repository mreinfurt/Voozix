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

            Player.OnDeath += data => GameState = GameState.PostGame;
            Global.OnReset += () => GameState = GameState.InGame;
        }

        private void Update()
        {
        }

        #endregion
    }
}