using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //int = number of players
    public UnityEvent<int> StartGame = new UnityEvent<int>();


    //Events

    #region Change Active Player
    public delegate void ChangeActivePlayer(int player);
    public event ChangeActivePlayer changeActivePlayer;
    public void InvokeChangeActivePlayer(int player)
    {
        changeActivePlayer?.Invoke(player);
    }
    #endregion

    #region Unit Dead
    public delegate void UnitDead();
    public event UnitDead unitDead;
    public void InvokeUnitDead()
    {
        unitDead?.Invoke();
    }
    #endregion

    #region Player Lost
    public delegate void PlayerLost(int playerID);
    public event PlayerLost playerLost;
    public void InvokePlayerLost(int playerID)
    {
        playerLost?.Invoke(playerID);
    }
    #endregion

    #region Turn End
    public delegate void TurnEnd();
    public event TurnEnd turnEnd;
    public void InvokeTurnEnd()
    {
        turnEnd?.Invoke();
    }
    #endregion

    #region Set Active Camera
    public delegate void ActiveCamera(Camera camera);
    public event ActiveCamera setActiveCamera;
    public void InvokeActiveCamera(Camera camera)
    {
        setActiveCamera?.Invoke(camera);
    }
    #endregion
}