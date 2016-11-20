using UnityEngine;
using System.Collections;
using System;

public class EndMenuManager : MonoBehaviour {

    // Use this for initialization

    private bool IsFadeIn = true;
    private bool inAnimation = false;
    private Transform endMenu;
    public Camera cam;

    public float smoothTime = 0.005F;
    private Vector3 velocity = Vector3.zero;

    public bool InAnimation
    {
        get
        {
            return inAnimation;
        }

        set
        {
            inAnimation = value;
        }
    }

    public bool isInAnimation()
    {
        return inAnimation = !inAnimation;
    }

    void Start()
    {
        endMenu = gameObject.transform.parent;
    }

    public void Fade ()
    {
        if (IsFadeIn)
        {
            gameObject.GetComponent<Animator>().Play("fadeOutEndMenu");
            IsFadeIn = false;
            cam.GetComponent<Animator>().enabled = false;
            cam.GetComponent<CameraMovement>().enabled = true;
        }

        else
        {
            gameObject.GetComponent<Animator>().Play("fadeInEndMenu");
            IsFadeIn = true;
            cam.GetComponent<Animator>().enabled = false;
            cam.GetComponent<CameraMovement>().enabled = false;
        }
    }

    void Update()
    {
        if (!IsFadeIn)
        {
            endMenu.position = new Vector3(cam.transform.position.x, 0, 0);            
        }
    }
}
