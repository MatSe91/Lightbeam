using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        int childCount = transform.childCount;
        LineRenderer renderer = GetComponent<LineRenderer>();
        renderer.SetVertexCount(childCount );
        renderer.SetWidth(0.2f, 0.2f);
        renderer.useWorldSpace = false;
        
        for (int i = 0; i < childCount; i++)
        {           
            GameObject point = transform.GetChild(i).gameObject;
            renderer.SetPosition(i, point.transform.localPosition);
        }
    }
}
