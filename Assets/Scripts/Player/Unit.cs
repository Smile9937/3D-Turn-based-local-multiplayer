using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(UnitMovemet)), RequireComponent(typeof(UnitHealth))]
[RequireComponent(typeof(SwitchCamera)), RequireComponent(typeof(UnitUI))]
public class Unit : MonoBehaviour, IDestructible
{
    [SerializeField] private UnitUI _unitUI;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private GameObject _deathParticles;

    private string _unitName;
    private UnitMovemet _movemet;
    private SwitchCamera _switchCamera;
    private PlayerController _player;

    private bool _activeUnit;
    private UnitState _currentState;
    private UnitHealth _health;

    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private List<int> _unlockedWeaponsList;

    private void Awake()
    {
        _movemet = GetComponent<UnitMovemet>();
        _switchCamera = GetComponent<SwitchCamera>();
        _health = GetComponent<UnitHealth>();
    }
    private void Start()
    {
        SetState(UnitState.Idle);
    }

    public void SetState(UnitState state)
    {
        _currentState = state;
    }
    public UnitState GetState()
    {
        return _currentState;
    }
    public void SetMaterial(Material material)
    {
        _mesh.GetComponent<SkinnedMeshRenderer>().material = material;
    }
    public void SetName(string name)
    {
        _unitName = name;
        _unitUI.SetStats(name, _health.GetMaxHealth());
    }
    public string GetName()
    {
        return _unitName;
    }
    private void OnEnable()
    {
        EventManager.turnEnd += TurnEnd;
        EventManager.pausePlayers += WaitForEndOfTurn;
        EventManager.openMenu += MenuOpen;
        EventManager.getUnlockedWeapons += ReturnUnlockedWeapons;
    }
    private void OnDisable()
    {
        EventManager.turnEnd -= TurnEnd;
        EventManager.pausePlayers -= WaitForEndOfTurn;
        EventManager.openMenu -= MenuOpen;
    }
    private void WaitForEndOfTurn()
    {
        SetState(UnitState.WaitingOnTurnEnd);
    }
    private void TurnEnd()
    {
        SetActive(false);
    }
    public void SetActive(bool active)
    {
        _activeUnit = active;
        _movemet.ChangeActiveState(_activeUnit);
        _switchCamera.ChangeActiveState(_activeUnit);

        if (_activeUnit)
        {
            OverlayManager.Instance.UpdatePlayerTurnText(_unitName);
            SetState(UnitState.ActiveTurn);
            _mesh.gameObject.layer = 8;
        }
        else
        {
            SetState(UnitState.Idle);
            _mesh.gameObject.layer = 7;
        }
    }
    public void MenuOpen(bool open)
    {
        if (!_activeUnit) return;

        if(open)
        {
            SetState(UnitState.InMenu);
            _movemet.ResetMovemet();
        }
        else
        {
            SetState(UnitState.ActiveTurn);
        }
    }
    public void SetPlayer(PlayerController player)
    {
        _player = player;
    }

    public void DoneWithTurn()
    {
        SetState(UnitState.WaitingOnTurnEnd);
        EventManager.InvokePlayerTurnOver();
    }
    public void Destroy()
    {
        _currentState = UnitState.Dead;
        _player.UnitDead(this);
        gameObject.SetActive(false);
        ObjectPoolManager.SpawnFromPool(_deathParticles, transform.position, transform.rotation);
    }

    private void ReturnUnlockedWeapons(EventManager.ReturnUnlockedWeapons eventCallback)
    {
        if(_currentState != UnitState.ActiveTurn) return;
        eventCallback?.Invoke(_unlockedWeaponsList);
    }
    public List<Weapon> GetWeapons()
    {
        return _weaponList;
    }
}