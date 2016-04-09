using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class DoorOpener: MonoBehaviour
{

	public CustomColor.CustomizedColor validColor;
    public GameObject Door;
    public bool WithColor;
    public int counter;
    public bool ConstantTrigger;
    public bool doorIsOpen =false;
    private Collider other;

    public int openDoorInSec; // wird später abgeknöpft von counter
    public GameObject loadingDoor;
    public GameObject activadedDoor;



    public void OpenDoor(CustomColor.CustomizedColor collisionColor)
    {

        if (!WithColor && !doorIsOpen)
        {
           
            Debug.Log("Animation Door open");
            doorIsOpen = true;
        }

        if (collisionColor == validColor && WithColor && !doorIsOpen)
        {
           
            Debug.Log("Animation Door open");
            doorIsOpen = true;
        }
 
    }

    private void activateLoadingDoor()
    {
        ParticleSystem parSys = loadingDoor.GetComponent<ParticleSystem>();
        for (int i =0; i< openDoorInSec; i++)
        {
            parSys.Simulate(i);
            StartCoroutine(moveOn(parSys));
        }
    }

    private IEnumerator moveOn(ParticleSystem parSys)
    {
        if (!parSys.IsAlive())
        {
            
        }
        throw new NotImplementedException();
    }


    private void invokeLoadingDoor()
    {

    }

    private void activateActiveDoorAnim()
    {

    }


    // starte die Partikel mit X sekunden dauer
    // Wenn lichtstrahl runter --> breche Partikel abspielen ab (aktuellen Loop beenden, nicht nur abbrechen)
    // wenn Partikel dauer in Sec erfolgreich durchgelaufen
        // beende Partikel
        // öffne Tür
        // spiele animation ab
    
    // ggf wenn licht von doorknop
        // dann beende animation
    // ggf selbe prozedure von vorn
}
