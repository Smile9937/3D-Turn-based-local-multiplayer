using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovemet)), RequireComponent(typeof(UnitHealth))]
[RequireComponent(typeof(SwitchCamera)), RequireComponent(typeof(UnitUI))]
public class Unit : MonoBehaviour, IDestructible
{
    [SerializeField] private UnitUI _unitUI;
    [SerializeField] protected GameObject _mesh;
    private string _unitName;
    private UnitMovemet _movemet;
    private SwitchCamera _switchCamera;
    private UnitHealth _health;
    private int _playerID;
    private int _unitID;
    private PlayerController _player;

    private void Awake()
    {
        _movemet = GetComponent<UnitMovemet>();
        _switchCamera = GetComponent<SwitchCamera>();
        _health = GetComponent<UnitHealth>();
    }
    public void SetName(string name)
    {
        _unitUI.SetStats(name, 10);
    }
    public string GetName()
    {
        return _unitName;
    }
    private void OnEnable()
    {
        EventManager.Instance.turnEnd += TurnEnd;
    }
    private void OnDisable()
    {
        EventManager.Instance.turnEnd -= TurnEnd;
    }
    private void TurnEnd()
    {
        SetActive(false);
    }
    public void SetActive(bool active)
    {
        _movemet.ChangeActiveState(active);
        _switchCamera.ChangeActiveState(active);

            if (active)
            {
                _mesh.gameObject.layer = 8;
            }
            else
            {
                _mesh.gameObject.layer = 7;
            }
    }
    public void SetID(int player, int unit)
    {
        _playerID = player;
        _unitID = unit;
    }
    public void SetPlayer(PlayerController player)
    {
        _player = player;
    }

    public void Destroy()
    {
        _player.UnitDead(this);
        gameObject.SetActive(false);
    }
}