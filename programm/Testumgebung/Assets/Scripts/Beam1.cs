using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer))]

public class Beam : MonoBehaviour {
    private LineRenderer lr;
    private Ray ray;
    private Transform goTransform;
    private Vector3 inDirection;
    private int lenghtOfLineRenderer = 2;
    private int reflections = 0;

    // Use this for initialization
    void Start () {
        lr = this.GetComponent<LineRenderer>();
        goTransform = this.GetComponent<Transform>();
        lr.enabled = true;
        lr.SetWidth(0.2f, 0.2f);
        lr.useWorldSpace = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        //clamp the number of reflections between 1 and int capacity
        reflections = Mathf.Clamp(reflections, 1, reflections);
        lenghtOfLineRenderer += reflections;
        ray = new Ray(goTransform.position, goTransform.right);

        //represent the ray using a line that can only be viewed at the scene tab
        Debug.DrawRay(goTransform.position, goTransform.right * 20, Color.magenta);

        // Wird etwas getroffen

        if (Physics.Raycast(transform.position, transform.right, out hit, 20))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                Wall(hit);
            }

            if (hit.transform.CompareTag("Mirror"))
            {
                Mirror(hit);
            }
        }
        else
        {
            lr.SetPosition(1, new Vector3(20, 0, 0));
        } 
       
	}

    private void Mirror(RaycastHit hit)
    {
        for (int i = 0; i <= reflections; i++)
        {
            //If the ray hasn't reflected yet
            if (i == 0)
            {
                //Check if the ray has hit something
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 20))//cast the ray 100 units at the specified direction  
                {
                    //the reflection direction is the reflection of the current ray direction flipped at the hit normal
                    inDirection = Vector3.Reflect(ray.direction, hit.normal);
                    //cast the reflected ray, using the hit point as the origin and the reflected direction as the direction
                    ray = new Ray(hit.point, inDirection);

                    //Draw the normal - only can be seen at the Scene tab for debugging purposes
                    Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);
                    //represent the ray using a line that can only be viewed at the scene tab
                    Debug.DrawRay(hit.point, inDirection * 20, Color.magenta);

                    //Print the name of the object the cast ray has hit, at the console
                    Debug.Log("Object name: " + hit.transform.name);

                    //if the number of reflections is set to 1
                    if (reflections == 1)
                    {
                        //add a new vertex to the line renderer
                        lr.SetVertexCount(++lenghtOfLineRenderer);
                    }

                    //set the position of the next vertex at the line renderer to be the same as the hit point
                    lr.SetPosition(i + 1, hit.point);
                }
                else
                    Wall(hit);

            }
            else // the ray has reflected at least once
            {
                //Check if the ray has hit something
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))//cast the ray 100 units at the specified direction  
                {
                    //the refletion direction is the reflection of the ray's direction at the hit normal
                    inDirection = Vector3.Reflect(inDirection, hit.normal);
                    //cast the reflected ray, using the hit point as the origin and the reflected direction as the direction
                    ray = new Ray(hit.point, inDirection);

                    //Draw the normal - only can be seen at the Scene tab for debugging purposes
                    Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);
                    //represent the ray using a line that can only be viewed at the scene tab
                    Debug.DrawRay(hit.point, inDirection * 20, Color.magenta);

                    //Print the name of the object the cast ray has hit, at the console
                    Debug.Log("Object name: " + hit.transform.name);

                    //add a new vertex to the line renderer
                    lr.SetVertexCount(++lenghtOfLineRenderer);
                    //set the position of the next vertex at the line renderer to be the same as the hit point
                    lr.SetPosition(i + 1, hit.point);
                }
                else
                {
                    Wall(hit);
                }

            }

        }
     }

    private void Wall(RaycastHit hit)
    {
        lr.SetPosition(lenghtOfLineRenderer-1, new Vector3(hit.distance, 0, 0));
    }
}
