#region Namespaces

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class ToggleButton : MonoBehaviour
    {
        #region Fields

        private Image image;

        public Sprite Off;

        private bool on = true;
        public Sprite On;

        #endregion

        #region Methods

        private void Start()
        {
            this.image = this.gameObject.GetComponent<Image>();
            this.image.overrideSprite = this.On;
        }

        private void Update()
        {
        }

        public void Toggle()
        {
            this.on = !this.on;
            this.image.overrideSprite = this.on ? this.On : this.Off;
        }

        #endregion
    }
}