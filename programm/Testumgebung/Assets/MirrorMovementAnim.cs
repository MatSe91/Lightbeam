using UnityEngine;
using System.Collections;

public class MirrorMovementAnim : MonoBehaviour {

    public GameObject moveMirror;
	
	// Update is called once per frame
	void Update() {
        transform.position = moveMirror.transform.position;
	}
}
