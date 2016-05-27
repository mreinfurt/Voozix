using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Provides utility functions for the screen space
    /// </summary>
    public class Screen
    {
        /// <summary>
        /// Returns a rect spanning the full visible screen bounds
        /// </summary>
        /// <returns></returns>
        public static Rect GetScreenSpaceBounds()
        {
            var mainCamera = Camera.main;
            var cameraPosition = mainCamera.transform.position;

            const float screenSpaceOffset = 0.2f;
            var distance = new Vector2(mainCamera.aspect * mainCamera.orthographicSize - screenSpaceOffset,
                mainCamera.orthographicSize - screenSpaceOffset);

            var xMax = cameraPosition.x + distance.x;
            var xMin = cameraPosition.x - distance.x;

            var yMax = cameraPosition.y + distance.y;
            var yMin = cameraPosition.y - distance.y;

            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }
    }
}