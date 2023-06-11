using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T pooledObject;
    public int pooledAmount = 10;

    private List<T> pooledObjects;
    private Transform poolParentTransform;

    public void Initialize(T prefab, int size, Transform parentTransform)
    {
        pooledObject = prefab;
        pooledAmount = size;
        poolParentTransform = parentTransform;

        pooledObjects = new List<T>();

        for (int i = 0; i < pooledAmount; i++)
        {
            T obj = GameObject.Instantiate(pooledObject, poolParentTransform);
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public T GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        T obj = GameObject.Instantiate(pooledObject, poolParentTransform);
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public void ReturePooledObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}