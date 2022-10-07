using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafWand : Weapon
{
    [SerializeField] private GameObject _leafStorm;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _attackDelay;
    
    public override void Shoot()
    {
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_attackDelay);

        RaycastHit hit;

        Physics.Raycast(_targetPosition.position, -transform.up, out hit, 2f, layerMask);

        if (hit.collider != null)
        {
            ObjectPoolManager.SpawnFromPool(_leafStorm, hit.point, _leafStorm.transform.rotation);
        }
        else
        {
            ObjectPoolManager.SpawnFromPool(_leafStorm, _targetPosition.position, _leafStorm.transform.rotation);
        }
    }
}