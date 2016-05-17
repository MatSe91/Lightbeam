using UnityEngine;
using System.Collections;

public class CheckPointManager : MonoBehaviour {

    private bool isBeamconntected;
    private bool isPhaseCompleted;
    private GameObject cam;
   

    public void Start()
    {
        isBeamconntected = false;
        isPhaseCompleted = false;
        cam = GameObject.Find("Main Camera");
    }

    /// <summary>
    /// Setze true oder false jenachdem ob der Lichtstrahl den Checkpoint berührt.
    /// </summary>
    /// <param name="bol"></param>
    public void setBeamConnectivity(bool bol)
    {
        isBeamconntected = bol;
    }

    /// <summary>
    /// Setze true oder false jenachdem ob die Phase (Phase 0, Phase 1) abgeschlossen ist.
    /// </summary>
    /// <param name="bol"></param>
    public void setPhaseCompleted(bool bol)
    {
        isPhaseCompleted = bol;
    }

    /// <summary>
    /// Bei Touch auf Checkpoint starte Animation
    /// </summary>
    public void setActiveGameObject()
    {
        if (isBeamconntected)
        {
            if (!isPhaseCompleted)
            {
                
                cam.GetComponent<CameraAnimationScript>().playAnimation(this.gameObject);
            }
        }

      
    }
}
