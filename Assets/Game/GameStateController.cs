using Events;
using UnityEngine;

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
        public static GameState GameState;

        private void Start()
        {
            GameState = GameState.InGame;

            Player.OnDeath += data => GameState = GameState.PostGame;
            Global.OnReset += () => GameState = GameState.InGame;
        }

        private void Update()
        {
        }
    }
}