#region Namespaces

using Data;
using Events;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace Entities
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        private PlayerData data = new PlayerData();

        public bool IsAlive = true;
        private Vector2 movement;

        public float Speed = 5.5f;

        #endregion

        #region Properties

        public int Score
        {
            get { return this.data.Score; }
            private set
            {
                var difference = value - this.data.Score;
                this.data.Score = value;

                Player.OnScoreChanged(this.data.Score, difference, this.transform.position);
            }
        }

        #endregion

        #region Methods

        private void Start()
        {
            this.data = PlayerDataSaveController.Load();
            Global.OnReset += OnReset;
        }

        private void OnReset()
        {
            this.Score = 0;
            this.IsAlive = true;
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
                              UnityEngine.Input.GetAxis(Game.Definitions.Player.JoystickHorizontalMovement);
            this.movement.y = UnityEngine.Input.GetAxis(Game.Definitions.Player.VerticalMovement) +
                              UnityEngine.Input.GetAxis(Game.Definitions.Player.JoystickVerticalMovement);
            this.movement.Normalize();
        }

        private void HandleMovement()
        {
            var position = new Vector3(this.Speed * this.movement.x * Time.deltaTime,
                this.Speed * this.movement.y * Time.deltaTime, 0);
            this.transform.position += position;
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            switch (collider.gameObject.tag.ToLower())
            {
                case "star":
                    this.Score += 20;
                    Player.OnStarCollected();
                    break;
                case "enemy":
                    Player.OnDeathBegin(this.data, this.transform.position);
                    this.HandleDeathBegin();
                    break;
            }
        }

        private void HandleDeathBegin()
        {
            PlayerDataSaveController.Save(this.data);
            this.GetComponent<AudioSource>().Play();
            this.IsAlive = false;

            Invoke("HandleDeathEnd", 0.3f);
        }

        [UsedImplicitly]
        private void HandleDeathEnd()
        {
            Player.OnDeathEnd(this.data, this.transform.position);
        }

        #endregion
    }
}