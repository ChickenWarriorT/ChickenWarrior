using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private float insideRadius;
    [SerializeField]
    private float outsideRadius;
    [SerializeField]
    private float spawnCD;
    [SerializeField]
    private float spawnOffSet;
    int count=0;

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
            SpawnInRandomInPosition(insideRadius, outsideRadius);
            time = spawnCD;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            print("生成怪物。。。。。。");
            SpawnInRecPosition(insideRadius, outsideRadius);
            
            print("怪物数量--------------："+count);
            count = 0;
            time = spawnCD;
        }
    }

    //随机生成怪物，在以自身为圆心，大于inRadius半径的圆，小于outRadius半径的圆的区域内
    public void SpawnInRandomInPosition(float inRadius, float outRadius)
    {

        Vector2 p = Random.insideUnitCircle * outRadius;
        Vector2 pos = p.normalized * (inRadius + p.magnitude);
        Vector2 relatePos = pos + (Vector2)PlayerManager._instance.player.transform.position;

        Enemy e = Instantiate(enemy, relatePos, Quaternion.identity);
        EnemyManager._instance.enemies.Add(e);

    }

    Vector2 randomPos;
    public void SpawnInRecPosition(float inRadius, float outRadius)
    {
        List<float> boundary = MapManager._instance.Boundary;

        bool isValidPosition = false;

        int maxAttempts = 100;
        int attemptCount = 0;
        Vector2 playerPos = PlayerManager._instance.player.transform.position;

        while (!isValidPosition && attemptCount < maxAttempts)
        {
            float randomX = Random.Range(boundary[0]+ spawnOffSet, boundary[1]- spawnOffSet);
            float randomY = Random.Range(boundary[3]+ spawnOffSet, boundary[2]- spawnOffSet);
            randomPos = new Vector2(randomX, randomY);

            if (inRadius <= Vector2.Distance(randomPos, playerPos)
                && Vector2.Distance(randomPos, playerPos) <= outRadius)
            {
                isValidPosition = true;
                count++;
            }
            attemptCount++;
        }

        if (isValidPosition)
        {
            Enemy e = Instantiate(enemy, randomPos, Quaternion.identity);
            EnemyManager._instance.enemies.Add(e);
        }

    }
}
