using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class TUT_Manager : MonoBehaviour {
    public float startTutAfterXSeconds;
    public InputManager manager;
    public GameObject playerOuter;
    public GameObject Collectables;
    public List<GameObject> tuts;
    private bool tut_2 = false;


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


    public void TUT_0(int phase)
    {
        if(phase == 0)
        {
            manager.enabled = false;
            getTUT("TUT_0").SetActive(true);
            getTUT("TUT_0").GetComponent<Animator>().SetBool("TUT_0_Phase_1", true);
        }
        if (phase == 1)
        {
            getTUT("TUT_0").GetComponent<Animator>().SetTrigger("TUT_0_Phase_2");
        }
        if (phase == 2)
        {
            getTUT("TUT_0").GetComponent<Animator>().SetTrigger("TUT_0_Phase_3");
        }
        if (phase == 3)
        {
            manager.enabled = true;
            getTUT("TUT_0").GetComponent<Animator>().SetTrigger("TUT_0_Phase_4");
            StartCoroutine(waitAndDisable(1));           
        }
    }


    public void TUT_1()
    {
        manager.enabled = false;
        playerOuter.GetComponent<PlayerRotator>().enabled = false;

        getTUT("TUT_1").SetActive(true);
        Debug.Log("Do something awesome!");
    }

    private IEnumerator waitAndDisable(int v)
    {
        yield return new WaitForSeconds(1);
        getTUT("TUT_0").SetActive(false);
    }

    void Update()
    {

        if (Time.timeSinceLevelLoad > startTutAfterXSeconds && Time.timeSinceLevelLoad < startTutAfterXSeconds+0.05f)
        {
            TUT_0(0);
        }
        if (LevelManager.GameStarted)
        {
            if (!tut_2)
            {
                TUT_1();
                tut_2 = true;
            }
        }       
    }
}
