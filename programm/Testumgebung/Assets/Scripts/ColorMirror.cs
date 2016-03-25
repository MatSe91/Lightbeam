using UnityEngine;
using System.Collections;

public class ColorMirror : MonoBehaviour
{
    public CustomColor.CustomizedColor ValidColor = CustomColor.CustomizedColor.white;

    
    

    public Vector3 GetReflection(CustomColor.CustomizedColor collisionColor, Vector3 dir, RaycastHit hit)
    {
        if (collisionColor == ValidColor )
        {
            return dir = Vector3.Reflect(dir, hit.normal);
        }

        return new Vector3(0, 0, 0);
        
    }
 
	
}
