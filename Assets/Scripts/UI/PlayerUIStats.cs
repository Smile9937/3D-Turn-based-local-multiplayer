using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIStats : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private bool isActive;

    [SerializeField] private Image[] healthBars;
    [SerializeField] private TextMeshProUGUI nameBar;

    public void SetActive(bool active)
    {
        isActive = active;
    }
    public void SetStats(Color color, string name)
    {
        if (!isActive) return;

        foreach (Image bar in healthBars)
        {
            bar.color = color;
        }

        nameBar.text = name;
    }
}