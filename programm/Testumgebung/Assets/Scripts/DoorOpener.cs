using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {

	public ColorMirror.CustomColor validColor;
    public GameObject Door;
    public bool WithColor;

     public void OpenDoor(ColorMirror.CustomColor collisionColor)
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
