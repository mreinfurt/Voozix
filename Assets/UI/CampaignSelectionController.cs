#region Namespaces

using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;
using Input = UnityEngine.Input;

#endregion

namespace UI
{
    public class CampaignSelectionController : MonoBehaviour
    {
        #region Fields

        public Button BackButton;

        public Button MuteButton;

        public Button Level1Button;

        public Button Level2Button;

        public Button Level3Button;

        public Button Level4Button;

        public Button PlayButton;

        public Text LevelDescriptionText;

        public Text ScoreText;

        public Text StarsText;

        public Text TimeText;

        [SerializeField]
        private List<Image> LevelUnlockImages;

        public int Chapter;

        private int currentlySelectedLevel;

        #endregion

        #region Methods

        private void Start()
        {
            this.Level1Button.onClick.AddListener(() => this.SelectLevel(1));
            this.Level2Button.onClick.AddListener(() => this.SelectLevel(2));
            this.Level3Button.onClick.AddListener(() => this.SelectLevel(3));
            this.Level4Button.onClick.AddListener(() => this.SelectLevel(4));

            if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                this.Level1Button.Select();
            }

            this.BackButton.onClick.AddListener(this.HandleOnBackButtonClick);
            this.MuteButton.onClick.AddListener(this.HandleOnMuteButtonClick);
            this.MuteButton.gameObject.GetComponent<ToggleButton>()
                .SetState(PlayerDataHolder.Instance.Data.MusicEnabled);

            // Level unlock images
            var chapter = PlayerDataHolder.Instance.Data.ChapterData[this.Chapter];
            var currentLevel = 0;
            foreach (var level in chapter.LevelData)
            {
                this.LevelUnlockImages[currentLevel].color = level.Completed ? new Color(r: 0.078f, g: 0.521f, b: 0.8f, a: 1) : new Color(r: 0.29f, g: 0.29f, b: 0.29f, a: 0.745f);

                currentLevel++;
            }
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

        public void SelectLevel(int level)
        {
            this.PlayButton.onClick.RemoveAllListeners();
            this.PlayButton.onClick.AddListener(() => { this.PlayLevel(level);});
            this.PlayButton.Select();

            this.currentlySelectedLevel = level;
            this.LevelDescriptionText.text = "Level " + level;

            if (PlayerDataHolder.Instance.Data.ChapterData.Count >= level)
            {
                var levelData = PlayerDataHolder.Instance.Data.ChapterData[this.Chapter].LevelData[level - 1];
                var time = new TimeSpan(0, 0, 0, 0, (int) (levelData.CompletionTime * 1000));

                this.ScoreText.text = "" + levelData.Score;
                this.TimeText.text = string.Format("{0:00}:{1:00}:{2:00}", (int) time.TotalHours, time.Minutes,
                    time.Seconds);
                this.StarsText.text = levelData.Stars + "/" + "?";
            }
            else
            {
                this.ScoreText.text = "0";
                this.TimeText.text = "-:--";
                this.StarsText.text = "0/" + Game.Definitions.Level.LevelStarList[0][level];
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
            if (UnityEngine.Input.GetButtonDown("Cancel"))
            {
                this.Level1Button.Select();
            }
        }

        #endregion
    }
}