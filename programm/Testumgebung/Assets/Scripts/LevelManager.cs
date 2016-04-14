using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MadLevelManager;

public class LevelManager : MonoBehaviour {
    private GameObject inputManager;
    private GameObject lastActiveGameObject;
    private GameObject pauseObject;

    //private List<GameObject> waterDropList;

    void Awake()
    {
        inputManager = GameObject.Find("Main Camera");
    }

    public  void Pause(GameObject obj)
    {
        pauseObject = obj;
        lastActiveGameObject = InputManager.lastObjectBeforePause;


        if (pauseObject.activeInHierarchy == false)
        {
            de_ActivateMenue(true, 0, false);
        }
        else
        {
            de_ActivateMenue(false, 1, true);
        }
    }

    private void de_ActivateMenue(bool canvasActive, int timeScale, bool objectActive)
    {
        pauseObject.SetActive(canvasActive);
        //Time.timeScale = timeScale;
        De_ActivateGameObjects(objectActive);
    }

    private void De_ActivateGameObjects(bool bo)
    {
        if (lastActiveGameObject != null)
        {
            lastActiveGameObject.SendMessage("setActiveGameObject", bo);
            inputManager.GetComponent<InputManager>().enabled = bo;
        }
    }

    public  void SetCollectedItems()
    {
        CollectibleManager.AddCollectedItems();
        CollectibleManager.SetCollectiblesToLevel();
    }


    public void ClickAgain()
    {
        MadLevel.LoadLevelByName(MadLevel.currentLevelName);
    }

    public void ClickNext()
    {
        MadLevel.LoadNext();
    }

    public void ClickSelect()
    {
        MadLevel.LoadLevelByName("Select Level");
    }
}
