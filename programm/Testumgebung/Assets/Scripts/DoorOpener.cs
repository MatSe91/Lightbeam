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

    public GameObject activated;
    public GameObject loading;
    

    private ParticleSystem activatedParticle;
    private Animator anim;


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
        if (activatedParticle.isPlaying) activatedParticle.Stop();

        if (!withColor && !DoorIsOpen && isBeamconnected)
        {          
            Debug.Log("Animation Door open");
            DoorIsOpen = true;
          
            MadLevel.LoadLevelByName("Select Level");
        }

        if (CollisionColor == validColor && withColor && !DoorIsOpen && isBeamconnected)
        {           
            Debug.Log("Animation Door open");
            DoorIsOpen = true;
        }
        GetComponentInChildren<SimulateDoor>().activate();

    }

    public void SetBeamConnected(bool value)
    {
        isBeamconnected = value;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!activatedParticle.isPlaying) activatedParticle.Play();
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

        if (isBeamconnected)
        {
            timecounter -= Time.deltaTime;
            
           // loadingParticle(true);
        }
        else
        {
            timecounter = secTillActivation;
            if (activatedParticle.isPlaying) activatedParticle.Stop();
            //loadingParticle(false);
        }
    }

    public void CloseDoor()
    {
        if (DoorIsOpen && !isBeamconnected)
        {
            Debug.Log("Animation Door close");
            GetComponentInChildren<SimulateDoor>().deactivate();
            DoorIsOpen = false;
           
        }
    }
    void Start()
    {
        timecounter = secTillActivation;
        activatedParticle = loading.GetComponent<ParticleSystem>();     
    }
}
