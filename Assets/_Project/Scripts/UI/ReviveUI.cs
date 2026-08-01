using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    [Header("Revive Points")]
    [SerializeField] private int revivePointsAmount = 500;
    [SerializeField] private float revivePointFactor = 1.5f;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text revivePointsText;
    [SerializeField] private Button reviveButton;
    [SerializeField] private Button endGameButton;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private float duration = 15f;
    private float elapsedTimer;
    private bool isTimeOut;

    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup reviveUICG;
    [SerializeField] private float fadeDuration = 1f;

    private DOTweenManager dotTweenManager;

    private void OnEnable()
    {
        if (dotTweenManager != null)
            dotTweenManager.FadeIn(reviveUICG, fadeDuration);

        SetupTimeSlider();
        UpdateRevivePointsText();
        reviveButton.onClick.AddListener(OnReviveButtonClicked);
        endGameButton.onClick.AddListener(OnEndGameButtonClicked);
    }

    private void OnDisable()
    {
        reviveButton.onClick.RemoveListener(OnReviveButtonClicked);
        endGameButton.onClick.RemoveListener(OnEndGameButtonClicked);
    }

    private void Start()
    {
        dotTweenManager = DOTweenManager.Instance;
    }

    private void Update()
    {
        HandleTimeSlider();
    }

    private void UpdateRevivePointsText()
    {
        revivePointsText.text = revivePointsAmount.ToString();
    }

    private void OnReviveButtonClicked()
    {
        UIEvents.RaiseButtonClicked();
        int pointsToUse = revivePointsAmount;
        UIManager.Instance.FadeOutMainGameUI();
        FadeOut(() => UIEvents.RaiseReviveButtonClicked(pointsToUse));

        revivePointsAmount = Mathf.RoundToInt(revivePointsAmount * revivePointFactor);
        UpdateRevivePointsText();
    }

    private void OnEndGameButtonClicked()
    {
        UIEvents.RaiseButtonClicked();
        FadeOut(UIEvents.RaiseQuitToGameOver);
    }

    public void EnableReviveButton(int revivePoint)
    {
        reviveButton.interactable = revivePoint >= revivePointsAmount;
    }

    public void FadeOut(Action callback)
    {
        dotTweenManager.FadeOut(callback, reviveUICG, fadeDuration);
    }

    private void SetupTimeSlider()
    {
        isTimeOut = false;
        timeSlider.value = 1;
    }

    private void HandleTimeSlider()
    {
        if (isTimeOut || dotTweenManager == null)
            return;

        elapsedTimer += Time.deltaTime;
        float newValue = Mathf.Lerp(1, 0, elapsedTimer / duration);
        timeSlider.value = newValue;

        if (elapsedTimer >= duration)
        {
            elapsedTimer = 0;
            isTimeOut = true;
            dotTweenManager.FadeOut(UIEvents.RaiseQuitToGameOver, reviveUICG, fadeDuration);
        }
    }
}
