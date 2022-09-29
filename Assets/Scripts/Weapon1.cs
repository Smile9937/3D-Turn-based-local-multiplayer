using System.Collections;
using UnityEngine;

public class Weapon1 : Weapon
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _speed;
    [SerializeField] private Unit _unit;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _attackCooldown;
    private bool _canAttack = true; 
    public override void Shoot()
    {
        if (!_canAttack) return;
        StartCoroutine(AttackDelay());
    }
    private IEnumerator AttackDelay()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);

        GameObject bullet = ObjectPoolManager.SpawnFromPool(_bullet, _spawnPoint.position, _unit.transform.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.AddForce(transform.up * _speed, ForceMode.Impulse);

        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
