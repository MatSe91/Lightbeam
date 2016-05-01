using UnityEngine;
using System.Collections;

public class EndMenuManager : MonoBehaviour {

    // Use this for initialization

    private bool IsFadeIn =true;
    public Canvas EndMenu;
    public Camera cam;

    public void Fade ()
    {
        if (IsFadeIn)
        {
            EndMenu.GetComponent<Animator>().Play("fadeOutEndMenu");
            IsFadeIn = false;
            cam.GetComponent<Animator>().enabled = false;
            cam.GetComponent<CameraMovement>().enabled = true;
        }

        else
        {
            EndMenu.GetComponent<Animator>().Play("fadeInEndMenu");
            IsFadeIn = true;
            cam.GetComponent<Animator>().enabled = false;
            cam.GetComponent<CameraMovement>().enabled = false;
        }
    }

    }
