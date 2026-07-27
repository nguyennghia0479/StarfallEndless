using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardPointsText;

    [Header("Button Elements")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button hangarButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;

    private void OnEnable()
    {
        UIEvents.OnRewardChanged += UpdateRewardPointsText;

        playButton.onClick.AddListener(OnPlayButtonClicked);
        hangarButton.onClick.AddListener(OnHangarButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        settingButton.onClick.AddListener(OnSettingButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        UIEvents.OnRewardChanged -= UpdateRewardPointsText;

        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        hangarButton.onClick.RemoveListener (OnHangarButtonClicked);
        creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
        settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void UpdateRewardPointsText(int rewardPoints)
    {
        rewardPointsText.text = rewardPoints.ToString();
    }

    private void OnPlayButtonClicked()
    {
        UIEvents.RaiseStartButtonClicked();
    }

    private void OnHangarButtonClicked()
    {
        UIManager.Instance.SwitchToHangarUI();
    }

    private void OnCreditsButtonClicked()
    {
        UIManager.Instance.SwitchToCreditsUI();
    }

    private void OnSettingButtonClicked()
    {
        UIManager.Instance.SwitchToSettingUI();
    }

    private void OnQuitButtonClicked()
    {
        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
}
