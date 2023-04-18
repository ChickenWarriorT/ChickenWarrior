using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour
{
    [SerializeField]
    private int health;

    public int Value { get { return health; } }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
    }
}
