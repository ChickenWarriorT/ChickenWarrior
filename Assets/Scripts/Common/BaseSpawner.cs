using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner<T, U> : MonoBehaviour where T : MonoBehaviour where U : BasePool<T>
{
    [SerializeField]
    protected T[] objectPrefabs;
    //protected List<U> objectPools = new List<U>();
    protected Dictionary<string, U> objectPools = new Dictionary<string, U>();

    protected void Start()
    {
        InitializePools();
    }


    protected void InitializePools()
    {
        foreach (var prefab in objectPrefabs)
        {
            var poolHolder = new GameObject($"Pool:{prefab.name}");

            poolHolder.transform.parent = transform;
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<U>();
            pool.Prefab = prefab;
            objectPools.Add(prefab.name,pool);

            poolHolder.SetActive(true);
        }
    }
}
