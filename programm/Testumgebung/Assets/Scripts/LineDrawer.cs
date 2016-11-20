using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    public float width = 1;
    public Color startColor;
    public Color endColor;
    public Material material;
    // Use this for initialization
    void Start()
    {
        LineRenderer renderer = gameObject.AddComponent<LineRenderer>();
        int childCount = transform.childCount;
        renderer.material = material;
        renderer.SetColors(startColor, endColor);
        renderer.SetVertexCount(childCount);
        renderer.SetWidth(width, width);
        renderer.useWorldSpace = false;
        
        for (int i = 0; i < childCount; i++)
        {           
            GameObject point = transform.GetChild(i).gameObject;
            renderer.SetPosition(i, point.transform.localPosition);
            point.GetComponentInChildren<Transform>().gameObject.SetActive(false);
        }
    }
}
