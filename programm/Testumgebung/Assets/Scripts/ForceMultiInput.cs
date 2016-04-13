using UnityEngine;
using System.Collections;

public class ForceMultiInput : MonoBehaviour {

    [Tooltip("Wenn enabled, dann Multi Input erlaubt")]
    public bool multiInpuEnabled;

	// Use this for initialization
	void Start () {
        Input.multiTouchEnabled = multiInpuEnabled;
    }
}
