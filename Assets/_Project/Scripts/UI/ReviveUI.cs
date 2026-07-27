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

    private void OnEnable()
    {
        UpdateRevivePointsText();
        reviveButton.onClick.AddListener(PlayReviveButton);
        endGameButton.onClick.AddListener(PlayEndGameButton);
    }

    private void OnDisable()
    {
        reviveButton.onClick.RemoveListener(PlayReviveButton);
        endGameButton.onClick.RemoveListener(PlayEndGameButton);
    }

    private void UpdateRevivePointsText()
    {
        revivePointsText.text = revivePointsAmount.ToString();
    }

    private void PlayReviveButton()
    {
        UIEvents.RaiseReviveButtonClicked(revivePointsAmount);

        revivePointsAmount = Mathf.RoundToInt(revivePointsAmount * revivePointFactor);
        UpdateRevivePointsText();
    }

    private void PlayEndGameButton()
    {
        UIEvents.RaiseEndGameButtonClicked();
    }

    public void EnableReviveButton(int revivePoint)
    {
        reviveButton.interactable = revivePoint >= revivePointsAmount;
    }
}
