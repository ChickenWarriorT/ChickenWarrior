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

}
