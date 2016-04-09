using UnityEngine;
using System.Collections;

public class DoorOpener: MonoBehaviour
{

	public CustomColor.CustomizedColor validColor;
    public GameObject Door;
    public bool WithColor;
    public int counter;
    public bool ConstantTrigger;
    private bool doorIsOpen =false;
    private bool isBeamconnected;
    private CustomColor.CustomizedColor collisionColor;
    private Collider other;

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

        if (!WithColor && !DoorIsOpen && isBeamconnected)
        {
           
            Debug.Log("Animation Door open");
            DoorIsOpen = true;
        }

        if (CollisionColor == validColor && WithColor && !DoorIsOpen && isBeamconnected)
        {
           
            Debug.Log("Animation Door open");
            DoorIsOpen = true;
        }

     
 
    }

    public void SetBeamConnected(bool value)
    {
        isBeamconnected = value;
    }

    void OnCollisionEnter(Collision col)
    {

        Debug.Log("Collison");
        OpenDoor();
        other = col.collider;

    }
    void Update()
    {
        if(!other && DoorIsOpen)
        { 
            CloseDoor();
        }
    }

    public void CloseDoor()
    {
        if (DoorIsOpen && !isBeamconnected)
        {
            Debug.Log("Animation Door close");
            DoorIsOpen = false;
        }
    }
}
