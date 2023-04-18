using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;
    public static PlayerManager _instance;


    private void Awake()
    {
        _instance = this;
    }
    public Vector2 PlayerPosition
    {
        get
        {
            return player.transform.position;
        }
    }
}
