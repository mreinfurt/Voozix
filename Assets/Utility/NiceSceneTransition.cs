#region Namespaces

using System.Collections;
using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

namespace Utility
{
    public class NiceSceneTransition : MonoBehaviour
    {
        #region Public

        public static NiceSceneTransition instance;

        #endregion

        #region Fields

        public Image fadeImg;

        public bool fadeIn;
        public bool fadeOut;

        float time = 0f;

        public float transitionTime = 1.0f;

        #endregion

        #region Methods

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this.transform.gameObject);
                instance = this;
                if (this.fadeIn)
                {
                    this.fadeImg.color = new Color(this.fadeImg.color.r, this.fadeImg.color.g, this.fadeImg.color.b,
                        1.0f);
                }
            }
            else
            {
                Destroy(this.transform.gameObject);
            }
        }

        void OnEnable()
        {
            if (this.fadeIn)
            {
                this.StartCoroutine(this.StartScene());
            }
        }

        public void LoadScene(string level)
        {
            this.StartCoroutine(this.EndScene(level));
        }

        IEnumerator StartScene()
        {
            this.time = 1.0f;
            yield return null;
            while (this.time >= 0.0f)
            {
                this.fadeImg.color = new Color(this.fadeImg.color.r, this.fadeImg.color.g, this.fadeImg.color.b,
                    this.time);
                this.time -= Time.deltaTime * (1.0f / this.transitionTime);
                yield return null;
            }
            this.fadeImg.gameObject.SetActive(false);
        }

        IEnumerator EndScene(string nextScene)
        {
            this.fadeImg.gameObject.SetActive(true);
            this.time = 0.0f;
            yield return null;
            while (this.time <= 1.0f)
            {
                this.fadeImg.color = new Color(this.fadeImg.color.r, this.fadeImg.color.g, this.fadeImg.color.b,
                    this.time);
                this.time += Time.deltaTime * (1.0f / this.transitionTime);
                yield return null;
            }

            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            this.StartCoroutine(this.StartScene());
        }

        #endregion
    }
}