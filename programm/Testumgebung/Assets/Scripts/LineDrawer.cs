using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{

    // Use this for initialization
    private List<GameObject> linePoints;
    void Start()
    {
        linePoints = new List<GameObject>();
        int childCount = transform.childCount;
        LineRenderer renderer = GetComponent<LineRenderer>();
        renderer.SetVertexCount(childCount );
        renderer.SetWidth(0.2f, 0.2f);
        renderer.useWorldSpace = false;
        
        Vector3 origin = transform.position;
        for (int i = 0; i < childCount; i++)
        {
           
            GameObject point = transform.GetChild(i).gameObject;
            renderer.SetPosition(i, point.transform.localPosition);
           

        }





    }
}
	
	
    

