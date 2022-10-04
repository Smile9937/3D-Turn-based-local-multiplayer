using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField] private Canvas _weaponSelectCanvas;
    [SerializeField] private UnitMovemet _unitMovemet;
    private bool _UIOpen;
    private bool _active;

    [Serializable]
    public class UnitWeapon
    {
        public Weapon _weapon;
        public Button _weaponButton;
        public bool _unlocked;
    }
    public List <UnitWeapon> weapons;

    private void Start()
    {
        _weaponSelectCanvas.gameObject.SetActive(false);
    }
    public void ChangeActiveState(bool active)
    {
        _active = active;
    }
    public void ToggleUI(InputAction.CallbackContext context)
    {
        if (!_active) return;
        if(context.performed)
        {
            if(!_UIOpen)
            {
                OpenUI();
            }
            else
            {
                CloseUI();
            }
        }
    }

    private void OpenUI()
    {
        _unitMovemet.ToggleMovement(false);
        _weaponSelectCanvas.gameObject.SetActive(true);
        foreach(UnitWeapon weapon in weapons)
        {
            if(weapon._unlocked)
            {
                weapon._weaponButton.gameObject.SetActive(true);
            }
            else
            {
                weapon._weaponButton.gameObject.SetActive(false);
            }
        }
        _UIOpen = true;
    }
    private void CloseUI()
    {
        foreach (UnitWeapon weapon in weapons)
        {
            weapon._weaponButton.gameObject.SetActive(false);
        }
        _UIOpen = false;
        _unitMovemet.ToggleMovement(true);
        _weaponSelectCanvas.gameObject.SetActive(false);
    }

    public void ChooseWeapon(int index)
    {
        for(int i = 0;i < weapons.Count;i++)
        {
            if(i == index)
            {
                _unitMovemet.SwitchWeapon(weapons[i]._weapon);
            }
        }
        CloseUI();
    }
}
