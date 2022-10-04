using System.Collections.Generic;
using UnityEngine;
using static UIManager;

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
    public delegate void UnitDead();
    public static event UnitDead unitDead;
    public static void InvokeUnitDead()
    {
        unitDead?.Invoke();
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

}