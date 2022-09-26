using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Transform[] _playerSpawnLocations;
    private List<Transform> _availableSpawnLocations = new List<Transform>();
    private int _playerID;
    private int _unitID;

    [SerializeField] private List<string> names = new List<string>();
    private List<string> availableNames = new List<string>();

    private static PlayerSpawner _instance;
    public static PlayerSpawner Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        availableNames = names;
        _availableSpawnLocations = _playerSpawnLocations.ToList();
    }
    public List<PlayerController> SpawnPlayers(int numberOfPlayers, int unitsPerPlayer)
    {
        _playerID = 0;
        List<PlayerController> players = new List<PlayerController>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            _unitID = 0;
            PlayerController currentPlayer = Instantiate(_playerController, transform.position, Quaternion.identity);
            currentPlayer.SetUnits(SpawnUnits(unitsPerPlayer, currentPlayer));
            currentPlayer.SetStats(_playerID, numberOfPlayers);
            players.Add(currentPlayer);
            _playerID++;
        }
        return players;
    }
    public List<Unit> SpawnUnits(int numberOfUnits, PlayerController currentPlayer)
    {
        List<Unit> units = new List<Unit>();
        for(int i = 0; i < numberOfUnits; i++)
        {
            int index = Random.Range(0, _availableSpawnLocations.Count);
            var pos = _availableSpawnLocations[index].transform.position;
            _availableSpawnLocations.RemoveAt(index);

            Unit currentUnit = Instantiate(_unitPrefab, pos, Quaternion.identity);
            currentUnit.SetID(_playerID, _unitID);
            currentUnit.SetPlayer(currentPlayer);
            string name = availableNames[Random.Range(0, names.Count)];
            availableNames.Remove(name);
            currentUnit.SetName(name);
            units.Add(currentUnit);
            _unitID++;
        }
        return units;
    }
}