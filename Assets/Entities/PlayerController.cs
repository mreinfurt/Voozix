#region Namespaces

using Data;
using Events;
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
            private set
            {
                var difference = value - PlayerDataHolder.Instance.Data.Score;
                PlayerDataHolder.Instance.Data.Score = value;

                Player.OnScoreChanged(PlayerDataHolder.Instance.Data.Score, difference, this.transform.position);
            }
        }

        #endregion

        #region Methods

        private void Start()
        {
            Global.OnReset += OnReset;
            this.touchController = this.GetComponent<TouchJoystickController>();
            this.collider2D = this.GetComponent<Collider2D>();
        }

        private void OnReset()
        {
            this.Score = 0;
            this.IsAlive = true;
            this.transform.position = new Vector3(this.lastCheckpoint.x, this.lastCheckpoint.y,
                this.transform.position.z);

            this.transform.localScale = Vector3.one;
            this.collider2D.enabled = true;
        }

        private void Update()
        {
            if (!this.IsAlive)
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
                    Player.OnStarCollected();
                    break;
                case "enemy":
                    Player.OnDeathBegin(PlayerDataHolder.Instance.Data, this.transform.position);
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
                    break;

                case "goal":
                    this.lastCheckpoint = this.transform.position;
                    Player.ReachedGoal(this.transform.position);
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
            Player.OnDeathEnd(PlayerDataHolder.Instance.Data, this.transform.position);
        }

        #endregion
    }
}