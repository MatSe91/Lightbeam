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
        violet,
    }

// TODO ergänzen und bearbeiten
    public static Color GetColor(CustomizedColor color)
    {
        if (color.Equals(CustomizedColor.white))
        {
            return new Color(0.58f, 0.529f, 1, 0.902f);
          //  return new Color(1, 1, 1, 0.9f);
        }
        if (color.Equals(CustomizedColor.red))
        {
            return new Color(1, 0, 0, 0.9f);
        }
        if (color.Equals(CustomizedColor.orange))
        {
            return new Color(1, 0.65f, 0, 0.9f);
        }
        if (color.Equals(CustomizedColor.yellow))
        {
            return new Color(1, 0.92f, 0.016f, 0.9f);
        }
        if (color.Equals(CustomizedColor.green))
        {
            return new Color(0, 1, 0, 0.9f);
        }
        if (color.Equals(CustomizedColor.blue))
        {
            return new Color(0, 0, 1, 0.9f);
        }
        if (color.Equals(CustomizedColor.violet))
        {
            return new Color(0.5f, 0, 0.5f, 0.9f);
        }
        return new Color(0.58f, 0.529f, 1, 0.902f);
       // return new Color(1, 1, 1, 0.9f); // white
    }

    public static CustomizedColor GetCustomColor(Color color)
    {
        if (color.Equals(new Color(0.58f, 0.529f, 1, 0.902f)))
        {
            return CustomizedColor.white;
        }
        if (color.Equals(new Color(1, 0, 0, 0.9f)))
        {
            return CustomizedColor.red;
        }
        if (color.Equals(new Color(1, 0.65f, 0, 0.9f)))
        {
            return CustomizedColor.orange;
        }
        if (color.Equals(new Color(1, 0.92f, 0.016f, 0.9f)))
        {
            return CustomizedColor.yellow;
        }
        if (color.Equals(new Color(0, 1, 0, 0.9f)))
        {
            return CustomizedColor.green;
        }
        if (color.Equals(new Color(0, 0, 1, 0.9f)))
        {
            return CustomizedColor.blue;
        }
        if (color.Equals(new Color(0.5f, 0, 0.5f, 0.9f)))
        {
            return CustomizedColor.violet;
        }
        return CustomizedColor.white;
    }


}
