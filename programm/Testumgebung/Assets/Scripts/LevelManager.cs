using UnityEngine;
using MadLevelManager;
using DigitalRuby.FastLineRenderer;

public class LevelManager : MonoBehaviour {

    private static bool gameStarted;
    private static bool gameFinished;

    private GameObject lastActiveGameObject;
    private GameObject pauseCanvas;
    private GameObject endMenuCanvas;

    public static bool GameStarted
    {
        get
        {
            return gameStarted;
        }

        set
        {
            gameStarted = value;
        }
    }

    public static bool GameFinished
    {
        get
        {
            return gameFinished;
        }

        set
        {
            gameFinished = value;
        }
    }

    void Awake()
    {
        GameFinished = false;
        GameStarted = false;
    }

    public void Pause(GameObject obj)
    {
        pauseCanvas = obj;
        lastActiveGameObject = InputManager.sameObject;


        if (!pauseCanvas.activeInHierarchy)
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
            lastActiveGameObject.SendMessage("setActiveGameObject", bo);
            gameObject.GetComponent<InputManager>().enabled = bo;
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
        MadLevel.LoadLevelByName("SelectLevel");
    }

    public void ClickMainMenu()
    {
        destroyLightbeam();
        MadLevel.LoadLevelByName("MainMenu");
      
    }

    public void ClickSettings()
    {
        destroyLightbeam();
        MadLevel.LoadLevelByName("SettingsMenu");

    }

    private static void destroyLightbeam()
    {
        var beam = GameObject.FindGameObjectsWithTag("Beam");
        foreach (var item in beam)
        {
            FastLineRenderer flr = item.GetComponent<Beamscript>().R;
            flr.Reset();
            Destroy(flr);
            Destroy(item);
        }
    }
}
