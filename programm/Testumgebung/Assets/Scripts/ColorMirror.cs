using UnityEngine;
using System.Collections;

public class ColorMirror : MonoBehaviour
{
    public CustomColor ValidColor = CustomColor.white;

    public enum CustomColor{     white,
                    red,
                    orange,
                    yellow,
                    green,
                    blue,
                    indigo,
                    violet,
                    All
    }
    

    public Vector3 GetReflection(CustomColor collisionColor, Vector3 dir, RaycastHit hit)
    {
        if (collisionColor == ValidColor )
        {
            return dir = Vector3.Reflect(dir, hit.normal);
        }

        return new Vector3(0, 0, 0);
        
    }
    // TODO ergänzen und bearbeiten
    public static CustomColor GetCustomColor(Color color)
    {
        if(color == Color.red )
        {
            return CustomColor.red;
        }

        if (color == Color.white)
        {
            return CustomColor.white;
        }
        if (color == Color.yellow)
        {
            return CustomColor.yellow;
        }
        if (color == Color.green)
        {
            return CustomColor.green;
        }
        if (color == Color.blue)
        {
            return CustomColor.blue;
        }
        if (color == Color.magenta)
        {
            return CustomColor.indigo;
        }
        return CustomColor.white;
     
    }
	
}
