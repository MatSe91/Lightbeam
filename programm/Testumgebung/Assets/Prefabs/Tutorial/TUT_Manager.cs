using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Reflection;

/// <summary>
/// Diese Klasse dient als Tutorial f�r das Demo-Spiel.
/// Sie ist absolut kacke geschrieben!!! In der Entwicklung des Live-Spiels wird hier wesentlich mehr Zeit investiert!
/// M. S. 
/// </summary>
public class TUT_Manager : MonoBehaviour {
    public float startTutAfterXSeconds;
    public InputManager manager;
    public PlayerRotator player;
    public GameObject endpoint;
    public List<GameObject> tuts;
    private bool tut_1 = false;
    private float timecounter;
    private bool tut_2;
    private float secTillActivation = 2f;

    private GameObject getTUT(string tutName)
    {
        foreach (var item in tuts)
        {
            if (item.name == tutName)
            {
                return item;
            }
        }
        throw new MissingReferenceException("Tutorial not found!");
    }


    internal void activate(string name, int v)
    {
        MethodInfo mi =  this.GetType().GetMethod(name, new Type[] {typeof(string), typeof(int) });      
        mi.Invoke(this, new object[] {name, v });
    }
    /// <summary>
    /// Level 1 Tutorial-Panel 0
    /// </summary>
    /// <param name="name"></param>
    /// <param name="phase"></param>
    public void TUT_0(string name, int phase)
    {
        
        if(phase == (int)phasen.initializePhase)
        {
            manager.enabled = false;
            getTUT(name).SetActive(true);
            getTUT(name).GetComponent<Animator>().SetBool(name+"_Phase_"+phase, true);
        }
        if (phase == (int)phasen.firstPhase)
        {
            getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }
        if (phase == (int)phasen.secondPhase)
        {
            getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }
        if (phase == (int)phasen.thirdPhase)
        {
            manager.enabled = true;
            getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
            StartCoroutine(waitAndDisable(name, 1));           
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
            manager.enabled = false;
            player.enabled = false;
            getTUT(name).SetActive(true);
            getTUT(name).GetComponent<Animator>().SetBool(name + "_Phase_" + phase, true);

        }
        if (phase == (int)phasen.firstPhase)
        {
            getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }

        if (phase == (int)phasen.secondPhase)
        {
            getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
        }

        if (phase == (int)phasen.thirdPhase)
        {
            getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
            StartCoroutine(waitAndDisable(name, 2));
            manager.enabled = true;
            player.enabled = true;
        }
    }


    public void TUT_2(string name, int phase)
    {

        if (phase == (int)phasen.initializePhase)
        {
            getTUT(name).SetActive(true);
            manager.enabled = false;
            player.enabled = false;
            getTUT(name).GetComponent<Animator>().SetBool(name + "_Phase_" + phase, true);
        }

        if (phase == (int)phasen.firstPhase)
        {
            getTUT(name).GetComponent<Animator>().SetTrigger(name + "_Phase_" + phase);
            manager.enabled = true;
            player.enabled = true;
            StartCoroutine(waitAndDisable(name, 1));
        }

    }

    private IEnumerator waitAndDisable(String tut, int v)
    {
        yield return new WaitForSeconds(v);
        getTUT(tut).SetActive(false);
    }

    void Update()
    {

        if (Time.timeSinceLevelLoad > startTutAfterXSeconds && Time.timeSinceLevelLoad < startTutAfterXSeconds+0.05f)
        {
            TUT_0("TUT_0", 0);
        }
        if (LevelManager.GameStarted)
        {
            if (!tut_1)
            {
                TUT_1("TUT_1", 0);
                tut_1 = true;
            }

            if (endpoint.GetComponent<Endpoint>().getBeamConnectivity())
            {
                timer();
            }
            else
            {
                timecounter = secTillActivation;
            }
        }       
    }

    private enum phasen
    {
        initializePhase,
        firstPhase,
        secondPhase,
        thirdPhase,
        fourPhase,
        fivePhase
    }

    private void timer()
    {
        timecounter -= Time.deltaTime;

        if (timecounter % 60 <= 0)
        {
            if (!tut_2)
            {
                TUT_2("TUT_2", 0);
                tut_2 = true;
            }

        }
    }
}
