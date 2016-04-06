using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
    private GameObject lastActiveGameObject;
    private GameObject pauseObject;

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
             lastActiveGameObject.SendMessage("setActiveGameObject", bo);
        GetComponentInParent<InputManager>().enabled = bo;
    }
}
