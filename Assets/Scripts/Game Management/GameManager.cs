using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _numberOfPlayers;
    [SerializeField] private int _unitsPerPlayer;
    private int _playerIndex;
    private List<PlayerController> _players = new List<PlayerController>();

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        EventManager.Instance.unitDead += UnitDead;
        EventManager.Instance.playerLost += PlayerLost;
        StartGame();
    }

    private void StartGame()
    {
        _players = PlayerSpawner.Instance.SpawnPlayers(_numberOfPlayers, _unitsPerPlayer);
        RandomStartPlayer();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SwapPlayer();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            EventManager.Instance.StartGame?.Invoke(_numberOfPlayers);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(1);
        }
    }
    private void RandomStartPlayer()
    {
        _playerIndex = UnityEngine.Random.Range(0, 1);
        SwapPlayer();
    }
    private void SwapPlayer()
    {
        EventManager.Instance.InvokeTurnEnd();
        EventManager.Instance.InvokeChangeActivePlayer(_players[_playerIndex].GetPlayerID());
        _playerIndex++;
        _playerIndex %= _numberOfPlayers;
    }
    private void UnitDead()
    {
        SwapPlayer();
    }
    private void PlayerLost(int playerID)
    {
        _numberOfPlayers--;
        if (_numberOfPlayers <= 1)
        {
            Debug.Log("Game ended!");
        }
        else
        {
            Destroy(_players[playerID].gameObject);
            _players.RemoveAt(playerID);
            Debug.Log("Player " + playerID + " lost");
            SwapPlayer();
        }
    }
}
