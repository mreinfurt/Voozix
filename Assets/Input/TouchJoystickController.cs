#region Namespaces

using UnityEngine;

#endregion

namespace Input
{
    public class TouchJoystickController : MonoBehaviour
    {
        #region Public

        private const float JoystickOffsetMaximum = 20f;

        #endregion

        #region Fields

        private bool isVisible;

        public Sprite Joystick;
        public Sprite JoystickArea;
        private GameObject joystickAreaGameObject;
        private SpriteRenderer joystickAreaRenderer;

        private GameObject joystickGameObject;

        private SpriteRenderer joystickRenderer;

        private Vector2 position = Vector2.zero;

        private float x;
        private float y;

        #endregion

        #region Properties

        public Vector2 Value
        {
            get { return this.isVisible ? new Vector2(this.x, this.y) : Vector2.zero; }
            set
            {
                this.x = value.x;
                this.y = value.y;
            }
        }

        #endregion

        #region Methods

        void Start()
        {
            this.joystickGameObject = new GameObject();
            this.joystickRenderer = joystickGameObject.AddComponent<SpriteRenderer>();
            this.joystickRenderer.sprite = this.Joystick;

            this.joystickAreaGameObject = new GameObject();
            this.joystickAreaRenderer = joystickAreaGameObject.AddComponent<SpriteRenderer>();
            this.joystickAreaRenderer.sprite = this.JoystickArea;
        }

        void Update()
        {
            this.joystickRenderer.color = this.isVisible ? Color.white : Color.clear;
            this.joystickAreaRenderer.color = this.isVisible ? Color.white : Color.clear;

            if (UnityEngine.Input.touchCount <= 0)
            {
                return;
            }

            var touch = UnityEngine.Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    this.isVisible = false;
                    this.position = touch.position;
                    this.joystickAreaGameObject.transform.position = Camera.main.ScreenToWorldPoint(this.position);
                    break;
                case TouchPhase.Ended:
                    this.isVisible = false;
                    break;
            }

            var delta = touch.position - this.position;
            var vectorLength = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));

            this.joystickGameObject.transform.position = Camera.main.ScreenToWorldPoint(this.position);

            if (vectorLength > JoystickOffsetMaximum)
            {
                this.x *= 1 / vectorLength * JoystickOffsetMaximum;
                this.y *= 1 / vectorLength * JoystickOffsetMaximum;
            }

            this.x = delta.x / JoystickOffsetMaximum;
            this.y = delta.y / JoystickOffsetMaximum;
        }

        #endregion
    }
}