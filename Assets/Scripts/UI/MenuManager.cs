using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] private Canvas _controlsCanvas;
    [SerializeField] private Canvas _weaponSelectCanvas;
    [SerializeField] private List<Button> _weaponButtons;

    public enum MenuOpen
    {
        None,
        Settings,
        WeaponSelect
    }
    private MenuOpen _menuOpen;

    private static MenuManager instance;
    public static MenuManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _menuOpen = MenuOpen.None;
            DisablePauseMenu();
            DisableWeaponSelect();
            ToggleSettingsMenu(false);
            ToggleControlsMenu(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        EventManager.returnUnlockedWeapons += GetWeapons;
        EventManager.unitDead += UnitDead;
    }
    private void OnDisable()
    {
        EventManager.returnUnlockedWeapons -= GetWeapons;
        EventManager.unitDead -= UnitDead;
    }
    public void PauseMenu(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            TogglePauseMenu();
        }
    }
    public void TogglePauseMenu()
    {
        switch(_menuOpen)
        {
            case MenuOpen.None:
                _menuOpen = MenuOpen.Settings;
                OpenPauseMenu();
                break;
            case MenuOpen.Settings:
                _menuOpen = MenuOpen.None;
                DisablePauseMenu();
                break;
            case MenuOpen.WeaponSelect:
                DisableWeaponSelect();
                break;
        }
    }

    private void OpenPauseMenu()
    {
        _pauseMenuCanvas.enabled = true;
        EventManager.InvokeOpenMenu(true);
    }
    private void DisablePauseMenu()
    {
        _pauseMenuCanvas.enabled = false;
        EventManager.InvokeOpenMenu(false);
    }
    private void UnitDead(bool active)
    {
        DisableWeaponSelect();
    }
    private void DisableWeaponSelect()
    {
        _weaponSelectCanvas.enabled = false;
        EventManager.InvokeOpenMenu(false);
    }
    private void OpenWeaponSelect()
    {
        _weaponSelectCanvas.enabled = true;
        EventManager.InvokeGetUnlockedWeapons();
        EventManager.InvokeOpenMenu(true);
    }
    private void GetWeapons(List<int> _weapons)
    {
        for(int i = 0; i < _weaponButtons.Count; i++)
        {
            _weaponButtons[i].gameObject.SetActive(_weapons.Contains(i));
        }
    }
    public void ToggleSettingsMenu(bool enabled)
    {
        _pauseMenuCanvas.enabled = enabled;
    }
    public void ToggleControlsMenu(bool enabled)
    {
        _pauseMenuCanvas.enabled = enabled;
    }
    public void ToggleWeaponSelectUI(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (_menuOpen)
            {
                case MenuOpen.None:
                    _menuOpen = MenuOpen.WeaponSelect;
                    OpenWeaponSelect();
                    break;
                case MenuOpen.WeaponSelect:
                    _menuOpen = MenuOpen.None;
                    DisableWeaponSelect();
                    break;
            }
        }
    }
    public void ChooseWeapon(int weapon)
    {
        EventManager.InvokeEquipWeapon(weapon);
        DisableWeaponSelect();
    }

    public void Back()
    {
        _menuOpen = MenuOpen.None;
        DisablePauseMenu();
    }

    public void MainMenu()
    {
        GameSceneManager.GoToScene(Scene.MainMenu);
    }
}