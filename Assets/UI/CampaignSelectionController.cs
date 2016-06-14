#region Namespaces

using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

#endregion

namespace UI
{
    public class CampaignSelectionController : MonoBehaviour
    {
        #region Fields

        public Button MuteButton;

        public Button BackButton;

        public Button PlayLevel1Button;

        public Button PlayLevel2Button;

        #endregion

        #region Methods

        private void Start()
        {
            this.PlayLevel1Button.onClick.AddListener(() => this.PlayLevel(1));
            this.PlayLevel2Button.onClick.AddListener(() => this.PlayLevel(2));

            if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                this.PlayLevel1Button.Select();
            }

            this.BackButton.onClick.AddListener(this.HandleOnBackButtonClick);
            this.MuteButton.onClick.AddListener(this.HandleOnMuteButtonClick);
            this.MuteButton.gameObject.GetComponent<ToggleButton>()
                .SetState(PlayerDataHolder.Instance.Data.MusicEnabled);
        }

        private void HandleOnBackButtonClick()
        {
            const string sceneToLoad = "MainMenu";
            if (NiceSceneTransition.instance != null)
            {
                NiceSceneTransition.instance.LoadScene(sceneToLoad);
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }
        }

        private void PlayLevel(int level)
        {
            var levelToLoad = "C1_L" + level;
            if (NiceSceneTransition.instance != null)
            {
                NiceSceneTransition.instance.LoadScene(levelToLoad);
            }
            else
            {
                SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
            }
        }

        // TODO: Refactor out to MuteButtonController
        private void HandleOnMuteButtonClick()
        {
            PlayerDataHolder.Instance.Data.MusicEnabled = !PlayerDataHolder.Instance.Data.MusicEnabled;
            this.MuteButton.gameObject.GetComponent<ToggleButton>().Toggle();
        }

        private void Update()
        {
        }

        #endregion
    }
}