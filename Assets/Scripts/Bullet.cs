using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDestructible
{
    [SerializeField] private int damage;
    public int Damage() {
        return damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
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
