using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolObject : MonoBehaviour
{
    private ParticlePool _particlePool;
    public void Init(ParticlePool particlePool)
    {
        _particlePool = particlePool;
    }
    private void OnDisable()
    {
        _particlePool.Return(gameObject);
    }
}
