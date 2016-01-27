using UnityEngine;
using System.Collections;
using System;

public class SelectObject : MonoBehaviour {

    private GameObject inUse;
    private GameObject sameObject;
    private bool gameStarted;
    public GameObject Outer;

	// Use this for initialization
	void Start () {
        gameStarted = false;
	}

    private void ElementZuweisen()
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
                if (inUse.name == "Background" || inUse.tag == "Wall")
                {
                    return;   
                }

                // wenn ein TouchCollider getroffen wurde
                if (inUse.name == "TouchCollider")
                {
                    // dann ermittle das Parent GameObject
                    inUse = inUse.transform.parent.gameObject;

                    // Wird der Lichtkörper angeklickt starte das Spiel und aktiviere Script PlayerRotator2
                    if (!gameStarted && inUse.Equals(Outer))
                    {
                        gameStarted = true;
                        Outer.GetComponent<PlayerRotator>().enabled = true;
                       //  Debug.Log("Spiel Gestartet:" + gameStarted );
                    }
                    
                    // wenn das Spiel gestartet wurde darfst du arbeiten
                    if (gameStarted)
                    {
                        // und setze Rotierbarkeit auf true
                        inUse.SendMessage("setRotatable", true);

                        // wenn ein neues Objekt gewählt wurde, setze das Alte auf nicht rotierbar
                        if (sameObject != null && !sameObject.Equals(inUse))
                        {
                            sameObject.SendMessage("setRotatable", false);
                        }

                    }

                }

                // speichere das GameObject zwischen zur Überprüfung
                sameObject = inUse;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        ElementZuweisen();

    }
}
