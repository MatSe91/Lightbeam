using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    private GameObject inputManager;
    private GameObject lastActiveGameObject;
    private GameObject pauseObject;

    private List<GameObject> waterDropList;

    void Awake()
    {
        inputManager = GameObject.Find("Main Camera");
    }

    public void Pause(GameObject obj)
    {
        pauseObject = obj;
        lastActiveGameObject = InputManager.lastObjectBevorePause;


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

    public void addWaterDrop(GameObject water)
    {
        waterDropList.Add(water);
    }

    private void gainWaterDrop(List<GameObject> waterDropList)
    {
        // TODO jeder Waterdrop, der in der Liste steht, soll persistiert werden
    }
}
