#region Namespaces

using Data;
using Events;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace Game
{
    public class SurvivalCameraController : MonoBehaviour
    {
        #region Fields

        private Camera defaultCamera;
        private Vector3 defaultPosition;
        private float shakeAmount;

        /// <summary>
        /// Intensity of the camera shake
        /// </summary>
        public float ShakeIntensity = 0.025f;

        #endregion

        #region Methods

        private void Start()
        {
            Player.OnDeath += this.OnDeath;
            this.defaultCamera = Camera.main;

            this.defaultPosition = this.defaultCamera.transform.position;
        }

        private void OnDeath(PlayerData playerData)
        {
            this.shakeAmount = this.ShakeIntensity;
            Invoke("StopShaking", 0.1f);
        }

        private void Update()
        {
            var quakeAmt = Random.value * this.shakeAmount * 2 - this.shakeAmount;
            var cameraChange = this.defaultCamera.transform.position;
            cameraChange.y += quakeAmt;
            cameraChange.x += quakeAmt;

            this.defaultCamera.transform.position = cameraChange;
        }

        [UsedImplicitly]
        private void StopShaking()
        {
            this.defaultCamera.transform.position = this.defaultPosition;
            this.shakeAmount = 0;
        }

        #endregion
    }
}