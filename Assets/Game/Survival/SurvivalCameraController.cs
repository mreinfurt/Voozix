#region Namespaces

using Data;
using Events;
using UnityEngine;

#endregion

namespace Game.Survival
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
            Player.OnDeathBegin += this.OnDeathBegin;
            Player.OnDeathEnd += this.OnDeathEnd;

            this.defaultCamera = Camera.main;
            this.defaultPosition = this.defaultCamera.transform.position;
        }

        private void OnDeathBegin(PlayerData playerData, Vector2 position)
        {
            this.shakeAmount = this.ShakeIntensity;
        }

        private void OnDeathEnd(PlayerData playerData, Vector2 position)
        {
            this.StopShaking();
        }

        private void Update()
        {
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