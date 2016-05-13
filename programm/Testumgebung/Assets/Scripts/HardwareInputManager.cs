using UnityEngine;
using MadLevelManager;
using System.Collections.Generic;

public class HardwareInputManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject endMenuCanvas;
    public GameObject leaveGameMenu;

    private LevelManager levelManager;
    void Start()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MadLevel.currentLevelName == "MainMenu")
            {
                if (leaveGameMenu != null)
                {
                    leaveGameMenu.SetActive(true);
                    return;
                }
            }

            if (MadLevel.currentLevelName == "SelectLevel")
            {
                MadLevel.LoadLevelByName("MainMenu");
                return;
            }

            // das ist hardcore kacke --> hier m�sste sowas hin wie alle nichtunterbrechbaren animationen
            // klasse angefangen, aber ist grad iwie zu aufwendig das zu proggen.. bl���d
            //if (!endMenuCanvas.GetComponent<EndMenuManager>().InAnimation)
            //{
                if (levelManager!= null && LevelManager.GameFinished) // game finished
                {
                    if (endMenuCanvas != null && !endMenuCanvas.GetComponent<EndMenuManager>().InAnimation)
                        levelManager.Pause(endMenuCanvas);
                }
                else if (pauseMenuCanvas != null)
                    levelManager.Pause(pauseMenuCanvas);
            //}
        }
    }
}
