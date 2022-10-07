using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private UnitUI _unitUI;
    [SerializeField] private GameObject _damageText;
    private int _health;
    private Unit _unit;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        DamageText _text = ObjectPoolManager.SpawnFromPool(_damageText, transform.position, transform.rotation).GetComponent<DamageText>();
        _text.SetText(damage);
        _unitUI.UpdateHealth(_health);
        if (_health <= 0)
        {
            _unit.Destroy();
        }
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public void TakeKnockback(Vector3 knockback)
    {
        _rigidbody.AddForce(knockback, ForceMode.Impulse);
    }
}