using UnityEngine;
using System.Collections;

public class PlayerRotator2 : MonoBehaviour {
    private Vector3 pos;
    private Vector3 dir;
    private float angle;

	// Use this for initialization
	void Start () {
        
	}

    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            // positioniert die Anzeige genau auf die Maus / Touch
            pos = Camera.main.WorldToScreenPoint(transform.position);
            dir = Input.mousePosition - pos;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

        
    }
}
