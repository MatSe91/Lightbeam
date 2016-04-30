using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

    private GameObject inUse;
    public static GameObject sameObject;
    public GameObject PlayerChild;
    public static bool touchInput;

    private LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = gameObject.GetComponent<LevelManager>();
	}

    // Update is called once per frame
    void Update()
    {
        // prüfe ob die linke Maustaste gedrückt wird
        if (Input.GetMouseButtonDown(0))
        {
            touchInput = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Prüfe ob etwas vom Raycast getroffen wird
            if (Physics.Raycast(ray, out hit))
            {
                // ermittle das GameObject, welches jetzt gerade getroffen wurde
                inUse = hit.transform.gameObject;

                if (inUse.layer == 14)
                { 
                    // Wird der Lichtkörper angeklickt starte das Spiel und aktiviere Script PlayerRotator2
                    if (!levelManager.GameStarted && inUse.Equals(PlayerChild))
                    {
                        levelManager.GameStarted = true;
                        PlayerChild.GetComponent<PlayerRotator>().enabled = true;
                    }

                    // wenn das Spiel gestartet wurde darfst du arbeiten
                    if (levelManager.GameStarted)
                    {
                        // wenn ein neues Objekt gewählt wurde, setze das Alte auf passiv
                        if (sameObject != null && !sameObject.Equals(inUse))
                        {
                            sameObject.SendMessage("setActiveGameObject", false);
                        }
                        // und setze Objekt auf aktiv
                     inUse.SendMessage("setActiveGameObject", true);
                     sameObject = inUse;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchInput = false;
        }
    }   
}
