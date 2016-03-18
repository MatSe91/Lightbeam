using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

    private GameObject inUse;
    private GameObject sameObject;
    private bool gameStarted;
    public GameObject PlayerChild;

	// Use this for initialization
	void Start () {
        gameStarted = false;
	}

    // Update is called once per frame
    void Update()
    {
        // prüfe ob die linke Maustaste gedrückt wird
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Prüfe ob etwas vom Raycast getroffen wird
            if (Physics.Raycast(ray, out hit))
            {
                // ermittle das GameObject, welches jetzt gerade getroffen wurde
                inUse = hit.collider.gameObject;

                // Wenn Hintergrund oder Wand getroffen wurde mache nichts
                // TODO refctoring
                if (inUse.name == "Background" || inUse.tag == "Wall" || inUse.layer == 4|| inUse.layer == 9)
                {
                    return;
                }

                // wenn ein TouchCollider getroffen wurde
                if (inUse.name == "TouchCollider")
                {
                    // dann ermittle das Parent GameObject
                    inUse = inUse.transform.parent.gameObject;

                    // Wird der Lichtkörper angeklickt starte das Spiel und aktiviere Script PlayerRotator2
                    if (!gameStarted && inUse.Equals(PlayerChild))
                    {
                        gameStarted = true;
                        PlayerChild.GetComponent<PlayerRotator>().enabled = true;
                        //  Debug.Log("Spiel Gestartet:" + gameStarted );
                    }

                    // wenn das Spiel gestartet wurde darfst du arbeiten
                    if (gameStarted)
                    {

                        // Debug.Log("foo" + inUse, inUse);
                        // wenn ein neues Objekt gewählt wurde, setze das Alte auf passiv
                        if (sameObject != null && !sameObject.Equals(inUse))
                        {

                            sameObject.SendMessage("setActiveGameObject", false);
                        }
                        // und setze Objekt auf aktiv
                        inUse.SendMessage("setActiveGameObject", true);

                    }

                }

                // speichere das GameObject zwischen zur Überprüfung
                sameObject = inUse;
            }

        }

    }
}
