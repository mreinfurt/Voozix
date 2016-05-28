#region Namespaces

using UnityEngine;

#endregion

namespace Utility
{
    /// <summary>
    /// Provides easy tween functions
    /// </summary>
    class Tween
    {
        #region Methods

        /// <summary>
        /// Scales an object linearly up and down
        /// </summary>
        /// <param name="gameObject">GameObject to scale</param>
        /// <param name="range">Scale range (i.e. (0.5f, 1.5f)</param>
        /// <param name="animationTime">Current animation time (has to be tracked by your class)</param>
        /// <returns>Current animation time</returns>
        public static float LinearScaleInOut(GameObject gameObject, Vector2 range, float animationTime)
        {
            float scale = range.x;
            if (animationTime <= 1)
            {
                scale = Mathf.Lerp(range.x, range.y, animationTime);
            }
            else if (animationTime <= 2)
            {
                scale = Mathf.Lerp(range.y, range.x, animationTime - 1);
            }
            else
            {
                animationTime = 0;
            }

            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            return animationTime;
        }

        /// <summary>
        /// Scales an object up once
        /// </summary>
        /// <param name="gameObject">GameObject to scale</param>
        /// <param name="range">Scale range (i.e. (0f, 1f)</param>
        /// <param name="animationTime">Current animation time (has to be tracked by your class)</param>
        /// <param name="speed">Animation speed</param>
        /// <returns>Current animation time</returns>
        public static float LinearScaleOut(GameObject gameObject, Vector2 range, float animationTime, float speed = 1f)
        {
            var scale = Mathf.Lerp(range.x, range.y, animationTime * speed);
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            return animationTime;
        }

        #endregion
    }
}