using UnityEngine;
using System.Collections;
using System;

public class ColorMirror : MonoBehaviour
{
    private Vector3 dir;
    private bool reflected;

    public CustomColor.CustomizedColor ValidColor;

    public bool Reflected
    {
        get
        {
            return reflected;
        }

        set
        {
            reflected = value;
        }
    }

    public Vector3 Dir
    {
        get
        {
            return dir;
        }

        set
        {
            dir = value;
        }
    }

    public ColorMirror Reflect (CustomColor.CustomizedColor collisionColor, Vector3 dir, RaycastHit hit)
    {
        dir = new Vector3(0, 0, 0);
        reflected = false;
        if (collisionColor == ValidColor)
        {
            dir = Vector3.Reflect(dir, hit.normal);
            reflected = true;
        }

        return this;
    }

    [Obsolete("This Method is Deprecated. Please use ColorMirror.Reflect")]
    public Vector3 GetReflection(CustomColor.CustomizedColor collisionColor, Vector3 dir, RaycastHit hit)
    {
        if (collisionColor == ValidColor)
        {
            return dir = Vector3.Reflect(dir, hit.normal);
        }
        return new Vector3(0, 0, 0);        
    }
}
