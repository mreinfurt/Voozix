#region Namespaces

using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

#endregion

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        #region Fields

        public Button PlayCampaignButton;
        public Button PlaySurvivalButton;
        public Button Quit;

        public Button MuteButton;

        #endregion

        #region Methods

        void Start()
        {
            this.PlaySurvivalButton.onClick.AddListener(this.HandlePlaySurvivalButtonClick);
            this.PlayCampaignButton.onClick.AddListener(this.HandlePlayCampaignButtonClick);
            this.Quit.onClick.AddListener(this.HandleQuitButtonClick);

            if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                this.PlaySurvivalButton.Select();
            }

            this.MuteButton.onClick.AddListener(this.HandleOnMuteButtonClick);
            this.MuteButton.gameObject.GetComponent<ToggleButton>().SetState(PlayerDataHolder.Instance.Data.MusicEnabled);
        }

        private void HandleOnMuteButtonClick()
        {
            PlayerDataHolder.Instance.Data.MusicEnabled = !PlayerDataHolder.Instance.Data.MusicEnabled;
            this.MuteButton.gameObject.GetComponent<ToggleButton>().Toggle();
        }

        private void HandleQuitButtonClick()
        {
            Application.Quit();
        }

        private void HandlePlayCampaignButtonClick()
        {
        }

        private void HandlePlaySurvivalButtonClick()
        {
            if (NiceSceneTransition.instance != null)
            {
                NiceSceneTransition.instance.LoadScene("Survival");
            }
            else
            {
                SceneManager.LoadScene("Survival", LoadSceneMode.Single);
            }
        }

        void Update()
        {
        }

        #endregion
    }
}