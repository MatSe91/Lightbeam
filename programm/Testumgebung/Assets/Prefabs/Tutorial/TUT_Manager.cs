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
    private bool tut_1 = false;
    private float timecounter;
    private bool tut_2;
    private float secTillActivation = 2f;

    private List<GameObject> levelList = new List<GameObject>();
    public List<GameObject> tuts = new List<GameObject>();
    private GameObject activeLevel;

    public InputManager Manager
    {
        get
        {
            return manager;
        }
    }

    public bool Tut_1
    {
        get
        {
            return tut_1;
        }

        set
        {
            tut_1 = value;
        }
    }

    void Start()
    {
        getAllLevelsWithTutorial();
        activateLevel();
        getAllTUTsInLevel();
        activeLevel.SetActive(true);

    }

    public void getAllTUTsInLevel()
    {
        for (int i = 0; i < activeLevel.transform.childCount; i++)
        {
            tuts.Add(activeLevel.transform.GetChild(i).gameObject);
            Debug.Log(activeLevel.transform.GetChild(i).gameObject);
        }
    }

    public void getAllLevelsWithTutorial()
    {

        for(int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {
            levelList.Add(gameObject.transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    public GameObject getTUT(string tutName)
    {
        foreach (var item in tuts)
        {
            if (item.name == tutName)
            {
                return item;
            }
        }
        return null;
    }

    public void activateLevel()
    {
        foreach (var level in levelList)
        {
            if (level.name == MadLevelManager.MadLevel.currentLevelName)
            {
                activeLevel = level;
            }
        }
    }


    internal void activate(TUT_Manager go, string name, int v)
    {
        MethodInfo mi = this.GetType().GetMethod(name, new Type[] { typeof(string), typeof(int) });
        mi.Invoke(go, new object[] { name, v });
    }
   
    public IEnumerator waitAndDisable(String tut, int v)
    {
        yield return new WaitForSeconds(v);
        getTUT(tut).SetActive(false);
    }

    void Update()
    {

        //if (Time.timeSinceLevelLoad > startTutAfterXSeconds && Time.timeSinceLevelLoad < startTutAfterXSeconds+0.05f)
        //{
        //    TUT_0("TUT_0", 0);
        //}
        
        //    if (endpoint.GetComponent<Endpoint>().getBeamConnectivity())
        //    {
        //        timer();
        //    }
        //    else
        //    {
        //        timecounter = secTillActivation;
        //    }
        //}       
    }

    public enum phasen
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
        //timecounter -= Time.deltaTime;

        //if (timecounter % 60 <= 0)
        //{
        //    if (!tut_2)
        //    {
        //        TUT_2("TUT_2", 0);
        //        tut_2 = true;
        //    }

        //}
    }
}
