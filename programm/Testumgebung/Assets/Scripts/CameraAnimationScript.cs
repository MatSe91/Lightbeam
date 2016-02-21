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
            animate.SetBool("phase_2", true);
        }
        if (anim_index == 1)
        {
            animate.SetBool("phase_3", true);
        }
    }

    /// <summary>
    /// Aufruf nur in der Animation selbst!
    /// Index, damit die 1. Phase also Bereich 1 abgeschlossen ist.
    /// </summary>
    public void isPhase2()
    {
        checkpoint.GetComponent<CheckPTouchCollider>().setPhaseCompleted(true);
        anim_index = 1;
        animate.SetBool("phase_2", false);
    }

    /// <summary>
    /// Aufruf nur in der Animation selbst!
    /// Index, damit die 2. Phase also Bereich 2 abgeschlossen ist.
    /// </summary>
    public void isPhase3()
    {
        checkpoint.GetComponent<CheckPTouchCollider>().setPhaseCompleted(true);
        animate.SetBool("phase_3", false);
    }
}