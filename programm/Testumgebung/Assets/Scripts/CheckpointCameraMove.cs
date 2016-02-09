using UnityEngine;
using System.Collections;

public class CheckpointCameraMove : MonoBehaviour {

    private bool active;

    public GameObject animatedObject;

    // Use this for initialization
    void Start ()
    {
        active = false;
	
	}

    public void setActiveGameObject(bool state)
    {
        active = state;
    }

    // Update is called once per frame
    void Update () {

        if (active)
        {
                print("Er war hier");
                animatedObject.GetComponent<PlayAnimation>().playAnimation();
                active = false;
        }
	}
}
