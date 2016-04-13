using UnityEngine;
using System.Collections;
using System;

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


    public void IsParticleBeamFinished(bool value)
    {
        isParticleBeamFinished = value;
    }

    void Start()
    {
        targetPosition =  doorOpenParticleBeam.transform.position;
        detectAchse();
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
            }
        }
        else
        {
            if (curPosition.x == targetPosition.x)
            {
                Debug.Log("macht er was?");
                isParticleBeamFinished = true;
            }
        }
        
    }
}
