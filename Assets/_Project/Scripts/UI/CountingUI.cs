using TMPro;
using UnityEngine;

public class CountingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float countdown = 5f;

    private float countdownTimer;
    private bool hasRollOut;

    private void Awake()
    {
        SetToCountdown();
    }

    private void Update()
    {
        HandleCountdown();
    }

    public void SetToCountdown()
    {
        countdownTimer = countdown;
        hasRollOut = false;
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
            GameEvents.RaiseStartGame();
        }
    }
}
