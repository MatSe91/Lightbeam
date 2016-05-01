using UnityEngine;
using MadLevelManager;


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

            if (levelManager!= null && levelManager.GameFinished) // game finished
            {
                if (endMenuCanvas != null)
                    levelManager.Pause(endMenuCanvas);
            }
            else if (pauseMenuCanvas != null)
                levelManager.Pause(pauseMenuCanvas);

            //gameObject.GetComponent<InputManager>().enabled = false;
        }

    }
}
