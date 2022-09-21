using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Transform[] _playerSpawnLocations;
    private int _playerID;
    private int _unitID;

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
            int index = Random.Range(0, _playerSpawnLocations.Length);
            var pos = _playerSpawnLocations[index].transform.position;

            Unit currentUnit = Instantiate(_unitPrefab, pos, Quaternion.identity);
            currentUnit.SetID(_playerID, _unitID);
            currentUnit.SetPlayer(currentPlayer);
            units.Add(currentUnit);
            _unitID++;
        }
        return units;
    }
}