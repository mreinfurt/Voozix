#region Namespaces

using System.Collections.Generic;

#endregion

namespace Game
{
    namespace Definitions
    {
        public class Player
        {
            #region Public

            public static string HorizontalMovement = "Horizontal";
            public static string VerticalMovement = "Vertical";

            public static string JoystickHorizontalMovement = "HorizontalJoystick";
            public static string JoystickVerticalMovement = "VerticalJoystick";

            #endregion
        }

        public class Level
        {
            #region Public

            public static List<List<int>> LevelStarList = new List<List<int>>
            {
                new List<int>() {14, 0, 0, 0},
                new List<int>() {0, 0, 0, 0},
            };

            #endregion
        }

        namespace LocalizationKeys
        {
            /// <summary>
            /// Base translation class for keyboard environment
            /// </summary>
            public class Base
            {
                #region Public

                public static string TryAgain = "Press <color=\"#fffc19\">R</color> to try again";
                public static string Score = "Score";

                #endregion
            }

            /// <summary>
            /// Controller
            /// </summary>
            public class Console : Base
            {
                #region Public

                public new static string TryAgain = "Press <color=\"#fffc19\">X</color> to try again";

                #endregion
            }

            /// <summary>
            /// Touch-Device
            /// </summary>
            public class Touch : Base
            {
                #region Public

                public new static string TryAgain = "<color=\"#fffc19\">Tap</color> to try again";

                #endregion
            }
        }
    }
}