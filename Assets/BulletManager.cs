using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    public Bullet CreateBullet(Bullet bullet,Vector2 offset)
    {

        Bullet bult = (Bullet)Instantiate(bullet, (Vector2)PlayerManager._instance.player.transform.position+ offset, Quaternion.identity);
        return bult;
    }
}
