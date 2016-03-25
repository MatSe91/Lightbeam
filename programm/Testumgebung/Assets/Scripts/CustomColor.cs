using UnityEngine;
using System.Collections;

public  class  CustomColor : MonoBehaviour   {

    public enum CustomizedColor
{
    white,
    red,
    orange,
    yellow,
    green,
    blue,
    indigo,
    violet,
    All
}

// TODO ergänzen und bearbeiten
public static CustomizedColor GetCustomColor(Color color)
{
    if (color == Color.red)
    {
        return CustomizedColor.red;
    }

    if (color == Color.white)
    {
        return CustomizedColor.white;
    }
    if (color == Color.yellow)
    {
        return CustomizedColor.yellow;
    }
    if (color == Color.green)
    {
        return CustomizedColor.green;
    }
    if (color == Color.blue)
    {
        return CustomizedColor.blue;
    }
    if (color == Color.magenta)
    {
        return CustomizedColor.indigo;
    }
    
    return CustomizedColor.white;

}
}
