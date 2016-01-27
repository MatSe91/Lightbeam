using UnityEngine;
using System.Collections;

public class MirrorRotator : MonoBehaviour {
    private bool rotatable;
    private Vector3 pos;
    private Vector3 dir;
    private float angle;

    // Use this for initialization
    void Start () {
        rotatable = false;
	
	}

    public void setRotatable(bool rot)  //müsste eigentlich setActive sein
    {
        rotatable = rot;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rotatable)
        {
            if (Input.GetMouseButton(0))
            { 
                
                pos = Camera.main.WorldToScreenPoint(transform.position);
                dir = Input.mousePosition - pos;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Debug.Log("Winkel ist: " + angle);
                transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
            }
        }


    }
}
