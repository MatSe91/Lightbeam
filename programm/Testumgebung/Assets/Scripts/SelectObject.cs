using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {
    private bool activated;

	// Use this for initialization
	void Start () {
        activated = false;
	
	}


    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Outer")
                {
                    if (!activated)
                    {
                        // hier muss iwie der rotator und linerenderer gestartet werden
                        activated = true;
                    }
                   Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
}
