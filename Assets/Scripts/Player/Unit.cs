using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovemet)), RequireComponent(typeof(UnitHealth)), RequireComponent(typeof(SwitchCamera)), RequireComponent(typeof(UnitUI))]
public class Unit : MonoBehaviour
{
    private string _playerName;
    private UnitMovemet _movemet;
    private SwitchCamera _switchCamera;
    private UnitHealth _health;
    [SerializeField] private int _playerID;
    [SerializeField] private int _unitID;
    private PlayerController _player;
    private void Awake()
    {
        _movemet = GetComponent<UnitMovemet>();
        _switchCamera = GetComponent<SwitchCamera>();
        _health = GetComponent<UnitHealth>();
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
    public void SetActive(int player, int unit)
    {
        bool active = _playerID == player && unit == _unitID;

        _movemet.ChangeActiveState(active);
        _switchCamera.ChangeActiveState(active);
        _health.ChangeActiveState(active);
    }

    public void SetActive(bool active)
    {
        _movemet.ChangeActiveState(active);
        _switchCamera.ChangeActiveState(active);
        _health.ChangeActiveState(active);
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
    private void OnDestroy()
    {
        Dead();
    }
    public void Dead()
    {
        _player.UnitDead(this);
        Debug.Log($"{gameObject.name} died");
    }
}