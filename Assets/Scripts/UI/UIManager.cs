using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerUIStats[] playerUIStats;

    [SerializeField] private LayoutGroup layoutGroup;
    [SerializeField] private GameObject playerStatsDisplay;

    [Serializable]
    public class PlayerStats
    {
        public string playerName;
        public Color healthBarColor;
    }
    public PlayerStats[] players;
    private void Start()
    {
        EventManager.Instance.StartGame.AddListener(StartUI);
    }
    public void StartUI(int numberOfPlayers)
    {
        for(int i = 0; i < playerUIStats.Length; i++)
        {
            if (i >= numberOfPlayers)
            {
                playerUIStats[i].gameObject.SetActive(false);
                playerUIStats[i].SetActive(false);
            }
            else
            {
                playerUIStats[i].gameObject.SetActive(true);
                playerUIStats[i].SetActive(true);
                playerUIStats[i].SetStats(players[i].healthBarColor, players[i].playerName);
            }
        }
    }
}