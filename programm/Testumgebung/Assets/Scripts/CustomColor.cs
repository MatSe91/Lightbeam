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
    }

// TODO ergänzen und bearbeiten
    public static Color GetColor(CustomizedColor color)
    {
        if (color == CustomizedColor.white)
        {
            return Color.white;
        }
        if (color == CustomizedColor.red)
        {
            return Color.red;
        }
        if (color == CustomizedColor.orange)
        {
            return new Color(1, 0.65f, 0, 1);
        }
        if (color == CustomizedColor.yellow)
        {
            return Color.yellow;
        }
        if (color == CustomizedColor.green)
        {
            return Color.green;
        }
        if (color == CustomizedColor.blue)
        {
            return Color.blue;
        }
        if (color == CustomizedColor.indigo)
        {
            return new Color(0.29f, 0, 0.51f, 1);
        }
        if (color == CustomizedColor.violet)
        {
            return new Color(0.5f, 0, 0.5f, 1);
        }
        return Color.white;
    }

    public static CustomizedColor GetCustomColor(Color color)
    {
        if (color == Color.white)
        {
            return CustomizedColor.white;
        }
        if (color == Color.red)
        {
            return CustomizedColor.red;
        }
        if (color == new Color(255, 165, 0, 1))
        {
            return CustomizedColor.orange;
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
        if (color == new Color(75, 0, 130, 1))
        {
            return CustomizedColor.indigo;
        }
        if (color == new Color(128, 0, 128, 1))
        {
            return CustomizedColor.violet;
        }
        return CustomizedColor.white;
    }


}
