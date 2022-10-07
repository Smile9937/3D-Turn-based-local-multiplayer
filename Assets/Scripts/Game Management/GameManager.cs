using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;

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
    private int _startPlayerCount;
    private int _startUnitCount;
    private List<Material> _playerMaterials = new List<Material>();
    private List<string> _availableNames = new List<string>();
    private List<PlayerStats> _playerStats = new List<PlayerStats>();

    private List<string> _playerNames = new List<string>();

    private int _playerIndex;
    private int _currentLevel = 1;
    private Unit _activeUnit;
    private Camera _mainCamera;
    private List<PlayerController> _players = new List<PlayerController>();
    private List<Transform> _locations = new List<Transform>();

    private bool _gameActive;

    private static GameManager instance;
    private bool _turnEnding;

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
            playerTurnOver += PlayerTurnOver;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetActiveUnit(Unit unit)
    {
        _activeUnit = unit;
    }
    public Unit GetActiveUnit()
    {
        return _activeUnit;
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
        _startPlayerCount = _numberOfPlayers;
        _startUnitCount = _unitsPerPlayer;
        _gameActive = true;
        InvokeDeactivatePlayerCameras();
        _locations = _spawnLocations.GetLocations(_currentLevel);
        _players = new List<PlayerController> (PlayerSpawner.SpawnPlayers(_numberOfPlayers, _unitsPerPlayer, _locations, _playerControllerPrefab, _unitPrefab, _playerMaterials, _playerNames));
        CameraManager.SetCameras();
        _mainCamera = Camera.main;
        CameraManager.SetActiveCamera(_mainCamera);
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
    private void PlayerTurnOver()
    {
        StartCoroutine(EndTurnTimer());
    }
    private IEnumerator EndTurnTimer()
    {        
        _turnEnding = true;
        yield return new WaitForSeconds(_timeUntilTurnEnd);
        _turnEnding = false;
        SwapPlayer();
    }
    private IEnumerator UnitDeadTimer()
    {
        if(_turnEnding) yield break;
        yield return new WaitForSeconds(_timeUntilTurnEnd);
        SwapPlayer();
    }
    private void SwapPlayer()
    {
        if (!_gameActive) return;

        _playerIndex %= _numberOfPlayers;
        InvokeTurnEnd();
        if (_players[_playerIndex].HasUits())
        {
            InvokeChangeActivePlayer(_players[_playerIndex].GetPlayerID());
        }
        else
        {
            _playerIndex++;
            SwapPlayer();
            return;
        }
        OverlayManager.Instance.ToggleMoveText(true);
        OverlayManager.Instance.TogglePlayerTurnText(true);
        _playerIndex++;
    }
    private void UnitDead(bool active)
    {
        if (!active) return;
        CameraManager.SetActiveCamera(_mainCamera);
        OverlayManager.Instance.ToggleMoveText(false);
        OverlayManager.Instance.TogglePlayerTurnText(false);
        StartCoroutine(UnitDeadTimer());
    }
    private void PlayerLost(int playerID)
    {
        _numberOfPlayers--;
        Destroy(_players[playerID].gameObject);
        _players.RemoveAt(playerID);
        if(_numberOfPlayers > 1)
        {
            SwapPlayer();
        }
        else
        {
            _numberOfPlayers = _startPlayerCount;
            _unitsPerPlayer = _startUnitCount;
            GameSceneManager.GoToScene(Scene.EndScreen);
            StartCoroutine(WaitForEndScreen());
            _gameActive = false;
        }
    }
    private IEnumerator WaitForEndScreen()
    {
        yield return null;
        EndScreenManager.Instance.SetWinnerText(_players[0].GetName());
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