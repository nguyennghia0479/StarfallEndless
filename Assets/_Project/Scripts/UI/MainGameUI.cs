using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private Button settingButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text rewardPointsText;
    [SerializeField] private TMP_Text scorePointText;

    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup mainGameUICG;
    [SerializeField] private float fadeDuration = 1f;

    private DOTweenManager dotTweenManager;

    private void OnEnable()
    {
        if (dotTweenManager != null)
            dotTweenManager.FadeIn(mainGameUICG, fadeDuration);

        settingButton.onClick.AddListener(OnSettingButtonClicked);
        UIEvents.OnRewardChanged += UpdateRewardPointText;
        UIEvents.OnScoreChanged += UpdateScorePointText;
        UIEvents.OnSettingQuitButtonClicked += HandleQuitToMainMenu;
        UIEvents.OnQuitToGameOver += HandleQuitToGameOver;
    }

    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        UIEvents.OnRewardChanged -= UpdateRewardPointText;
        UIEvents.OnScoreChanged -= UpdateScorePointText;
        UIEvents.OnSettingQuitButtonClicked -= HandleQuitToMainMenu;
        UIEvents.OnQuitToGameOver -= HandleQuitToGameOver;
    }

    private void Start()
    {
        dotTweenManager = DOTweenManager.Instance;
    }

    private void OnSettingButtonClicked()
    {
        UIManager.Instance.SwitchToSettingUI();
        UIEvents.RaiseButtonClicked();
    }

    private void UpdateRewardPointText(int currentReward)
    {
        StartCoroutine(ChangeRewadPointsRoutine(currentReward));
    }

    private IEnumerator ChangeRewadPointsRoutine(int targetValue)
    {
        float elapsedTime = 0;

        if (!int.TryParse(rewardPointsText.text, out int lastValue))
        {
            rewardPointsText.text = targetValue.ToString();
            yield break;
        }

        while (elapsedTime <= 1)
        {
            float currentValue = Mathf.Lerp(lastValue, targetValue, Mathf.Clamp01(elapsedTime / 1));
            rewardPointsText.text = Mathf.RoundToInt(currentValue).ToString();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rewardPointsText.text = targetValue.ToString();
    }

    private void UpdateScorePointText(int currentScore)
    {
        scorePointText.text = currentScore.ToString();
    }

    private void HandleQuitToMainMenu()
    {
        FadeOut(UIEvents.RaiseMainMenuButtonClicked);
    }

    private void HandleQuitToGameOver()
    {
        FadeOut(UIEvents.RaiseEndGameButtonClicked);
    }

    public void UpdatePointsOnReady(int currentReward)
    {
        UpdateRewardPointText(currentReward);
        UpdateScorePointText(0);
    }

    public void FadeOut(Action callback)
    {
        dotTweenManager.FadeOut(callback, mainGameUICG, fadeDuration);
    }
}
