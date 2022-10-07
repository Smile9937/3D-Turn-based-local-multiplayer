using System.Collections;
using UnityEngine;

public class FireballWand : Weapon
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _speed;
    [SerializeField] private Unit _unit;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _attackDelay;
    public override void Shoot()
    {
        StartCoroutine(AttackDelay());
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_attackDelay);

        GameObject bullet = ObjectPoolManager.SpawnFromPool(_bullet, _spawnPoint.position, _unit.transform.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.AddForce(transform.up * _speed, ForceMode.Impulse);
    }
}