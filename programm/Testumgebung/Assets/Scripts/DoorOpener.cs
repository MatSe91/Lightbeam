using UnityEngine;
using System.Collections;
using System;

public class DoorOpener: MonoBehaviour
{
    [Tooltip("Gameobjekt der Tür (wo die Animation ist)")]
    public GameObject Door;

    [Tooltip("Ist eine Farbe zur Aktivierung notwendig")]
    public bool withColor;

    [Tooltip("Farbe, welche den Schalter aktiviert. Nur mit 'withColor' aktiv")]  
	public CustomColor.CustomizedColor validColor;

    [Tooltip("Soll der Türschalter dauerhaft angeleuchtet werden?")]
    public bool ConstantTrigger;

    [Tooltip("Sekunden bis Schalter aktiviert")]
    public float secTillActivation = 60f;


    private bool doorIsOpen =false;
    private bool isBeamconnected;
    private CustomColor.CustomizedColor collisionColor;
    private Collider other;
    private float timecounter = 0f;

    public CustomColor.CustomizedColor CollisionColor
    {
        get
        {
            return collisionColor;
        }

        set
        {
            collisionColor = value;
        }
    }

    public bool DoorIsOpen
    {
        get
        {
            return doorIsOpen;
        }

        set
        {
            doorIsOpen = value;
        }
    }

    public void OpenDoor()
    {

        if (!withColor && !DoorIsOpen && isBeamconnected)
        {          
            Debug.Log("Animation Door open");
            DoorIsOpen = true;
        }

        if (CollisionColor == validColor && withColor && !DoorIsOpen && isBeamconnected)
        {           
            Debug.Log("Animation Door open");
            DoorIsOpen = true;
        } 
    }

    public void SetBeamConnected(bool value)
    {
        isBeamconnected = value;
        if (value)
        {
           
        }
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (timecounter%60 <= 0)
        {
            
            OpenDoor();
            other = col.collider;
        }
        Debug.Log(Mathf.Floor(timecounter % 60));
    }

  


    void Update()
    {      
        if (!other && DoorIsOpen && ConstantTrigger)
        { 
            CloseDoor(); 
        }
        if(isBeamconnected)
        {
            timecounter -= Time.deltaTime;
        }
        else timecounter = secTillActivation;

    }

    public void CloseDoor()
    {
        if (DoorIsOpen && !isBeamconnected)
        {
            Debug.Log("Animation Door close");
            DoorIsOpen = false;
           
        }
    }
    void Start()
    {
        timecounter = secTillActivation;
    }
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

