using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Spawn Locations", fileName = "New Player Spawn Locations")]
public class PlayerSpawnLocations : ScriptableObject
{
    [SerializeField] private GameObject[] _levelSpawnLocations;
    private List<Transform> _spawnLocations = new List<Transform>();

    public List<Transform> GetLocations(int level)
    {
        _spawnLocations = _levelSpawnLocations[level - 1].GetComponentsInChildren<Transform>().ToList();
        _spawnLocations.Remove(_levelSpawnLocations[level - 1].transform);
        return _spawnLocations;
    }
}