using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class Level01_TUT_Manager : TUT_Manager  {

   public TUT_Manager tutManage;

    void Start()
    {
        activate(this, "TUT_0", 0);
    }

    void Update()
    {
        if (LevelManager.GameStarted)
        {
            if (!Tut_1)
            {
                TUT_1("TUT_1", 0);
                Tut_1 = true;
            }
        }
    }


    public void TUT_0(string name, int phase)
    {
        if (phase == (int)TUT_Manager.phasen.initializePhase)
        {
            tutManage.Manager.enabled = false;
            tutManage.getTUT(name).SetActive(true);
            tutManage.getTUT(name).GetComponent<Animator>().SetBool(name + "_Phase_" + phase, true);
        }
        if (phase == (int)TUT_Manager.phasen.firstPhase)
        {
            tutManage.getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }
        if (phase == (int)TUT_Manager.phasen.secondPhase)
        {
            tutManage.getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }
        if (phase == (int)TUT_Manager.phasen.thirdPhase)
        {
            tutManage.Manager.enabled = true;
            tutManage.getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
            tutManage.StartCoroutine(tutManage.waitAndDisable(name, 1));
        }
    }

    /// <summary>
    /// Level 1 Tutorial-Panel 1
    /// </summary>
    /// <param name="name"></param>
    /// <param name="phase"></param>
    public void TUT_1(string name, int phase)
    {
        if (phase == (int)phasen.initializePhase)
        {
            tutManage.Manager.enabled = false;
            tutManage.player.enabled = false;
            tutManage.getTUT(name).SetActive(true);
            tutManage.getTUT(name).GetComponent<Animator>().SetBool(name + "_Phase_" + phase, true);

        }
        if (phase == (int)phasen.firstPhase)
        {
            tutManage.getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }

        if (phase == (int)phasen.secondPhase)
        {
            tutManage.getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }

        if (phase == (int)phasen.thirdPhase)
        {
            tutManage.getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
            tutManage.StartCoroutine(tutManage.waitAndDisable(name, 2)); ;
            tutManage.Manager.enabled = true;
            tutManage.player.enabled = true;
        }
    }

    /// <summary>
    /// Level 1 Tutorial-Panel 2
    /// </summary>
    /// <param name="name"></param>
    /// <param name="phase"></param>
    public void TUT_2(string name, int phase)
    {

        if (phase == (int)phasen.initializePhase)
        {
            tutManage.getTUT(name).SetActive(true);
            tutManage.Manager.enabled = false;
            tutManage.player.enabled = false;
            tutManage.getTUT(name).GetComponent<Animator>().SetBool(name + "_Phase_" + phase, true);
        }

        if (phase == (int)phasen.firstPhase)
        {
            tutManage.getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
            tutManage.Manager.enabled = true;
            tutManage.player.enabled = true;
            tutManage.StartCoroutine(tutManage.waitAndDisable(name, 1));
        }

    }


}
