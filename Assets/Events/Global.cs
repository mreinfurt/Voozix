#region Namespaces

using System;

#endregion

namespace Events
{
    internal class Global
    {
        #region Public

        /// <summary>
        /// Called when the game resets
        /// </summary>
        public static Action OnReset;

        #endregion
    }
}