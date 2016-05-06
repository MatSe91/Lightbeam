using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Reflection;

public class TUT_Manager : MonoBehaviour {
    public float startTutAfterXSeconds;
    public InputManager manager;
    public GameObject playerOuter;
    public List<GameObject> tuts;
    private bool tut_1 = false;


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
        MethodInfo mi =  this.GetType().GetMethod(name, new Type[] { typeof(int) });      
        mi.Invoke(this, new object[] { v });
    }

    public void TUT_0(int phase)
    {
        
        if(phase == 0)
        {
            manager.enabled = false;
            getTUT("TUT_0").SetActive(true);
            getTUT("TUT_0").GetComponent<Animator>().SetBool("TUT_0_Phase_0", true);
        }
        if (phase == 1)
        {
            getTUT("TUT_0").GetComponent<Animator>().SetTrigger("TUT_0_Phase_1");
        }
        if (phase == 2)
        {
            getTUT("TUT_0").GetComponent<Animator>().SetTrigger("TUT_0_Phase_2");
        }
        if (phase == 3)
        {
            manager.enabled = true;
            getTUT("TUT_0").GetComponent<Animator>().SetTrigger("TUT_0_Phase_3");
            StartCoroutine(waitAndDisable("TUT_0", 1));           
        }
    }


    public void TUT_1(int phase)
    {
        if (phase == 0)
        {
            manager.enabled = false;
            playerOuter.GetComponent<PlayerRotator>().enabled = false;

            getTUT("TUT_1").SetActive(true);
            getTUT("TUT_1").GetComponent<Animator>().SetBool("TUT_1_Phase_0", true);

        }
        if (phase == 1)
        {
            getTUT("TUT_1").GetComponent<Animator>().SetTrigger("TUT_1_Phase_1_2");
            getTUT("TUT_1").GetComponent<Animator>().SetBool("TUT_1_Phase_1_1_arrow", false);
        }

        if (phase == 2)
        {
            getTUT("TUT_1").GetComponent<Animator>().SetTrigger("TUT_1_Phase_1_3");
        }

        if (phase == 3)
        {
            getTUT("TUT_1").GetComponent<Animator>().SetTrigger("TUT_1_Phase_1_end");
            StartCoroutine(waitAndDisable("TUT_1", 2));
            manager.enabled = true;
            playerOuter.GetComponent<PlayerRotator>().enabled = true;
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
            TUT_0(0);
        }
        if (LevelManager.GameStarted)
        {
            if (!tut_1)
            {
                TUT_1(0);
                tut_1 = true;
            }
        }       
    }
}
