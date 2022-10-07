using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] protected int _weaponIndex;
    public abstract void Shoot();

    private void OnEnable()
    {
        _meshRenderer.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        _meshRenderer.gameObject.SetActive(false);
    }
}