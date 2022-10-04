using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;
using static UIManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _numberOfPlayers;
    [SerializeField] private int _unitsPerPlayer;
    [SerializeField] private PlayerSpawnLocations _spawnLocations;
    [SerializeField] private PlayerController _playerControllerPrefab;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private string[] _playerNameArray;
    [SerializeField] private PlayerColors _playerColors;
    [SerializeField] private float _timeUntilGameStart;
    [SerializeField] private float _timeUntilTurnEnd;

    private List<Material> _playerMaterials = new List<Material>();
    private List<string> _availableNames = new List<string>();
    private List<PlayerStats> _playerStats = new List<PlayerStats>();

    private List<string> _playerNames = new List<string>();

    private int _playerIndex;
    private int _currentLevel = 1;
    private List<PlayerController> _players = new List<PlayerController>();
    List<Transform> _locations = new List<Transform>();

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //EventManger Events
            unitDead += UnitDead;
            playerLost += PlayerLost;
            setPlayers += SetPlayers;
            enterGame += StartLevel;
            playerTurnOver += WaitForEndOfTurn;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void SetPlayers(int players, int units)
    {
        _numberOfPlayers = players;
        _unitsPerPlayer = units;
    }
    void Start()
    {
        GameSceneManager.SetScene();

        if(GameSceneManager.IsInLevel())
        {
            _currentLevel = GameSceneManager.GetLevel();
            _playerNames = new List<string>();
            _playerMaterials = new List<Material>();
            _availableNames = _playerNameArray.ToList();

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                string name = _availableNames[Random.Range(0, _availableNames.Count)];
                _playerNames.Add(name);
                _availableNames.Remove(name);

                _playerMaterials.Add(_playerColors.GetMaterialArray()[i]);
            }
            StartGame();
        }
    } 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SwapPlayer();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            InvokeStartGame(_numberOfPlayers);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(1);
        }
    }
    public void StartLevel(int level, List<PlayerStats> playerStats)
    {
        GameSceneManager.GoToLevel(level);
        _currentLevel = level;

        _playerStats = new List<PlayerStats> (playerStats);
        _playerNames = new List<string>();
        _playerMaterials = new List<Material>();
        _availableNames = _playerNameArray.ToList();

        for(int i = 0; i < _numberOfPlayers; i++)
        {
            if (_playerStats[i]._name == "")
            {
                string name = _availableNames[Random.Range(0, _availableNames.Count)];
                _playerNames.Add(name);
                _availableNames.Remove(name);
            }
            else
            {
                _playerNames.Add(_playerStats[i]._name);
            }
            _playerMaterials.Add(_playerColors.GetColor(_playerStats[i]._color));

        }
        StartCoroutine(WaitForScene());
    }
    private IEnumerator WaitForScene()
    {
        yield return null;
        StartGame();
    }
    private void StartGame()
    {
        _locations = _spawnLocations.GetLocations(_currentLevel);
        _players = PlayerSpawner.SpawnPlayers(_numberOfPlayers, _unitsPerPlayer, _locations, _playerControllerPrefab, _unitPrefab, _playerMaterials, _playerNames);
        StartCoroutine(StartDelay());
    }
    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(_timeUntilGameStart);
        RandomStartPlayer();
    }

    private void RandomStartPlayer()
    {
        _playerIndex = Random.Range(0, _numberOfPlayers - 1);
        SwapPlayer();
    }
    private void WaitForEndOfTurn()
    {
        //InvokePausePlayers();
        StartCoroutine(EndTurnTimer());
    }
    private IEnumerator EndTurnTimer()
    {
        yield return new WaitForSeconds(_timeUntilTurnEnd);
        SwapPlayer();
    }
    private void SwapPlayer()
    {
        _playerIndex %= _numberOfPlayers;
        InvokeTurnEnd();
        InvokeChangeActivePlayer(_players[_playerIndex].GetPlayerID());
        _playerIndex++;
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
    public int GetNumberOfPlayers()
    {
        return _numberOfPlayers;
    }

    public int GetNumberOfUnits()
    {
        return _unitsPerPlayer;
    }
}
