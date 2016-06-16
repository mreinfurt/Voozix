#region Namespaces

using Data;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Game
{
    public class PostEffectsController : MonoBehaviour
    {
        public Material PostEffectMaterial;

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