using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PoolConfig<T>
//{
//    public T type;
//    public GameObject prefab;
//    public int initialSize;
//}
//[System.Serializable]
//public class MonsterPoolConfig: PoolConfig<string>
//{
//}
//[System.Serializable]
//public class BulletPoolConfig: PoolConfig<string>
//{
//}

//[CreateAssetMenu(fileName = "PoolConfig1", menuName = "ScriptableObjects/PoolConfig1", order = 1)]
//public class BulletPoolConfig1:ScriptableObject
//{
//    public BulletType type;
//    public Bullet prefab;
//    public int initialSize;
//}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager _instance;
    private Dictionary<string, BasePool<Monster>> monsterPools;
    private Dictionary<string, BasePool<Bullet>> bulletPools;
    [SerializeField]
    private Transform poolTransform;

    //[SerializeField]
    //private List<MonsterPoolConfig> monsterConfigs;
    //[SerializeField]
    //private List<BulletPoolConfig> bulletConfigs;

    private void Awake()
    {
        _instance = this;
    }
    //private void Start()
    //{
    //    monsterPools = new Dictionary<MonsterType, ObjectPoolTest<Monster>>();
    //    foreach (var config in monsterConfigs)
    //    {
    //        ObjectPoolTest<Monster> pool = new ObjectPoolTest<Monster>();
    //        pool.Initialize(config.prefab.GetComponent<Monster>(), config.initialSize, poolTransform);
    //        monsterPools[config.type] = pool;
    //    }

    //    bulletPools = new Dictionary<BulletType, ObjectPoolTest<Bullet>>();
    //    foreach (var config in bulletConfigs)
    //    {
    //        ObjectPoolTest<Bullet> pool = new ObjectPoolTest<Bullet>();
    //        pool.Initialize(config.prefab.GetComponent<Bullet>(), config.initialSize, poolTransform);
    //        bulletPools[config.type] = pool;
    //    }
    //}



    //public Monster GetMonster(MonsterType type)
    //{
    //    if (monsterPools.ContainsKey(type))
    //    {
    //        return monsterPools[type].GetPooledObject();
    //    }
    //    else
    //    {
    //        Debug.LogError("No object pool found for type " + type);
    //        return null;
    //    }
    //}


    //public Bullet GetBullet(BulletType type)
    //{
    //    if (bulletPools.ContainsKey(type))
    //    {
    //        return bulletPools[type].GetPooledObject();
    //    }
    //    else
    //    {
    //        Debug.LogError("No object pool found for type " + type);
    //        return null;
    //    }
    //}
    [SerializeField] List<BasePool<Component>> pools;

    private Dictionary<string, BasePool<Component>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, BasePool<Component>>();

        foreach (var pool in pools)
        {
            pool.Initialize();
            poolDictionary[pool.Prefab.name] = pool;
        }
    }

    public T Get<T>(string prefabName) where T : Component
    {
        if (poolDictionary.TryGetValue(prefabName, out BasePool<Component> pool))
        {
            return pool.Get() as T;
        }
        else
        {
            Debug.LogError("No object pool found for prefab " + prefabName);
            return null;
        }
    }

    public void Release<T>(T obj) where T : Component
    {
        string prefabName = obj.name.Replace("(Clone)", "").Trim();
        if (poolDictionary.TryGetValue(prefabName, out BasePool<Component> pool))
        {
            pool.Release(obj);
        }
        else
        {
            Debug.LogError("No object pool found for prefab " + prefabName);
        }
    }
}
