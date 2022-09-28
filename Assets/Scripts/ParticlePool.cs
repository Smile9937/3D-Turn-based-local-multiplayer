using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool
{
    private Stack<GameObject> _particlePool;
    private Transform _poolParent;
    private GameObject _particlePrefab;

    public ParticlePool(GameObject particlePrefab)
    {
        _poolParent = new GameObject("Particle Pool").GetComponent<Transform>();
        _particlePool = new Stack<GameObject>();
        _particlePrefab = particlePrefab;
    }

    public GameObject GetParticle()
    {
        var particle = _particlePool.Count > 0 ? _particlePool.Pop() : Object.Instantiate(_particlePrefab.gameObject, _poolParent);
        var particlePoolObject = particle.gameObject.GetComponent<ParticlePoolObject>();

        if(particlePoolObject == null)
        {
            particlePoolObject = particle.gameObject.AddComponent<ParticlePoolObject>();
            particlePoolObject.Init(this);
        }

        particle.transform.parent = null;
        particle.SetActive(true);
        return particle;
    }

    public void Return(GameObject particle)
    {
        particle.transform.parent = _poolParent;
        particle.SetActive(false);
        _particlePool.Push(particle);
    }
}
