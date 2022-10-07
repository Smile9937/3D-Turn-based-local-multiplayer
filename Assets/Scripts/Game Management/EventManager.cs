using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    #region Start Game
    public delegate void StartGame(int numberOfPlayers);
    public static event StartGame startGame;
    public static void InvokeStartGame(int numberOfPlayers)
    {
        startGame?.Invoke(numberOfPlayers);
    }
    #endregion

    #region Change Active Player
    public delegate void ChangeActivePlayer(int player);
    public static event ChangeActivePlayer changeActivePlayer;
    public static void InvokeChangeActivePlayer(int player)
    {
        changeActivePlayer?.Invoke(player);
    }
    #endregion

    #region Unit Dead
    public delegate void UnitDead(bool active);
    public static event UnitDead unitDead;
    public static void InvokeUnitDead(bool active)
    {
        unitDead?.Invoke(active);
    }
    #endregion

    #region Player Lost
    public delegate void PlayerLost(int playerID);
    public static event PlayerLost playerLost;
    public static void InvokePlayerLost(int playerID)
    {
        playerLost?.Invoke(playerID);
    }
    #endregion

    #region Turn End
    public delegate void TurnEnd();
    public static event TurnEnd turnEnd;
    public static void InvokeTurnEnd()
    {
        turnEnd?.Invoke();
    }
    #endregion

    #region Set Active Camera
    public delegate void ActiveCamera(Camera camera);
    public static event ActiveCamera setActiveCamera;
    public static void InvokeActiveCamera(Camera camera)
    {
        setActiveCamera?.Invoke(camera);
    }
    #endregion

    #region Set Players
    public delegate void SetPlayers(int numberOfPlayers, int numberOfUnits);
    public static event SetPlayers setPlayers;
    public static void InvokeSetPlayers(int numberOfPlayers, int numberOfUnits)
    {
        setPlayers?.Invoke(numberOfPlayers, numberOfUnits);
    }
    #endregion

    #region Enter Game
    public delegate void EnterGame(int level, List<PlayerStats>playerStats);
    public static event EnterGame enterGame;
    public static void InvokeEnterGame(int level, List<PlayerStats> playerStats)
    {
        enterGame?.Invoke(level, playerStats);
    }
    #endregion

    #region PlayerTurnOver
    public delegate void PlayerTurnOver();
    public static event PlayerTurnOver playerTurnOver;
    public static void InvokePlayerTurnOver()
    {
        playerTurnOver?.Invoke();
    }
    #endregion

    #region PausePlayers
    public delegate void PausePlayers();
    public static event PausePlayers pausePlayers;
    public static void InvokePausePlayers()
    {
        pausePlayers?.Invoke();
    }
    #endregion
    
    #region DeactivatePlayerCameras
    public delegate void DeactivatePlayerCameras();
    public static event DeactivatePlayerCameras deactivatePlayerCameras;
    public static void InvokeDeactivatePlayerCameras()
    {
        deactivatePlayerCameras?.Invoke();
    }
    #endregion

    #region GetCameras
    public delegate void GetCameras();
    public static event GetCameras getCameras;
    public static void InvokeGetCameras()
    {
        getCameras?.Invoke();
    }
    #endregion
    
    #region OpenMenu
    public delegate void OpenMenu(bool open);
    public static event OpenMenu openMenu;
    public static void InvokeOpenMenu(bool open)
    {
        openMenu?.Invoke(open);
    }
    #endregion

    #region OpenWeaponSelect
    public delegate void OpenWeaponSelect(bool open);
    public static event OpenWeaponSelect openWeaponSelect;
    public static void InvokeOpenWeaponSelect(bool open)
    {
        openWeaponSelect?.Invoke(open);
    }
    #endregion
    
    #region GetUnlockedWeapons
    public delegate void GetUnlockedWeapons(ReturnUnlockedWeapons eventCallback);
    public static event GetUnlockedWeapons getUnlockedWeapons;
    public static void InvokeGetUnlockedWeapons()
    {
        getUnlockedWeapons?.Invoke(returnUnlockedWeapons);
    }
    #endregion

    #region ReturnUnlockedWeapons
    public delegate void ReturnUnlockedWeapons(List<int> _weapons);
    public static event ReturnUnlockedWeapons returnUnlockedWeapons;
    #endregion

    #region EquipWeapon
    public delegate void EquipWeapon(int weapon);
    public static event EquipWeapon equipWeapon;
    public static void InvokeEquipWeapon(int weapon)
    {
        equipWeapon?.Invoke(weapon);
    }
    #endregion

    #region ActiveCameraChanged
    public delegate void ActiveCameraChanged(Camera camera);
    public static event ActiveCameraChanged activeCameraChanged;
    public static void InvokeActiveCameraChanged(Camera camera)
    {
        activeCameraChanged?.Invoke(camera);
    }
    #endregion
}