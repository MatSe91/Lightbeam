using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

    private Animator animate;
    private bool completed = true;

    void Awake()
    {
        animate = GetComponent<Animator>();
    }

    public void playAnimation()
    {
        if (completed)
        {
             animate.SetBool("nextArea", true);

        }
        else
        {
            animate.SetBool("nextArea", false);
        }
    }


    public void isNextArea()
    {
        completed = !completed;
    }
}
