using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : BaseSpawner<Bullet,BulletPool>
{


    public Bullet SpawnBullet(string prefabName, Vector2 startPos, GameObject enemy, int attakeDamage)
    {
        Bullet bullet = objectPools[prefabName].Get();
        bullet.Init(startPos, enemy, attakeDamage);

        return bullet;
    }
}
