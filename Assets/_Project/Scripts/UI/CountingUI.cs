using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CountingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float countdown = 5f;

    [Header("Localization Dynamic")]
    [SerializeField] private string tableName;
    [SerializeField] private string countTextEntry;

    private float countdownTimer;
    private bool hasRollOut;
    private bool isRestarted;

    public void SetStartCountdown(bool isRestarted)
    {
        this.isRestarted = isRestarted;
        countdownTimer = countdown;
        hasRollOut = false;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        HandleCountdown();
    }

    private void HandleCountdown()
    {
        if (hasRollOut)
            return;

        countdownTimer -= Time.deltaTime;
        countdownText.text = Mathf.RoundToInt(countdownTimer).ToString();

        if (countdownTimer < 1)
            LocalizationManager.Instance.ChangeDynamicLocalizedText(tableName, countTextEntry, countdownText);

        if (countdownTimer <= -.5f)
        {
            hasRollOut = true;
            gameObject.SetActive(false);
            GameEvents.RaiseGameStart(isRestarted);
        }
    }

    //private void ShowLocalizedCountdownEnd()
    //{
    //    LocalizedString localizedEndText = new(tableName, countTextEntry);
    //    localizedEndText.StringChanged += (translatedValue) =>
    //    {
    //        if (countdownText != null)
    //        {
    //            countdownText.text = translatedValue;
    //        }
    //    };

    //    localizedEndText.RefreshString();
    //}
}
