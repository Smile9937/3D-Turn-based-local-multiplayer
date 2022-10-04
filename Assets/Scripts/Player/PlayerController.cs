using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int _unitIndex;
    private int _id;
    private bool _active;
    public List<Unit> _unitsList = new List<Unit>();
    public void SetUnits(List<Unit> units)
    {
        _unitsList = units;
    }
    private void OnEnable()
    {
        EventManager.changeActivePlayer += ChangeActivePlayer;
    }
    private void OnDisable()
    {
        EventManager.changeActivePlayer -= ChangeActivePlayer;
    }
    public int GetPlayerID()
    {
        return _id;
    }
    public void UnitDead(Unit unit)
    {
        _unitsList.Remove(unit);
        if(_unitsList.Count == 0)
        {
            EventManager.InvokePlayerLost(_id);
        } else
        {
            EventManager.InvokeUnitDead();
        }
    }
    private void ChangeActivePlayer(int player)
    {
        _active = _id == player;
        if (player == 0) { _unitIndex++; }
        if (_unitIndex >= _unitsList.Count) { _unitIndex = 0; }
        _unitsList[_unitIndex].SetActive(_active);
    }
    public void SetStats(int id, int numberOfPlayers)
    {
        _id = id;
    }
}