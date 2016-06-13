#region Namespaces

using UnityEngine;

#endregion

namespace Input
{
    public class TouchJoystickController : MonoBehaviour
    {
        #region Public

        private const float JoystickOffsetMaximum = 25f;

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

		private Color color = new Color(255, 255, 255, 128);

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

            this.joystickGameObject.transform.parent = this.joystickAreaGameObject.transform;
        }

        void Update()
        {
			this.joystickRenderer.color = this.isVisible ? this.color : Color.clear;
			this.joystickAreaRenderer.color = this.isVisible ? this.color : Color.clear;

            if (UnityEngine.Input.touchCount <= 0)
            {
                return;
            }

            var touch = UnityEngine.Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    this.isVisible = true;
                    this.position = touch.position;
					var worldPosition = Camera.main.ScreenToWorldPoint(this.position);
					this.joystickAreaGameObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
                    break;
                case TouchPhase.Ended:
                    this.isVisible = false;
                    break;
            }

            var delta = touch.position - this.position;

            if (delta.magnitude > JoystickOffsetMaximum)
            {
                delta.x *= 1 / delta.magnitude * JoystickOffsetMaximum;
                delta.y *= 1 / delta.magnitude * JoystickOffsetMaximum;
            }

			var joystickWorldPosition = Camera.main.ScreenToWorldPoint(this.position + new Vector2(delta.x, delta.y));
			this.joystickGameObject.transform.position = new Vector3(joystickWorldPosition.x, joystickWorldPosition.y, 0);
            this.x = delta.x / JoystickOffsetMaximum;
            this.y = delta.y / JoystickOffsetMaximum;
        }

        #endregion
    }
}