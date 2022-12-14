using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolManager
{
    private static Dictionary<GameObject, Queue<GameObject>> _poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    private static GameObject _poolParent;
    public static GameObject SpawnFromPool(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        if(_poolParent == null) {
            _poolDictionary.Clear();
            _poolParent = new GameObject(name: "Pool Parent");
        }
        if(!_poolDictionary.ContainsKey(gameObject))
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            _poolDictionary.Add(gameObject, objectPool);
        }
        
        if (_poolDictionary[gameObject].Count > 0 && !_poolDictionary[gameObject].Peek().activeInHierarchy)
        {
            GameObject objectToSpawn = _poolDictionary[gameObject].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);

            _poolDictionary[gameObject].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        else
        {
            GameObject objectToSpawn = Object.Instantiate(gameObject, position, rotation);
            _poolDictionary[gameObject].Enqueue(objectToSpawn);
            objectToSpawn.transform.SetParent(_poolParent.transform, false);

            return objectToSpawn;
        }
    }
}