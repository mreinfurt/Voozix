#region Namespaces

using Data;
using UnityEngine;

#endregion

namespace Game
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
            this.renderer.material.SetFloat("_StartTime", -50);

            Events.Player.OnDeathBegin += OnDeathBegin;
        }

        private void OnDeathBegin(PlayerData playerData, Vector2 position)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(position);
            var texCoordPosition = new Vector2(screenPosition.x / (float)Screen.width, screenPosition.y / (float)Screen.height);

            this.renderer.material.SetFloat("_StartTime", Time.time);
            this.renderer.material.SetVector("_ShockwaveCenter", texCoordPosition);
        }

        private void Update()
        {
        }

        #endregion
    }
}