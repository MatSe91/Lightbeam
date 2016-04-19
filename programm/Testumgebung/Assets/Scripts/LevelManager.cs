using UnityEngine;
using MadLevelManager;
using DigitalRuby.FastLineRenderer;

public class LevelManager : MonoBehaviour {
    private GameObject inputManager;
    private GameObject lastActiveGameObject;
    private GameObject pauseCanvas;

    void Awake()
    {
        inputManager = GameObject.Find("Main Camera");
    }

    public void Pause(GameObject obj)
    {
        pauseCanvas = obj;
        lastActiveGameObject = InputManager.sameObject;


        if (pauseCanvas.activeInHierarchy == false)
        {
            de_ActivateMenue(true, false);
        }
        else
        {
            de_ActivateMenue(false, true);
        }
    }

    private void de_ActivateMenue(bool canvasActive, bool objectActive)
    {
        pauseCanvas.SetActive(canvasActive);
        De_ActivateGameObjects(objectActive);
    }

    private void De_ActivateGameObjects(bool bo)
    {
        if (lastActiveGameObject != null)
        {
            Debug.Log("Input: " +inputManager);
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
        destroyLightbeam();
        MadLevel.LoadLevelByName(MadLevel.currentLevelName);
    }

    public void ClickNext()
    {
        destroyLightbeam();
        MadLevel.LoadNext();
    }

    public void ClickSelect()
    {
        destroyLightbeam();
        MadLevel.LoadLevelByName("Select Level");
    }

    public void ClickMainMenu()
    {
        destroyLightbeam();
        MadLevel.LoadLevelByName("MainMenu");
    }

    private static void destroyLightbeam()
    {
        var beam = GameObject.FindGameObjectsWithTag("Beam");
        foreach (var item in beam)
        {
            FastLineRenderer flr = item.GetComponent<BeamScript_RJ>().R;
            flr.Reset();
            Destroy(flr);
            Destroy(item);
        }
    }
}
