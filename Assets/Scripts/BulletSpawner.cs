using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Bullet SpawnBullet(BulletType type, Vector2 startPos, GameObject enemy, int attakeDamage)
    {
        Bullet bullet = ObjectPoolManager._instance.GetBullet(type);
        bullet.Init(startPos, enemy, attakeDamage);
        bullet.gameObject.SetActive(true);
        //Bullet bult = (Bullet)Instantiate(bullet, (Vector2)PlayerManager._instance.player.transform.position + offset, Quaternion.identity);
        return bullet;
    }
}
