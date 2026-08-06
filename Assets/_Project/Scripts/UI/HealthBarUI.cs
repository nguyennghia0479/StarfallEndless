using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Canvas healthBarCanvas;
    [SerializeField] private float changeDuration = .5f;

    [Space]
    [SerializeField] private float healthThreshold = .35f;
    [SerializeField] private float blinkTime = .2f;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;

    private Coroutine healthChangeRoutine;
    private Coroutine textChangeRoutine;
    private Coroutine healthBlinkRoutine;
    private WaitForSeconds waitTime;
    private int lastHealthAmount;
    private bool isBlinking;

    private void Awake()
    {
        if (healthBarCanvas != null)
            healthBarCanvas.worldCamera = Camera.main;

        waitTime = new WaitForSeconds(blinkTime);
    }

    private void OnEnable()
    {
        UIEvents.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        UIEvents.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(GameObject gameObject, float maxHP, float currentHP)
    {
        if (gameObject != transform.root.gameObject)
            return;

        float newFillAmount = currentHP / maxHP;
        ChangeHealthAmount(newFillAmount);
        ChangeHealthText(newFillAmount);
        CheckCanPlayBlink(gameObject);
    }

    private void ChangeHealthAmount(float newFillAmount)
    {
        if (healthChangeRoutine != null)
            StopCoroutine(healthChangeRoutine);

        healthChangeRoutine = StartCoroutine(ChangeHealthRoutine(newFillAmount, healthBar.fillAmount));
    }

    private IEnumerator ChangeHealthRoutine(float targetValue, float lastValue)
    {
        float elapsedTime = 0;
        while (elapsedTime <= changeDuration)
        {
            float currentValue = Mathf.Lerp(lastValue, targetValue, Mathf.Clamp01(elapsedTime / changeDuration));
            healthBar.fillAmount = currentValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        healthBar.fillAmount = targetValue;
    }

    private void ChangeHealthText(float newFillAmount)
    {
        if (healthText == null)
            return;

        if (textChangeRoutine != null)
            StopCoroutine(textChangeRoutine);

        int targetValue = Mathf.RoundToInt(newFillAmount * 100);
        targetValue = Mathf.Clamp(targetValue, 0, targetValue);
        textChangeRoutine = StartCoroutine(ChangeTextRoutine(targetValue, lastHealthAmount));
    }

    private IEnumerator ChangeTextRoutine(int targetValue, int startValue)
    {
        float elapsedTime = 0;
        while (elapsedTime <= changeDuration)
        {
            float currentValue = Mathf.Lerp(startValue, targetValue, Mathf.Clamp01(elapsedTime / changeDuration));
            healthText.text = Mathf.RoundToInt(currentValue).ToString() + "%";
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        healthText.text = targetValue.ToString() + "%";
        lastHealthAmount = targetValue;
    }

    private void CheckCanPlayBlink(GameObject gameObject)
    {
        if (gameObject.CompareTag(GameIdentifiers.GameTags.TAG_PLAYER))
        {
            if (healthBar.fillAmount <= healthThreshold)
            {
                if (isBlinking) return;
                isBlinking = true;
                healthBlinkRoutine = StartCoroutine(HealthBlinkRoutine());
            }
            else
            {
                isBlinking = false;
                healthBar.color = greenColor;
                if (healthBlinkRoutine != null)
                    StopCoroutine(healthBlinkRoutine);
            }
        }
    }

    private IEnumerator HealthBlinkRoutine()
    {
        while (isBlinking)
        {
            healthBar.color = redColor;
            yield return waitTime;

            healthBar.color = greenColor;
            yield return waitTime;
        }
    }
}
