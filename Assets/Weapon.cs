using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float speed;
    private List<GameObject> bullets = new List<GameObject>();
    private bool notEnoughBulletsInPool = true;
    public void Shoot()
    {
        //var bullet = Instantiate(_bulletPrefab, spawnPoint.position, Quaternion.identity);

        GameObject bullet = GetBullet();

        bullet.SetActive(true);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bulletRigidbody.velocity = Vector3.zero;

        float angle = Mathf.Abs(90 - transform.eulerAngles.x) * 1 / 90;
        Vector3 direction = Vector3.up * angle + Vector3.forward;

        bulletRigidbody.AddForce(direction * speed, ForceMode.Impulse);
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
