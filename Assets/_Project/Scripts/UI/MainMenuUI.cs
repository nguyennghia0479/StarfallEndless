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

    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup mainMenuUICG;
    [SerializeField] private float fadeDuration = 1f;

    private DOTweenManager dotTweenManager;

    private void OnEnable()
    {
        if (dotTweenManager != null)
            dotTweenManager.FadeIn(mainMenuUICG, fadeDuration);

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
        hangarButton.onClick.RemoveListener(OnHangarButtonClicked);
        creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
        settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void Start()
    {
        dotTweenManager = DOTweenManager.Instance;
    }

    public void UpdateRewardPointsText(int rewardPoints)
    {
        rewardPointsText.text = rewardPoints.ToString();
    }

    private void OnPlayButtonClicked()
    {
        UIEvents.RaiseButtonClicked();
        dotTweenManager.FadeOut(UIEvents.RaiseStartButtonClicked, mainMenuUICG, fadeDuration);
    }

    private void OnHangarButtonClicked()
    {
        UIManager.Instance.SwitchToHangarUI();
        UIEvents.RaiseButtonClicked();
    }

    private void OnCreditsButtonClicked()
    {
        UIManager.Instance.SwitchToCreditsUI();
        UIEvents.RaiseButtonClicked();
    }

    private void OnSettingButtonClicked()
    {
        UIManager.Instance.SwitchToSettingUI();
        UIEvents.RaiseButtonClicked();
    }

    private void OnQuitButtonClicked()
    {
        UIEvents.RaiseButtonClicked();
        dotTweenManager.FadeOut(HandleQuitGame, mainMenuUICG, fadeDuration);
    }

    private void HandleQuitGame()
    {  
        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
}
