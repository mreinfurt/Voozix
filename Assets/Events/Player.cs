#region Namespaces

using System;
using Data;
using UnityEngine;

#endregion

namespace Events
{
    internal class Player
    {
        #region Public

        /// <summary>
        /// Called when the player collected a star
        /// </summary>
        public static Action OnStarCollected = () => {};

        /// <summary>
        /// Called when the player dies
        /// </summary>
        public static Action<PlayerData> OnDeathBegin = data => {};

        /// <summary>
        /// Called when the player died
        /// </summary>
        public static Action<PlayerData> OnDeathEnd = data => {};

        /// <summary>
        /// Called when the score of the player changes
        /// </summary>
        public static Action<int, int, Vector2> OnScoreChanged = (totalScore, difference, playerPosition) => {};

        #endregion
    }
}