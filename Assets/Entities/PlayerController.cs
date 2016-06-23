#region Namespaces

using Data;
using Input;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace Entities
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        private new Collider2D collider2D;

        public bool IsActive = true;

        public bool IsAlive = true;

        [SerializeField] private Vector2 lastCheckpoint = Vector2.zero;

        private Vector2 movement;

        public float Speed = 5.5f;
        private TouchJoystickController touchController;

        #endregion

        #region Properties

        public int Score
        {
            get { return PlayerDataHolder.Instance.Data.Score; }
            private set { PlayerDataHolder.Instance.Data.Score = value; }
        }

        #endregion

        #region Methods

        private void Start()
        {
            Events.Global.OnReset += OnReset;
            Events.Player.OnFreezeMovement += OnFreezeMovement;

            this.touchController = this.GetComponent<TouchJoystickController>();
            this.collider2D = this.GetComponent<Collider2D>();

            PlayerDataHolder.Instance.Data.Score = 0;
            PlayerDataHolder.Instance.Player = this.gameObject;
        }

        private void OnDestroy()
        {
            Events.Global.OnReset -= OnReset;
            Events.Player.OnFreezeMovement -= OnFreezeMovement;
        }

        private void OnFreezeMovement(bool freeze)
        {
            this.IsActive = !freeze;
        }

        private void OnReset()
        {
            this.IsAlive = true;
            this.transform.position = new Vector3(this.lastCheckpoint.x, this.lastCheckpoint.y,
                this.transform.position.z);

            this.transform.localScale = Vector3.one;
            this.collider2D.enabled = true;
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
            if (!this.IsAlive || !this.IsActive)
            {
                return;
            }

            this.HandleInput();
            this.HandleMovement();
        }

        private void HandleInput()
        {
            this.movement.x = UnityEngine.Input.GetAxis(Game.Definitions.Player.HorizontalMovement) +
                              UnityEngine.Input.GetAxis(Game.Definitions.Player.JoystickHorizontalMovement) +
                              this.touchController.Value.x;
            this.movement.y = UnityEngine.Input.GetAxis(Game.Definitions.Player.VerticalMovement) +
                              UnityEngine.Input.GetAxis(Game.Definitions.Player.JoystickVerticalMovement) +
                              this.touchController.Value.y;
            this.movement.Normalize();
        }

        private void HandleMovement()
        {
            var position = new Vector3(this.Speed * this.movement.x * Time.deltaTime,
                this.Speed * this.movement.y * Time.deltaTime, 0);
            this.transform.position += position;
        }

        private void OnCollisionEnter2D(Collision2D otherCollider)
        {
            switch (otherCollider.gameObject.tag.ToLower())
            {
                case "star":
                    this.Score += 20;
                    Events.Player.OnStarCollected();
                    break;
                case "enemy":
                    Events.Player.OnDeathBegin(PlayerDataHolder.Instance.Data, this.transform.position);
                    this.HandleDeathBegin();
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            switch (otherCollider.gameObject.tag.ToLower())
            {
                case "checkpoint":
                    this.lastCheckpoint = this.transform.position;
                    Events.Player.OnReachedCheckpoint(this.transform.position);
                    break;

                case "goal":
                    this.lastCheckpoint = this.transform.position;
                    this.IsActive = false;
                    Events.Player.OnReachedGoal(this.transform.position);
                    break;
            }
        }

        private void HandleDeathBegin()
        {
            this.transform.localScale = Vector3.zero;
            this.collider2D.enabled = false;
            this.IsAlive = false;

            PlayerDataSaveController.Save(PlayerDataHolder.Instance.Data);
            this.GetComponent<AudioSource>().Play();

            Invoke("HandleDeathEnd", 0.3f);
        }

        [UsedImplicitly]
        private void HandleDeathEnd()
        {
            Events.Player.OnDeathEnd(PlayerDataHolder.Instance.Data, this.transform.position);
        }

        #endregion
    }
}