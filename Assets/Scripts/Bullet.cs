using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDestructible
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject _hitObject;
    public int Damage()
    {
        return damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ObjectPoolManager.SpawnFromPool(_hitObject, collision.contacts[0].point, transform.rotation);
        IDamageable damageTarget = collision.gameObject.GetComponent<IDamageable>();
        if(damageTarget != null)
        { 
            damageTarget.TakeDamage(Damage());
        }
        Destroy();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
