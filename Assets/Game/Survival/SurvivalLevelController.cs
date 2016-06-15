#region Namespaces

using Data;
using UnityEngine;

#endregion

namespace Game.Survival
{
    public class SurvivalLevelController : MonoBehaviour
    {
        #region Fields

        private new Renderer renderer;

        #endregion

        #region Methods

        private void Start()
        {
            this.renderer = this.GetComponent<Renderer>();
            this.renderer.material.SetFloat("_StartTime", -int.MaxValue);

            Events.Player.OnDeathBegin += this.OnDeathBegin;
        }

        private void OnDeathBegin(PlayerData playerData, Vector2 position)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(position);
            var texCoordPosition = new Vector2(screenPosition.x / Screen.width,
                screenPosition.y / Screen.height);

            Debug.Log("Shockwave: " + texCoordPosition + "... " + texCoordPosition.x);

            if (this.renderer == null)
            {
                this.renderer = this.GetComponent<Renderer>();
            }

            this.renderer.material.SetFloat("_StartTime", Time.timeSinceLevelLoad);
            this.renderer.material.SetVector("_ShockwaveCenter", texCoordPosition);
        }

        private void Update()
        {
        }

        private void OnDestroy()
        {
            Events.Player.OnDeathBegin -= this.OnDeathBegin;
        }

        #endregion
    }
}