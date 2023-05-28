using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager _instance;

    [SerializeField]
    private float boundaryLeft;
    [SerializeField]
    private float boundaryRight;
    [SerializeField]
    private float boundaryTop;
    [SerializeField]
    private float boundaryBottom;

    private List<float> boundary;

    //±ß½çlist
    public List<float> Boundary { get => boundary; set => boundary = value; }

    private void Awake()
    {
        _instance = this;
        boundary = new List<float>();
        boundary.Add(boundaryLeft);
        boundary.Add(boundaryRight);
        boundary.Add(boundaryTop);
        boundary.Add(boundaryBottom);
        
    }

}
