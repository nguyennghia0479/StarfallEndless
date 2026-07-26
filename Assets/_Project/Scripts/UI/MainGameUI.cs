using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardPointText;
    [SerializeField] private TMP_Text scorePointText;

    private void OnEnable()
    {
        UIEvents.OnRewardChanged += UpdateRewardPointText;
        UIEvents.OnScoreChanged += UpdateScorePointText;
    }

    private void OnDisable()
    {
        UIEvents.OnRewardChanged -= UpdateRewardPointText;
        UIEvents.OnScoreChanged -= UpdateScorePointText;
    }

    private void UpdateRewardPointText(int currentReward)
    {
        rewardPointText.text = currentReward.ToString();
    }

    private void UpdateScorePointText(int currentScore)
    {
        scorePointText.text = currentScore.ToString();
    }
}
