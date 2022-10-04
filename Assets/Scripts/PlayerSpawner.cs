using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerSpawner
{
    private static List<Transform> _availableSpawnLocations = new List<Transform>();
    private static List<PlayerController> _players = new List<PlayerController>();
    private static List<Unit> _units = new List<Unit>();
    public static List<PlayerController> SpawnPlayers(int numberOfPlayers, int unitsPerPlayer, List<Transform> locations, PlayerController player, Unit unit, List<Material> materials, List<string> names)
    {
        _availableSpawnLocations = new List<Transform>(locations);
        _players = new List<PlayerController>();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            PlayerController currentPlayer = Object.Instantiate(player, Vector3.zero, Quaternion.identity);

            currentPlayer.SetStats(i, numberOfPlayers);
            currentPlayer.SetUnits(SpawnUnits(unitsPerPlayer, currentPlayer, materials[i], unit, names[i]));

            _players.Add(currentPlayer);
        }
        return _players;
    }
    public static List<Unit> SpawnUnits(int numberOfUnits, PlayerController currentPlayer, Material currentMaterial, Unit unit, string name)
    {
        _units = new List<Unit>();

        for(int i = 0; i < numberOfUnits; i++)
        {
            int index = Random.Range(0, _availableSpawnLocations.Count);
            Vector3 pos = _availableSpawnLocations[index].transform.position;
            _availableSpawnLocations.RemoveAt(index);

            Unit currentUnit = Object.Instantiate(unit, pos, Quaternion.identity);
            currentUnit.SetPlayer(currentPlayer);
            currentUnit.SetMaterial(currentMaterial);
            currentUnit.SetName(name);
            _units.Add(currentUnit);
        }
        return _units;
    }
}