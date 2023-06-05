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

    public Vector2 PosRestrainInBoundary(Vector2 pos)
    {
        if (boundary != null)
        {
            float clampedX = Mathf.Clamp(pos.x, boundary[0], boundary[1]);
            float clampedY = Mathf.Clamp(pos.y, boundary[3], boundary[2]);

            return new Vector2(clampedX, clampedY);
        }

        return Vector2.zero;
    }

    public bool IsInsideBoundary(Vector2 pos)
    {


        return false;
    }

}
