using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Result Text")]
    [SerializeField] private TMP_Text enemiesKillText;
    [SerializeField] private TMP_Text bossesKillText;
    [SerializeField] private TMP_Text rewardPointsText;
    [SerializeField] private TMP_Text scorePointsText;
    [SerializeField] private TMP_Text waveCompletedText;

    [Header("Button")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    private void OnEnable()
    {
        retryButton.onClick.AddListener(PlayRetryButton);
        mainMenuButton.onClick.AddListener(PlayMainMenuButton);
    }

    private void OnDisable()
    {
        retryButton.onClick.RemoveListener(PlayRetryButton);
        mainMenuButton.onClick.RemoveListener(PlayMainMenuButton);
    }

    public void SetGameResult(GameResultData resultData)
    {
        enemiesKillText.text = resultData.enemiesKillText;
        bossesKillText.text = resultData.bossesKillText;
        rewardPointsText.text = resultData.rewardPointsText;
        scorePointsText.text = resultData.scorePointsText;
        waveCompletedText.text = resultData.waveCompletedText;
    }

    private void PlayRetryButton()
    {
        GameEvents.RaiseGameRetry();
    }

    private void PlayMainMenuButton()
    {

    }
}

public struct GameResultData
{
    public string enemiesKillText;
    public string bossesKillText;
    public string rewardPointsText;
    public string scorePointsText;
    public string waveCompletedText;
}