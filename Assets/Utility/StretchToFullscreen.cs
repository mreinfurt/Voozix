#region Namespaces

using System;
using UnityEngine;

#endregion

namespace Utility
{
    /// <summary>
    /// This script will stretch the Sprite to camera fullscreen
    /// </summary>
    public class StretchToFullscreen : MonoBehaviour
    {
        #region Fields

        public float DefaultScale = 1.31f;

        #endregion

        #region Methods

        void Start()
        {
        }

        void Update()
        {
            var sprite = this.GetComponent<SpriteRenderer>().sprite;
            var scale = new Vector2(this.DefaultScale, this.DefaultScale);

            float screenAspectRatio = (float) UnityEngine.Screen.width / UnityEngine.Screen.height;
            float textureAspectRatio = sprite.rect.width / sprite.rect.height;
            var difference = screenAspectRatio - textureAspectRatio;

            if (difference > 0)
            {
                scale.x += Math.Abs(difference);
            }
            else if (difference < 0)
            {
                scale.y += Math.Abs(difference);
            }

            this.transform.localScale = new Vector3(scale.x, scale.y, 0);
        }

        #endregion
    }
}