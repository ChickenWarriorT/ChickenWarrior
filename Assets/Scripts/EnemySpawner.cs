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

    private float time=1.0f;

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
            print("Éú³É¹ÖÎï¡£¡£¡£¡£¡£¡£");
            SpawnInRandomInPosition(insideRadius, outsideRadius);
            time = spawnCD;
        }
    }

    public void SpawnInRandomInPosition(float inRadius, float outRadius)
    {

        Vector2 p = Random.insideUnitCircle * outsideRadius;
        Vector2 pos = p.normalized * (inRadius + p.magnitude);
        Vector2 relatePos = pos + (Vector2)PlayerManager._instance.player.transform.position;

        //Instantiate(enemy, (Vector2)PlayerManager._instance.player.transform.position , Quaternion.identity);
        Enemy e = Instantiate(enemy, relatePos, Quaternion.identity);
        EnemyManager._instance.enemies.Add(e);

    }
}
