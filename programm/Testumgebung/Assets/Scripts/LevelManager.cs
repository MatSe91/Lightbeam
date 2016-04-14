using UnityEngine;
using MadLevelManager;
using DigitalRuby.FastLineRenderer;

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
            de_ActivateMenue(true, false);
        }
        else
        {
            de_ActivateMenue(false, true);
        }
    }

    private void de_ActivateMenue(bool canvasActive, bool objectActive)
    {
        pauseObject.SetActive(canvasActive);
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
