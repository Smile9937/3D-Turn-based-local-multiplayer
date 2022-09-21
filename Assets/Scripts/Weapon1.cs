using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Weapon1 : Weapon
{
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float speed;
    private List<GameObject> bullets = new List<GameObject>();
    private bool notEnoughBulletsInPool = true;
    public override void Shoot()
    {
        GameObject bullet = GetBullet();

        bullet.SetActive(true);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.AddForce(transform.up * speed, ForceMode.Impulse);
    }

    private GameObject GetBullet()
    {
        //Checks if there are bullets in the bullets array and returns one if it is active
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }
        //Spawns a new bullet if there are not enough in the pool and returns it
        if (notEnoughBulletsInPool)
        {
            GameObject currentBullet = Instantiate(_bulletPrefab.gameObject);
            currentBullet.SetActive(false);
            bullets.Add(currentBullet);
            return currentBullet;
        }
        return null;
    }
}
