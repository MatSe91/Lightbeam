using UnityEngine;
using System.Collections;

public class DoorOpener: MonoBehaviour
{

	public CustomColor.CustomizedColor validColor;
    public GameObject Door;
    public bool WithColor;

    public int counter;

     public void OpenDoor(CustomColor.CustomizedColor collisionColor)
    {

        if (!WithColor)
        {
            //Testzwecke
            Door.transform.position = new Vector3(10f, 10f, 0f);

        }

        if (collisionColor == validColor && WithColor)
        {
            //Testzweck
            Door.transform.position = new Vector3(10f, 10f, 0f);
            //playanimation
        }

        
    }
}
