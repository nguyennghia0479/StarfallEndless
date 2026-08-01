using DG.Tweening;
using System;
using UnityEngine;

public class DOTweenManager : MonoBehaviour
{
    public static DOTweenManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void FadeIn(CanvasGroup canvasGroup, float fadeDuration)
    {
        canvasGroup.alpha = 0;
        Sequence fadeInSeq = DOTween.Sequence();
        canvasGroup.blocksRaycasts = false;
        fadeInSeq.Append(canvasGroup.DOFade(1, fadeDuration));
        fadeInSeq.OnComplete(() =>
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void FadeOut(Action callback, CanvasGroup canvasGroup, float fadeDuration)
    {
        Sequence fadeInSeq = DOTween.Sequence();
        canvasGroup.blocksRaycasts = false;
        fadeInSeq.Append(canvasGroup.DOFade(0, fadeDuration));
        fadeInSeq.OnComplete(() =>
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = true;
            callback?.Invoke();
        });
    }
}
