using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scorePointText;

    private void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScorePointText;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScorePointText;
    }

    private void UpdateScorePointText(int scorePoints)
    {
        scorePointText.text = scorePoints.ToString();
    }
}
