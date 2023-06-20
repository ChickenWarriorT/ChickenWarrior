using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
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

    public Player Player { get => player;  }
}
