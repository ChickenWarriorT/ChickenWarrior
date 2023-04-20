using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager _instance;

    [SerializeField]
    private List<Enemy> enemies;
    private void Awake()
    {
        _instance = this;
    }

    public Enemy GetRandomEnemy()
    {
        if (enemies.Count > 0)
        {
            int r = Random.Range(0, enemies.Count);
            //int randomInt = 
        }
        return null;
    }
}
