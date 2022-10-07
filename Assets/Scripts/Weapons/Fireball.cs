using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fireball : MonoBehaviour, IDestructible
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private GameObject _hitObject;
    [SerializeField] private Vector2 _knockback = new Vector2(3, 2);
    public int Damage()
    {
        return _damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ObjectPoolManager.SpawnFromPool(_hitObject, collision.contacts[0].point, transform.rotation);
        
        IDamageable damageTarget = collision.gameObject.GetComponent<IDamageable>();
        if(damageTarget != null)
        { 
            damageTarget.TakeDamage(Damage());
            damageTarget.TakeKnockback(transform.forward * _knockback.x + transform.up * _knockback.y);
        }
        Destroy();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
