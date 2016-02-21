using UnityEngine;
using System.Collections;

public class PlayerRotator : MonoBehaviour {
    private bool active;
    private Vector3 pos;
    private Vector3 dir;
    private float angle;
    public GameObject lightBeam;


    // Use this for initialization
    void Start() {
        active = true;
        lightBeam.GetComponent<LightBeam>().enabled = true;
       //ightBeam.SendMessage("setBeamActive", true);

    }

    void setActiveGameObject(bool rot)
    {
        active = rot;
    }

    void Update() {

       if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse0))
       {
         if (active)
           {
                // positioniert die Anzeige genau auf die Maus / Touch
                pos = Camera.main.WorldToScreenPoint(transform.position);
                dir = Input.mousePosition - pos;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }
        }
    }
}
