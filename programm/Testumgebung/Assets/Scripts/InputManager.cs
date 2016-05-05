using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

    private GameObject inUse;
    public static GameObject sameObject;
    private GameObject player;
    public static bool touchInput;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
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
                    if (!LevelManager.GameStarted && hit.transform.gameObject.Equals(player.GetComponentInChildren<PlayerRotator>().gameObject))
                    {
                        player.GetComponent<LoadPlayerForGame>().ManageStart();
                    }

                    // wenn das Spiel gestartet wurde darfst du arbeiten
                    if (LevelManager.GameStarted)
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
