using TMPro;
using UnityEngine;

public class CountingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float countdown = 5f;

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
            countdownText.text = "ROLL OUT!";

        if (countdownTimer < 0)
        {
            hasRollOut = true;
            gameObject.SetActive(false);
            GameEvents.RaiseGameStart(isRestarted);
        }
    }
}
