using UnityEngine;
using System.Collections;

public class CameraAnimationScript : MonoBehaviour {

    private Animator animate;
    private GameObject checkpoint;
    private int anim_index;

    void Awake()
    {
        animate = GetComponent<Animator>();
    }

    /// <summary>
    /// Spiele die Animation zur entsprechenden Phase ab
    /// </summary>
    /// <param name="checkP"></param>
    public void playAnimation(GameObject checkP)
    {
        checkpoint = checkP;

        if (anim_index == 0)
        {
            animate.SetTrigger("phase_2");
        }
        if (anim_index == 1)
        {
            animate.SetTrigger("phase_3");
        }
    }

    /// <summary>
    /// Aufruf nur durch die Animation selbst!
    /// Index, damit die 1. Phase also Bereich 1 abgeschlossen ist.
    /// </summary>
    public void isPhase2()
    {
        checkpoint.GetComponent<CheckPTouchCollider>().setPhaseCompleted(true);
        anim_index = 1;
    }

        public void enableBeam()
        {
            checkpoint.GetComponent<PlayerRotator>().enabled = true;
        }

    /// <summary>
    /// Aufruf nur durch die Animation selbst!
    /// Index, damit die 2. Phase also Bereich 2 abgeschlossen ist.
    /// </summary>
    public void isPhase3()
    {
        checkpoint.GetComponent<CheckPTouchCollider>().setPhaseCompleted(true);
    }
}