using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private float duration = 1f;

    public void TurnOffLoading()
    {
        StartCoroutine(TurnOffRoutine());
    }

    private IEnumerator TurnOffRoutine()
    {
        float time = 0;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(1, 0, time / duration);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
