using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Canvas healthBarCanvas;

    private float maxHP;

    private void Awake()
    {
        if (healthBarCanvas != null)
            healthBarCanvas.worldCamera = Camera.main;
    }

    private void OnEnable()
    {
        GameEvents.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        GameEvents.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(GameObject gameObject, float currentHP)
    {
        if (gameObject != transform.gameObject)
            return;

        if (maxHP == 0)
            maxHP = currentHP;

        healthBar.fillAmount = currentHP / maxHP;
        if(healthText != null) 
            healthText.text = Mathf.RoundToInt(healthBar.fillAmount * 100).ToString() + "%";
    }
}
