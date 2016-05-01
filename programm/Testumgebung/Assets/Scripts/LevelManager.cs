using UnityEngine;
using MadLevelManager;
using DigitalRuby.FastLineRenderer;

public class LevelManager : MonoBehaviour {

    private bool gameStarted;
    private bool gameFinished;

    private GameObject lastActiveGameObject;
    private GameObject pauseCanvas;
    private GameObject endMenuCanvas;

    public bool GameStarted
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

    public bool GameFinished
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
        MadLevel.LoadLevelByName("Select Level");
    }

    public void ClickMainMenu(GameObject game)
    {
        destroyLightbeam();
        MadLevel.LoadLevelByName("MainMenu");
        game.GetComponent<AudioSource>().Play();
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
