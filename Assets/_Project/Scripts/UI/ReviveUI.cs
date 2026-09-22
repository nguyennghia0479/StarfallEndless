using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    [SerializeField] private InterstitialAdManager adManager;
    private bool isMobilePlatform;

    [Header("Revive Points")]
    [SerializeField] private int revivePointsAmount = 500;
    [SerializeField] private float revivePointFactor = 1.5f;

    [Space]
    [SerializeField] private TMP_Text revivePointsText;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private float duration = 15f;
    private float elapsedTimer;
    private bool isTimeOut;

    [Header("Button Elements")]
    [SerializeField] private Button reviveButton;
    [SerializeField] private Button watchAdButton;
    [SerializeField] private Button endGameButton;

    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup reviveUICG;
    [SerializeField] private float fadeDuration = 1f;

    private DOTweenManager dotTweenManager;
    private void Awake()
    {
        isMobilePlatform = Application.isMobilePlatform;
        watchAdButton.gameObject.SetActive(Application.isMobilePlatform);
#if UNITY_ANDROID
        isMobilePlatform = true;
        watchAdButton.gameObject.SetActive(true);
#endif
    }

    private void OnEnable()
    {
        if (dotTweenManager != null)
            dotTweenManager.FadeIn(reviveUICG, fadeDuration);

        SetupTimeSlider();
        UpdateRevivePointsText();
        reviveButton.onClick.AddListener(OnReviveButtonClicked);
        watchAdButton.onClick.AddListener(OnWatchAdButtonClicked);
        endGameButton.onClick.AddListener(OnEndGameButtonClicked);
    }

    private void OnDisable()
    {
        reviveButton.onClick.RemoveListener(OnReviveButtonClicked);
        watchAdButton.onClick.RemoveListener(OnWatchAdButtonClicked);
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

    private void OnWatchAdButtonClicked()
    {
        if (isMobilePlatform && adManager != null)
        {
            UIEvents.RaiseButtonClicked();
            adManager.ShowAd(() =>
            {
                UIEvents.RaiseWatchAdButtonClicked();
                EnableReviveButton(GameManager.Instance.RewardPoints);
            });
        }
    }

    private void OnEndGameButtonClicked()
    {
        if (isMobilePlatform && adManager != null)
            adManager.ShowAd(ProceedEndGameButtonClicked);
        else
            ProceedEndGameButtonClicked();
    }

    private void ProceedEndGameButtonClicked()
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
        elapsedTimer = 0;
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
