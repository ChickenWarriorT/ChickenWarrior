using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private float insideRadius;
    [SerializeField]
    private float outsideRadius;
    [SerializeField]
    private float spawnCD;
    [SerializeField]
    private float spawnOffSet;
    private float outRadiusStep = 1.0f;
    int count = 0;

    private float time = 1.0f;

    private void Awake()
    {
    }
    private void Start()
    {

        //time = spawnCD;
    }

    private void Update()
    {
        //time -= Time.deltaTime;
        //if (time <= 0.0f)
        //{
        //    SpawnInRandomInPosition(insideRadius, outsideRadius);
        //    time = spawnCD;
        //}


        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("生成怪物。。。。。。");
            SpawnInRandomInPosition(MonsterType.SkullReaper, insideRadius, outsideRadius);
            time = spawnCD;
        }
        //按R生成追踪怪物
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("生成怪物。。。。。。");
            for (int i = 0; i < 100; i++)
            {
                SpawnInRecPosition(MonsterType.SkullReaper, insideRadius, outsideRadius);
            }
            print("怪物数量--------------：" + count);
            count = 0;
            time = spawnCD;
        }
        //按R生成反射怪物
        if (Input.GetKeyDown(KeyCode.T))
        {
            print("生成怪物。。。。。。");
            for (int i = 0; i < 100; i++)
            {
                SpawnInRecPosition(MonsterType.Monster02, insideRadius, outsideRadius);
            }
            print("怪物数量--------------：" + count);
            count = 0;
            time = spawnCD;
        }
    }

    //随机生成怪物，在以自身为圆心，大于inRadius半径的圆，小于outRadius半径的圆的区域内
    public void SpawnInRandomInPosition(MonsterType type,float inRadius, float outRadius)
    {

        Vector2 p = Random.insideUnitCircle * outRadius;
        Vector2 pos = p.normalized * (inRadius + p.magnitude);
        Vector2 relatePos = pos + (Vector2)PlayerManager._instance.player.transform.position;

        //Monster e = Instantiate(type, relatePos, Quaternion.identity).GetComponent<Monster>();
        Monster monster = ObjectPoolManager._instance.GetMonster(type);
        MonsterManager._instance.monsters.Add(monster);
        monster.Init(relatePos);
        monster.gameObject.SetActive(true);

    }

    //随机生成怪物，在以自身为圆心，大于inRadius半径的圆，小于outRadius半径的圆的区域内，且在一个固定范围内
    Vector2 randomPos;
    public void SpawnInRecPosition(MonsterType type,float inRadius, float outRadius)
    {

        List<float> boundary = MapManager._instance.Boundary;

        bool isValidPosition = false;

        int maxAttempts = 100;
        int attemptCount = 0;
        Vector2 playerPos = PlayerManager._instance.player.transform.position;

        //尝试找到合适位置
        while (!isValidPosition && attemptCount < maxAttempts)
        {
            float randomX = Random.Range(boundary[0] + spawnOffSet, boundary[1] - spawnOffSet);
            float randomY = Random.Range(boundary[3] + spawnOffSet, boundary[2] - spawnOffSet);
            randomPos = new Vector2(randomX, randomY);

            if (inRadius <= Vector2.Distance(randomPos, playerPos)
                && Vector2.Distance(randomPos, playerPos) <= outRadius)
            {
                isValidPosition = true;

            }
            attemptCount++;
        }
        //如果多次尝试没有找到合适的位置，则增加外圈范围
        while (!isValidPosition)
        {
            //增加外圈范围
            outRadius += outRadiusStep; 

            float randomX = Random.Range(boundary[0] + spawnOffSet, boundary[1] - spawnOffSet);
            float randomY = Random.Range(boundary[3] + spawnOffSet, boundary[2] - spawnOffSet);
            randomPos = new Vector2(randomX, randomY);

            if (inRadius <= Vector2.Distance(randomPos, playerPos)
                && Vector2.Distance(randomPos, playerPos) <= outRadius)
            {
                isValidPosition = true;
            }
        }

        //Monster e = Instantiate(enemy, randomPos, Quaternion.identity).GetComponent<Monster>();
        Monster monster = ObjectPoolManager._instance.GetMonster(type);
        MonsterManager._instance.monsters.Add(monster);
        monster.Init(randomPos);
        monster.gameObject.SetActive(true);
        
        count++;
    }

}

