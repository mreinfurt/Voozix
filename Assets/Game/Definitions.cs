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

        namespace LocalizationKeys
        {
            /// <summary>
            /// Keyboard
            /// </summary>
            public class Desktop
            {
                public static string TryAgain = "Press <color=\"#fffc19\">R</color> to try again";
                public static string Score = "Score";
            }

            /// <summary>
            /// Controller
            /// </summary>
            public class Console
            {
                public static string TryAgain = "Press <color=\"#fffc19\">X</color> to try again";
                public static string Score = "Score";
            }

            /// <summary>
            /// Touch-Device
            /// </summary>
            public class Touch
            {
                public static string TryAgain = "<color=\"#fffc19\">Tap</color> to try again";
                public static string Score = "Score";
            }
        }
    }
}