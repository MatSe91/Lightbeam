using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.touches[0];
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
