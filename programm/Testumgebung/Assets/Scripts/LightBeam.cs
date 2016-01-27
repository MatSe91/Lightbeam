using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class LightBeam : MonoBehaviour {
    private LineRenderer lr;
    private Vector3 curPos;
    private Vector3 inDirection;
    private bool isActive;
    public string refTag; //tag it can reflect off.
  //  public int Distanz; //max distance for beam to travel.
    public int limit; // max reflections
    private int verti = 1; //segment handler don't touch.




    // Use this for initialization
    void Start () {
        lr = this.GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.SetWidth(0.2f, 0.2f);
        lr.useWorldSpace = true;
    }

    void EnableLineRenderer()
    {
        lr.enabled = true;
    }

    // Update is called once per frame
    void Update() {
        verti = 1;
        isActive = true;
        inDirection = transform.right;
        curPos = transform.position;
        lr.SetVertexCount(verti);
        lr.SetPosition(0, curPos);

        while (isActive)
        {
            verti++;
            RaycastHit hit;
            lr.SetVertexCount(verti);
            if (Physics.Raycast(curPos, inDirection, out hit))
            {
              // Debug.DrawRay(curPos, inDirection*40, Color.magenta);

               // Debug.Log("Richtung: " + inDirection);
                curPos = hit.point;
                inDirection = Vector3.Reflect(inDirection, hit.normal);
                lr.SetPosition(verti - 1, hit.point);
                if (hit.transform.gameObject.tag != refTag)
                {
                     isActive = false;
                }
                if (hit.transform.gameObject.tag == refTag)
                {
                    float angle = Vector3.Angle(inDirection, hit.normal);
                 //   Debug.DrawRay(curPos, hit.normal *3, Color.blue);

                //  Debug.Log("Game Object:"+ hit.transform.gameObject.name+", Winkel: " + angle);
                  //  Debug.Log("Die Normale: "+hit.normal);

                }
            }
            else
            {
                isActive = false;
                lr.SetPosition(verti - 1,curPos+ 40 * inDirection);

            }
            if (verti > limit)
            {
                isActive = false;
            }

        }
    }
}
