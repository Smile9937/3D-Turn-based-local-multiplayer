using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : Weapon
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _speed;
    [SerializeField] private Unit _unit;
    private ObjectPoolManager _objectPoolManager;
    private void Start()
    {
        _objectPoolManager = ObjectPoolManager.Instance;
    }
    public override void Shoot()
    {
        GameObject bullet = _objectPoolManager.SpawnFromPool(PoolObject.Bullet, _spawnPoint.position, _unit.transform.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.AddForce(transform.up * _speed, ForceMode.Impulse);
    }
}
