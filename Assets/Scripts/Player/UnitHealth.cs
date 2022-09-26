using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private UnitUI unitUI;
    private int health;
    private Unit unit;
    private void Awake()
    {
        unit = GetComponent<Unit>();
    }
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        unitUI.UpdateHealth(health);
        if (health <= 0)
        {
            unit.Destroy();
        }
    }
}