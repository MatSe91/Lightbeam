using UnityEngine;
using System.Collections;

public class MirrorRotatorAnim : MonoBehaviour {
	
	void Update() {
        transform.Rotate(-(Vector3.forward * Time.deltaTime*20));
    }
}
