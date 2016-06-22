#region Namespaces

using Data;
using UnityEngine;

#endregion

namespace Game
{
    public class CampaignCameraController : MonoBehaviour
    {
        #region Fields

        private Camera defaultCamera;
        private Vector3 defaultPosition;

        public GameObject Player;
        private float shakeAmount;

        /// <summary>
        /// Intensity of the camera shake
        /// </summary>
        public float ShakeIntensity = 0.025f;

        #endregion

        #region Methods

        void Start()
        {
            Events.Player.OnDeathBegin += this.OnDeathBegin;
            Events.Player.OnDeathEnd += this.OnDeathEnd;

            this.defaultCamera = Camera.main;
            this.defaultPosition = this.defaultCamera.transform.position;
        }

        private void OnDeathBegin(PlayerData arg1, Vector2 arg2)
        {
            this.defaultPosition = this.defaultCamera.transform.position;
            this.shakeAmount = this.ShakeIntensity;
        }

        private void OnDeathEnd(PlayerData arg1, Vector2 arg2)
        {
            this.StopShaking();
        }

        private void OnDestroy()
        {
            Events.Player.OnDeathBegin -= this.OnDeathBegin;
            Events.Player.OnDeathEnd -= this.OnDeathEnd;
        }

        void Update()
        {
            var newPosition = new Vector3(this.Player.transform.position.x, this.Player.transform.position.y,
                this.transform.position.z);

            if (newPosition.x <= 0)
            {
                newPosition.x = 0;
            }

            this.transform.position = newPosition;

            var quakeAmount = Random.value * this.shakeAmount * 2 - this.shakeAmount;
            var cameraChange = this.defaultCamera.transform.position;
            cameraChange.y += quakeAmount;
            cameraChange.x += quakeAmount;

            this.defaultCamera.transform.position = cameraChange;
        }

        private void StopShaking()
        {
            this.defaultCamera.transform.position = this.defaultPosition;
            this.shakeAmount = 0;
        }

        #endregion
    }
}