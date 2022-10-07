using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private PlayerColors _playerColors;

    [Header("Cavases")]
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _playerCountCanvas;
    [SerializeField] private Canvas _teamSelectCanvas;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown _numberOfPlayersDropdown;
    [SerializeField] private TMP_Dropdown _numberOfUnitsDropdown;

    [Header("Arrays")]
    [SerializeField] private GameObject[] _playerEdits;
    [SerializeField] private SkinnedMeshRenderer[] _playerModels;
    [SerializeField] private TeamColorDropdown[] _teamColorDropdowns;
    [SerializeField] private TMP_InputField[] _playerNameInputFields;
    [SerializeField] private TeamColor[] _colors;

    private PlayerStats _currentPlayerStats = new PlayerStats();

    private int _numberOfPlayers = 1;
    private int _numberOfUnits = 1;
    private int _levelToEnter = 1;
    private GameManager _gameManager;

    [SerializeField] private List<PlayerStats> _playerStats = new List<PlayerStats>();
    public enum MenuCanvas
    {
        Main,
        PlayerCount,
        TeamSelect
    }
    private void Start()
    {
        _gameManager = GameManager.Instance;
        SwitchCanvas(MenuCanvas.Main);
    }
    public void GoToPlayerCount()
    {
        SwitchCanvas(MenuCanvas.PlayerCount);

        _numberOfPlayers = _gameManager.GetNumberOfPlayers();
        _numberOfUnits = _gameManager.GetNumberOfUnits();

        _numberOfPlayersDropdown.value = _numberOfPlayers - 2;
        _numberOfUnitsDropdown.value = _numberOfUnits - 1;
    }
    public void StartGame()
    {
        _playerStats = new List<PlayerStats>();
        for(int i = 0; i < _numberOfPlayers; i++)
        {
            _currentPlayerStats = new PlayerStats();
            _currentPlayerStats._color = _teamColorDropdowns[i].Color();
            _currentPlayerStats._name = _playerNameInputFields[i].text;
            _playerStats.Add(_currentPlayerStats);
        }
        EventManager.InvokeSetPlayers(_numberOfPlayers, _numberOfUnits);
        EventManager.InvokeEnterGame(_levelToEnter, _playerStats);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectNumberOfPlayers(int numberOfPlayers)
    {
        _numberOfPlayers = numberOfPlayers + 2;
    }
    public void SelectNumberOfUnits(int numberOfUnits)
    {
        _numberOfUnits = numberOfUnits + 1;
    }

    public void Continue()
    {
        SwitchCanvas(MenuCanvas.TeamSelect);
        for(int i = 0; i < _playerEdits.Length;i++)
        {
            _playerEdits[i].SetActive(false);
            if(i < _numberOfPlayers)
            {
                _playerEdits[i].SetActive(true);
            }
        }

        for(int i = 0; i < _teamColorDropdowns.Length; i++)
        {
            _teamColorDropdowns[i].SetOptions(_playerColors.GetColorArray());
            _teamColorDropdowns[i].SetTeamColor(_colors[i]);
        }
    }
    public void SetColor(TeamColorDropdown dropdown)
    {
        _playerModels[dropdown.Player()].material = _playerColors.GetColor(dropdown.Color());
    }
    public void Back(int canvas)
    {
        switch(canvas)
        {
            case 0:
                SwitchCanvas(MenuCanvas.Main);
                break;
            case 1:
                SwitchCanvas(MenuCanvas.PlayerCount);
                break;
        }
    }

    private void SwitchCanvas(MenuCanvas canvas)
    {
        switch(canvas)
        {
            case MenuCanvas.Main:
                _mainCanvas.gameObject.SetActive(true);
                _playerCountCanvas.gameObject.SetActive(false);
                _teamSelectCanvas.gameObject.SetActive(false);
                break;
            case MenuCanvas.PlayerCount:
                _mainCanvas.gameObject.SetActive(false);
                _playerCountCanvas.gameObject.SetActive(true);
                _teamSelectCanvas.gameObject.SetActive(false);
                break;
            case MenuCanvas.TeamSelect:
                _mainCanvas.gameObject.SetActive(false);
                _playerCountCanvas.gameObject.SetActive(false);
                _teamSelectCanvas.gameObject.SetActive(true);
                break;
        }
    }
}