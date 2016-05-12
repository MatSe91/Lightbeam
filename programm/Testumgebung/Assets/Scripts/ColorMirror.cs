using UnityEngine;

public class ColorMirror : MonoBehaviour
{
    public CustomColor.CustomizedColor ValidColor;

    public Vector3 GetReflection(CustomColor.CustomizedColor collisionColor, Vector3 dir, RaycastHit hit)
    {
        if (collisionColor == ValidColor)
        {
            return Vector3.Reflect(dir, hit.normal);
        }
        return new Vector3(0, 0, 0);        
    }
}
