using System.Collections;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] shipVisuals;
    [SerializeField] private SpriteRenderer shieldSr;

    [Header("Blink Effect")]
    [SerializeField] private float blinkTime = .2f;

    private float enableShieldTimer;
    private Coroutine blinkEffectRoutine;

    private void Update()
    {
        DisableShield();
    }

    public void EnableShipVisual()
    {
        foreach (var visual in shipVisuals)
            visual.gameObject.SetActive(true);
    }

    public void DisableShipVisual()
    {
        foreach (var visual in shipVisuals)
            visual.gameObject.SetActive(false);
    }

    public void EnableShield(Sprite shieldSprite, float duration)
    {
        enableShieldTimer = duration;
        shieldSr.sprite = shieldSprite;
        shieldSr.gameObject.SetActive(true);
    }

    private void DisableShield()
    {
        if (!shieldSr.gameObject.activeSelf)
            return;

        enableShieldTimer -= Time.deltaTime;
        if (enableShieldTimer <= 0)
            shieldSr.gameObject.SetActive(false);
    }

    public void HideShield() => enableShieldTimer = 0;

    public void PlayBlinkEffect()
    {
        StopBlinkEffect();
        blinkEffectRoutine = StartCoroutine(BlinkEffectRoutine());
    }

    public void StopBlinkEffect()
    {
        if (blinkEffectRoutine != null)
        {
            StopCoroutine(blinkEffectRoutine);
            blinkEffectRoutine = null;
        }

        foreach (var visual in shipVisuals)
            visual.color = Color.white;
    }

    private IEnumerator BlinkEffectRoutine()
    {
        while (true)
        {
            foreach (var visual in shipVisuals)
                visual.color = Color.clear;

            yield return new WaitForSeconds(blinkTime);

            foreach (var visual in shipVisuals)
                visual.color = Color.white;

            yield return new WaitForSeconds(blinkTime);
        }
    }

}
