using UnityEngine;
using System.Collections;

public class PlayerRotator : MonoBehaviour {
    private bool active;
    private Vector3 pos;
    private Vector3 dir;
    private float angle;
    public int framesTillMovable = 8;
    public GameObject lightBeam;
    public GameObject touchAnimGameObject;
    private int pressed = 0;


    // Use this for initialization
    void Start() {
        active = true;
        lightBeam.SetActive(true);
    }

    void setActiveGameObject(bool rot)
    {
        active = rot;
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

    void Update() {


        if (active)
        {
            if (Input.GetMouseButton(0))
            {
                if (pressed < framesTillMovable) pressed++;
                else
                {
                    // positioniert die Anzeige genau auf die Maus / Touch
                    pos = Camera.main.WorldToScreenPoint(transform.position);
                    dir = Input.mousePosition - pos;
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            else
                pressed = 0;
        }
    }
}
