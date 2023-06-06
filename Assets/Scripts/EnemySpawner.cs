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
            print("���ɹ������������");
            SpawnInRandomInPosition(insideRadius, outsideRadius);
            time = spawnCD;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            print("���ɹ������������");
            for (int i = 0; i < 100; i++)
            {
                SpawnInRecPosition(insideRadius, outsideRadius);
            }
            

            print("��������--------------��" + count);
            count = 0;
            time = spawnCD;
        }
    }

    //������ɹ����������ΪԲ�ģ�����inRadius�뾶��Բ��С��outRadius�뾶��Բ��������
    public void SpawnInRandomInPosition(float inRadius, float outRadius)
    {

        Vector2 p = Random.insideUnitCircle * outRadius;
        Vector2 pos = p.normalized * (inRadius + p.magnitude);
        Vector2 relatePos = pos + (Vector2)PlayerManager._instance.player.transform.position;

        Enemy e = Instantiate(enemy, relatePos, Quaternion.identity);
        EnemyManager._instance.enemies.Add(e);

    }

    //������ɹ����������ΪԲ�ģ�����inRadius�뾶��Բ��С��outRadius�뾶��Բ�������ڣ�����һ���̶���Χ��
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
        //�����γ���û���ҵ����ʵ�λ�ã���������Ȧ��Χ
        while (!isValidPosition)
        {
            //������Ȧ��Χ
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

        Enemy e = Instantiate(enemy, randomPos, Quaternion.identity);
        EnemyManager._instance.enemies.Add(e);
        count++;
    }

}

