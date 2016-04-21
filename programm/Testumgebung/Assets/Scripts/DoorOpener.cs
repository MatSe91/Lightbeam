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
        OpenDoorProperties();
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

    void Update()
    {      

        if (isBeamconnected)
        {
            if ((!withColor && !DoorIsOpen && isBeamconnected) || (withColor && CollisionColor.Equals(validColor) && !DoorIsOpen && isBeamconnected))
            {
                if (!loadingParticleSystem.isPlaying && !doorIsOpen)
                {
                    loadingParticleSystem.Play();
                }
                if (!doorIsOpen) timer();
            }


        }
        else
        {
            timecounter = secTillActivation;
            if (loadingParticleSystem.isPlaying) loadingParticleSystem.Stop();
        }

        CloseDoor();
    }

    private void timer()
    {
        timecounter -= Time.deltaTime;

        if (timecounter % 60 <= 0)
        {
            OpenDoor();
        }
    }

    public void CloseDoor()
    {
        if (DoorIsOpen && !isBeamconnected && ConstantTrigger)
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
