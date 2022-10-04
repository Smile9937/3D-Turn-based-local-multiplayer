using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(UnitMovemet)), RequireComponent(typeof(UnitHealth))]
[RequireComponent(typeof(SwitchCamera)), RequireComponent(typeof(UnitUI))]
public class Unit : MonoBehaviour, IDestructible
{
    [SerializeField] private UnitUI _unitUI;
    [SerializeField] private WeaponSelectUI _weaponSelectUI;
    [SerializeField] private GameObject _mesh;
    private string _unitName;
    private UnitMovemet _movemet;
    private SwitchCamera _switchCamera;
    private PlayerController _player;

    private void Awake()
    {
        _movemet = GetComponent<UnitMovemet>();
        _switchCamera = GetComponent<SwitchCamera>();
    }

    public void SetMaterial(Material material)
    {
        _mesh.GetComponent<SkinnedMeshRenderer>().material = material;
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
        EventManager.turnEnd += TurnEnd;
        EventManager.pausePlayers += WaitForEndOfTurn;
    }
    private void OnDisable()
    {
        EventManager.turnEnd -= TurnEnd;
        EventManager.pausePlayers -= WaitForEndOfTurn;
    }
    private void WaitForEndOfTurn()
    {
        _movemet.ChangeActiveState(false);
        _weaponSelectUI.ChangeActiveState(false);
    }
    private void TurnEnd()
    {
        SetActive(false);
    }
    public void SetActive(bool active)
    {
        _movemet.ChangeActiveState(active);
        _switchCamera.ChangeActiveState(active);
        _weaponSelectUI.ChangeActiveState(active);

        if (active)
        {
            _mesh.gameObject.layer = 8;
        }
        else
        {
            _mesh.gameObject.layer = 7;
        }
    }
    public void SetPlayer(PlayerController player)
    {
        _player = player;
    }

    public void DoneWithTurn()
    {
        EventManager.InvokePlayerTurnOver();
    }
    public void Destroy()
    {
        _player.UnitDead(this);
        gameObject.SetActive(false);
    }
}