using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DoorManager : MonoBehaviour {

    [Tooltip("GameObject Door Open Particle Beam")]
    public GameObject doorOpenParticleBeam;

    [Tooltip("Schwingung des Particle Beams")]
    public float amplitude;

    [Tooltip("Wiederholung der Schwingung")]
    public float frequency;

    [Tooltip("Geschwindigkeit der Bewebung des Particle Beams")]
    public float speed;

    [Tooltip("Achse in der die Amplitude schwinkt.")]
    public achse amplituteAchse;

    public enum achse {x_Achse, y_Achse }
    private bool isParticleBeamFinished;
    private bool isDoorOpen = false;

    private Vector3 curPosition;
    private Vector3 targetPosition;
    private Vector3 V3Achse;
    public List<GameObject> doors;


    public void IsParticleBeamFinished(bool value)
    {
        isParticleBeamFinished = value;
    }

    void Start()
    {
        targetPosition =  doorOpenParticleBeam.transform.position;
        detectAchse();
        doorOpenParticleBeam.GetComponent<AudioSource>().Play(44100);
    }

    private void detectAchse()
    {
        if (amplituteAchse == achse.x_Achse)
        {
            V3Achse = transform.right;
        }
        else
        {
            V3Achse = transform.up;
        }
    }

    public void OpenDoor()
    {
        isDoorOpen = true;
        doorOpenParticleBeam.SetActive(true);
        curPosition = transform.parent.GetChild(0).localPosition;
      
    }
    public void CloseDoor()
    {
        isDoorOpen = false;
        isParticleBeamFinished = false;
    }

    void Update()
    {
        if (isDoorOpen && !isParticleBeamFinished)
        {
            float step = speed * Time.deltaTime;
            doorOpenParticleBeam.transform.position = Vector3.MoveTowards(curPosition, targetPosition, step);
            doorOpenParticleBeam.transform.position += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * V3Achse;
            curPosition = doorOpenParticleBeam.transform.position;
            checkEnd(curPosition);
        }
        else
        {
            var sys = doorOpenParticleBeam.GetComponent<ParticleSystem>();
            sys.Stop();

            if (!sys.IsAlive())
                doorOpenParticleBeam.SetActive(false);
        }
    }

    private void checkEnd(Vector3 curP)
    {
        if (amplituteAchse == achse.x_Achse)
        {
            if (curPosition.y == targetPosition.y)
            {
                Debug.Log("macht er was?");
                isParticleBeamFinished = true;
                OpenDoorAnimation();
            }
        }
        else
        {
            if (curPosition.x == targetPosition.x)
            {
                Debug.Log("macht er was?");
                isParticleBeamFinished = true;
                OpenDoorAnimation();
            }
        }
        
    }
    private void OpenDoorAnimation()
    {
        foreach (var door in doors)
        {
            door.GetComponent<Animator>().SetTrigger("DoorOpen");
           
        }
    }
}
