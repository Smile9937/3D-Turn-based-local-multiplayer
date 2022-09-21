using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int health;
    private bool _isActivePlayer;
    private Unit unit;
    private void Awake()
    {
        unit = GetComponent<Unit>();
    }
    private void Start()
    {
        health = maxHealth;
    }
    public void ChangeActiveState(bool isActive)
    {
        _isActivePlayer = isActive;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            unit.Dead();
        }
    }
}