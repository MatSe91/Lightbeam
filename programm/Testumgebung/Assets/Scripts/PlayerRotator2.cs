using UnityEngine;
using System.Collections;

public class PlayerRotator2 : MonoBehaviour {
    private bool rotatable;
    private Vector3 pos;
    private Vector3 dir;
    private float angle;
    public GameObject laser;


    // Use this for initialization
    void Start() {
        rotatable = false;
    }

    void Update() {

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!rotatable)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name == "Outer")
                    {
                        rotatable = true;
                        laser.SetActive(true);

                        // Debug.Log(hit.collider.gameObject.name);
                    }
                }
            }
            else
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
