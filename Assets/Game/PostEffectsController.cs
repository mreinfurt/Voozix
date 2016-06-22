#region Namespaces

using System;
using Data;
using UnityEngine;

#endregion

namespace Game
{
    public enum PostEffect
    {
        Shockwave,
        Blur
    }

    public class PostEffectsController : MonoBehaviour
    {
        #region Fields

        public PostEffect PostEffect = PostEffect.Shockwave;

        public Material PostEffectMaterial;

        public Material BlurMaterial;

        #endregion

        #region Methods

        private void Start()
        {
            this.PostEffectMaterial.SetFloat("_StartTime", -int.MaxValue);
            Events.Player.OnDeathBegin += this.OnDeathBegin;
        }

        private void OnDeathBegin(PlayerData playerData, Vector2 position)
        {
            this.PostEffect = PostEffect.Shockwave;
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
            switch (PostEffect)
            {
                case PostEffect.Shockwave:
                    Graphics.Blit(src, dest, this.PostEffectMaterial);
                    break;
                case PostEffect.Blur:

                    int downsample = 3;
                    int width = src.width >> downsample;
                    int height = src.height >> downsample;

                    RenderTexture rt = RenderTexture.GetTemporary(width, height);
                    Graphics.Blit(src, rt);

                    for (int i = 0; i < 4; i++)
                    {
                        RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
                        Graphics.Blit(rt, rt2, this.BlurMaterial);
                        RenderTexture.ReleaseTemporary(rt);
                        rt = rt2;
                    }

                    Graphics.Blit(rt, dest, this.BlurMaterial);
                    RenderTexture.ReleaseTemporary(rt);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}