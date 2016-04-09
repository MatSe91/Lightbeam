using UnityEngine;
using System.Collections;

public class DoorOpener: MonoBehaviour
{

	public CustomColor.CustomizedColor validColor;
    public GameObject Door;
    public bool WithColor;
    public int counter;
    public bool ConstantTrigger;
    public bool dooIsOpen =false;
    private Collider other;

    public void OpenDoor(CustomColor.CustomizedColor collisionColor)
    {

        if (!WithColor && !dooIsOpen)
        {
           
            Debug.Log("Animation Door open");
            dooIsOpen = true;
        }

        if (collisionColor == validColor && WithColor && !dooIsOpen)
        {
           
            Debug.Log("Animation Door open");
            dooIsOpen = true;
        }
 
    }
}
