using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private Button settingButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text rewardPointText;
    [SerializeField] private TMP_Text scorePointText;

    private void OnEnable()
    {
        settingButton.onClick.AddListener(PlaySettingButton);

        UIEvents.OnRewardChanged += UpdateRewardPointText;
        UIEvents.OnScoreChanged += UpdateScorePointText;
    }

    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(PlaySettingButton);

        UIEvents.OnRewardChanged -= UpdateRewardPointText;
        UIEvents.OnScoreChanged -= UpdateScorePointText;
    }

    private void PlaySettingButton()
    {
        UIManager.Instance.SwitchToSettingUI();
    }

    private void UpdateRewardPointText(int currentReward)
    {
        rewardPointText.text = currentReward.ToString();
    }

    private void UpdateScorePointText(int currentScore)
    {
        scorePointText.text = currentScore.ToString();
    }

    public void ResetPointsUI()
    {
        UpdateScorePointText(0);
        UpdateRewardPointText(0);
    }
}
