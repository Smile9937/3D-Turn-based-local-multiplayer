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
    public delegate void ChangeActivePlayer(int player, int unit);
    public event ChangeActivePlayer changeActivePlayer;
    public void InvokeChangeActivePlayer(int player, int unit)
    {
        changeActivePlayer?.Invoke(player, unit);
    }
    #endregion

    #region Unit Dead
    public delegate void UnitDead(Unit unit);
    public event UnitDead unitDead;
    public void InvokeUnitDead(Unit unit)
    {
        unitDead?.Invoke(unit);
    }
    #endregion
}