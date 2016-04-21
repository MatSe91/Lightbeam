using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MirrorMovement : MonoBehaviour {


    private bool active = false;
    public GameObject Line;
    private List<GameObject> linePoints;
    public GameObject touchAnimGameObject;
    private int heldDown = 0;
    public int framesTillMovable = 8;
    //  public GameObject background;
    // Use this for initialization

    void Start()
    {
        linePoints = new List<GameObject>();
        int childCount = Line.transform.childCount;
        for (int i = 0; i <childCount ; i++)
        {
            linePoints.Add(Line.transform.GetChild(i).gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetMouseButton(0))
            {
                if (heldDown < framesTillMovable) heldDown++;
                else
                {
                    Vector3 nearPoint = FindNearPoint(GetWorldPositionOnPlane(Input.mousePosition, 0));
                    this.transform.position = nearPoint;
                }

            }
            else
            {
                heldDown = 0;
            }
        }
    }
	

    public void setActiveGameObject(bool mov)
    {
        active = mov;
        if (active)
        {
            if (touchAnimGameObject != null)
            {
                touchAnimGameObject.SetActive(true);
            }
        }
        else
        {
            touchAnimGameObject.SetActive(false);
        }
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

  
    public Vector3 FindNearPoint( Vector3 nearPoint)
    {
        float shortestDistance = 100f;
        GameObject index = linePoints.FirstOrDefault().gameObject;
        foreach (GameObject point in linePoints)
        {
            float distance = Vector3.Distance(point.transform.position, nearPoint);
            if(distance < shortestDistance)
            {
                index = point;
                shortestDistance = distance;
            }
        }
       
        return index.transform.position;//
   }

}
