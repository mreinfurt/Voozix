#region Namespaces

using System;
using Game;

#endregion

namespace Events
{
    internal class Global
    {
        #region Public

        /// <summary>
        /// Called when the game resets
        /// </summary>
        public static Action OnReset = () => {};

        /// <summary>
        /// Called when the game state changes
        /// </summary>
        public static Action<GameState, GameState> OnGameStateChanged = (oldGameState, newGameState) => {};

        #endregion
    }
}