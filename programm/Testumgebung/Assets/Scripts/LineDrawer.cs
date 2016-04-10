using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    public float width = 1;
    // Use this for initialization
    void Start()
    {
        int childCount = transform.childCount;
        LineRenderer renderer = GetComponent<LineRenderer>();
        renderer.SetVertexCount(childCount );
        renderer.SetWidth(width, width);
        renderer.useWorldSpace = false;
        
        for (int i = 0; i < childCount; i++)
        {           
            GameObject point = transform.GetChild(i).gameObject;
            renderer.SetPosition(i, point.transform.localPosition);
        }
    }
}
