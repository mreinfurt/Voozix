#region Namespaces

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

public class MainMenuController : MonoBehaviour
{
    #region Fields

    public Button PlayCampaignButton;

    public Button PlaySurvivalButton;
    public Button Quit;

    #endregion

    #region Methods

    void Start()
    {
        this.PlaySurvivalButton.onClick.AddListener(HandlePlaySurvivalButtonClick);
        this.PlayCampaignButton.onClick.AddListener(HandlePlayCampaignButtonClick);
        this.Quit.onClick.AddListener(HandleQuitButtonClick);

        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            this.PlaySurvivalButton.Select();
        }
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