using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : BaseSpawner<Bullet,BulletPool>
{

    // Start is called before the first frame update
    public Bullet SpawnBullet(string prefabName, Vector2 startPos, GameObject enemy, int attakeDamage)
    {
        Bullet bullet = objectPools[prefabName].Get();
        bullet.Init(startPos, enemy, attakeDamage);
        //bullet.gameObject.SetActive(true);
        //Bullet bult = (Bullet)Instantiate(bullet, (Vector2)PlayerManager._instance.player.transform.position + offset, Quaternion.identity);
        return bullet;
    }
}
