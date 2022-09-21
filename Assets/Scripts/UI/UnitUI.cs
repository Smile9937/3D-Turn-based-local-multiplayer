using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameBar;
    [SerializeField] private Slider _healthBar;

    public void SetStats(string name, int health)
    {
        _nameBar.text = name;
        _healthBar.maxValue = health;
    }
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        _healthBar.value = currentHealth;
    }
}
