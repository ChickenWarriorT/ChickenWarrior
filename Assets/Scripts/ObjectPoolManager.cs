using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterPoolConfig {
    public MonsterType type;
    public GameObject prefab;
    public int initialSize;
}
[System.Serializable]
public class BulletPoolConfig
{
    public BulletType type;
    public GameObject prefab;
    public int initialSize;
}

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
    private Dictionary<MonsterType, ObjectPool<Monster>> monsterPools;
    private Dictionary<BulletType, ObjectPool<Bullet>> bulletPools;
    [SerializeField]
    private Transform poolTransform;

    [SerializeField]
    private List<MonsterPoolConfig> monsterConfigs;
    [SerializeField]
    private List<BulletPoolConfig> bulletConfigs;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        monsterPools = new Dictionary<MonsterType, ObjectPool<Monster>>();
        foreach (var config in monsterConfigs)
        {
            ObjectPool<Monster> pool = new ObjectPool<Monster>();
            pool.Initialize(config.prefab.GetComponent<Monster>(), config.initialSize, poolTransform);
            monsterPools[config.type] = pool;
        }

        bulletPools = new Dictionary<BulletType, ObjectPool<Bullet>>();
        foreach (var config in bulletConfigs)
        {
            ObjectPool<Bullet> pool = new ObjectPool<Bullet>();
            pool.Initialize(config.prefab.GetComponent<Bullet>(), config.initialSize, poolTransform);
            bulletPools[config.type] = pool;
        }
    }

    public Monster GetMonster(MonsterType type)
    {
        if (monsterPools.ContainsKey(type))
        {
            return monsterPools[type].GetPooledObject();
        }
        else
        {
            Debug.LogError("No object pool found for type " + type);
            return null;
        }
    }


    public Bullet GetBullet(BulletType type)
    {
        if (bulletPools.ContainsKey(type))
        {
            return bulletPools[type].GetPooledObject();
        }
        else
        {
            Debug.LogError("No object pool found for type " + type);
            return null;
        }
    }
}
