using UnityEngine;
using System.Collections;

public class CheckPTouchCollider : MonoBehaviour {

    private bool isBeamconntected;
    private bool isPhaseCompleted;
    private GameObject touchCollider;
    private GameObject cam;

    public void Start()
    {
        isBeamconntected = false;
        isPhaseCompleted = false;
        touchCollider = transform.GetChild(0).gameObject;
        cam = GameObject.Find("Main Camera");
    }

    /// <summary>
    /// Setze true oder false jenachdem ob der Lichtstrahl den Checkpoint berührt.
    /// </summary>
    /// <param name="bol"></param>
    public void setBeamConnectivity(bool bol)
    {
        isBeamconntected = bol;
        setTouchCollider();
    }

    /// <summary>
    /// Setze true oder false jenachdem ob die Phase (Phase 0, Phase 1) abgeschlossen ist.
    /// </summary>
    /// <param name="bol"></param>
    public void setPhaseCompleted(bool bol)
    {
        isPhaseCompleted = bol;
        setTouchCollider();
    }

    /// <summary>
    /// aktiviert / deaktiviert den eigentlichen TouchCollider vom Checkpoint
    /// </summary>
    private void setTouchCollider()
    {
        
        if (!isPhaseCompleted)
        {
            if (isBeamconntected)
            {
                touchCollider.SetActive(true);
            }
            else
            {
                touchCollider.SetActive(false);
            }

        }
        else
        {
            touchCollider.SetActive(false);
        }
    }

    /// <summary>
    /// Bei Touch auf Checkpoint starte Animation
    /// </summary>
    public void setActiveGameObject()
    {

        if (isPhaseCompleted)
        {
            cam.GetComponent<CameraAnimationScript>().playAnimation(this.gameObject);
        }
        else
        {
            print("Do something else");
        }


    }


}
