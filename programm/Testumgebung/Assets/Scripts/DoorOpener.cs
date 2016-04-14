using UnityEngine;
using System.Collections;
using System;
using MadLevelManager;
using DigitalRuby.FastLineRenderer;

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

    [Tooltip("Object, das die Türknopfsimulation besitzt (Script).")]
    public GameObject activated;

    [Tooltip("Loading Particlesystem")]
    public GameObject loading; 

    private ParticleSystem loadingParticleSystem;
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
            OpenDoorProperties();
            Debug.Log("ohne color");
            //   MadLevel.LoadLevelByName("Select Level");
        }

        if (CollisionColor == validColor && withColor && !DoorIsOpen && isBeamconnected)
        {
            OpenDoorProperties();
            Debug.Log("mit color");
        }
    }

    private void OpenDoorProperties()
    {
        if (loadingParticleSystem.isPlaying) loadingParticleSystem.Stop();
        Door.GetComponent<DoorManager>().OpenDoor();
        GetComponentInChildren<DoorKnopSimulation>().activate();
        DoorIsOpen = true;
    }

    public void SetBeamConnected(bool value)
    {
        isBeamconnected = value;
    }

    void OnCollisionEnter(Collision col)
    {

        if (timecounter%60 <= 0)
        {
            OpenDoor();
            other = col.collider;
        }

        if (!loadingParticleSystem.isPlaying)
        {
            if (doorIsOpen)
            {
                return;
            }
            else
                loadingParticleSystem.Play();
        }
    }

    void Update()
    {      
        if (!other && DoorIsOpen && ConstantTrigger)
        { 
            CloseDoor(); 
        }

        if (isBeamconnected)
        {
            timecounter -= Time.deltaTime;
        }
        else
        {
            timecounter = secTillActivation;
            if (loadingParticleSystem.isPlaying) loadingParticleSystem.Stop();
        }
    }

    public void CloseDoor()
    {
        if (DoorIsOpen && !isBeamconnected)
        {
            Door.GetComponent<DoorManager>().CloseDoor();
            GetComponentInChildren<DoorKnopSimulation>().deactivate();
            DoorIsOpen = false;
           
        }
    }
    void Start()
    {
        timecounter = secTillActivation;
        loadingParticleSystem = loading.GetComponent<ParticleSystem>();
    }
}
