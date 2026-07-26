using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    [SerializeField] private Button reviveButton;
    [SerializeField] private Button endGameButton;

    private void OnEnable()
    {
        reviveButton.onClick.AddListener(PlayReviveButton);
        endGameButton.onClick.AddListener(PlayEndGameButton);
    }

    private void OnDisable()
    {
        reviveButton.onClick.RemoveListener(PlayReviveButton);
        endGameButton.onClick.RemoveListener(PlayEndGameButton);
    }

    private void PlayReviveButton()
    {
        UIEvents.RaisePlayerRevived();
    }

    private void PlayEndGameButton()
    {
        UIEvents.RaiseGameEnded();
    }
}
