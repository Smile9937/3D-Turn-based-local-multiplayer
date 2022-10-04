using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject _particle;
    private void OnTriggerEnter(Collider other)
    {
        ObjectPoolManager.SpawnFromPool(_particle, new Vector3(other.transform.position.x, transform.position.y + 0.1f, other.transform.position.z), _particle.transform.rotation);
    }
}
