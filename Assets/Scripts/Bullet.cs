using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDestructible
{
    [SerializeField] private int damage;
    [SerializeField] private PoolObject _hitParticle;
    private ObjectPoolManager _poolManager;
    private void Start()
    {
        _poolManager = ObjectPoolManager.Instance;
    }
    public int Damage()
    {
        return damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        _poolManager.SpawnFromPool(_hitParticle, collision.contacts[0].point, transform.rotation);
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
