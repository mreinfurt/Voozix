#region Namespaces

using Data;
using UnityEngine;

#endregion

namespace Game
{
    public class PostEffectsController : MonoBehaviour
    {
        #region Fields

        public Material PostEffectMaterial;

        #endregion

        #region Methods

        private void Start()
        {
            this.PostEffectMaterial.SetFloat("_StartTime", -int.MaxValue);
            Events.Player.OnDeathBegin += this.OnDeathBegin;
        }

        private void OnDeathBegin(PlayerData playerData, Vector2 position)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(position);
            var texCoordPosition = new Vector2(screenPosition.x / Screen.width,
                screenPosition.y / Screen.height);

            texCoordPosition.y = 1 - texCoordPosition.y;

            Debug.Log("Shockwave: " + texCoordPosition + "... " + texCoordPosition.x);

            this.PostEffectMaterial.SetFloat("_StartTime", Time.timeSinceLevelLoad);
            this.PostEffectMaterial.SetVector("_ShockwaveCenter", texCoordPosition);
        }

        private void Update()
        {
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, this.PostEffectMaterial);
        }

        #endregion
    }
}