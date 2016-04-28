using UnityEngine;
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

    public static Color GetColor(CustomizedColor color)
    {
        int number = (int)color;
        switch (number)
        {
            case 0:
                return new Color(0.58f, 0.529f, 1, 0.902f); // white
            case 1:
                return new Color(1, 0, 0, 0.8f); // red
            case 2:
                return new Color(1, 0.65f, 0, 0.8f); // orange
            case 3:
                return new Color(1, 0.92f, 0.016f, 0.8f); // yellow
            case 4:
                return new Color(0, 1, 0, 0.8f); // green
            case 5:
                return new Color(0, 0, 1, 0.8f); // blue
            case 6:
                return new Color(0.5f, 0, 0.5f, 0.8f); // violet
            default:
                return new Color(0.58f, 0.529f, 1, 0.902f); // white

        }
    }

    public static CustomizedColor GetCustomColor(Color color)
    {
        if (checkColor(color, new Color(0.58f, 0.529f, 1, 0.902f)))
        {
            return CustomizedColor.white;
        }
        if (checkColor(color, new Color(1, 0, 0, 0.8f)))
        {
            return CustomizedColor.red;
        }
        if (checkColor(color, new Color(1, 0.65f, 0, 0.8f)))
        {
            return CustomizedColor.orange;
        }
        if (checkColor(color, new Color(1, 0.92f, 0.016f, 0.8f)))
        {
            return CustomizedColor.yellow;
        }
        if (checkColor(color, new Color(0, 1, 0, 0.8f)))
        {
            return CustomizedColor.green;
        }
        if (checkColor(color, new Color(0, 0, 1, 0.8f)))
        {
            return CustomizedColor.blue;
        }
        if (checkColor(color, new Color(0.5f, 0, 0.5f, 0.8f)))
        {
            return CustomizedColor.violet;
        }
        return CustomizedColor.white;
    }

    private static bool checkColor(Color c1, Color c2)
    {        
        if (Mathf.RoundToInt(c1.r) == Mathf.RoundToInt(c2.r) && Mathf.RoundToInt(c1.g) == Mathf.RoundToInt(c2.g) && Mathf.RoundToInt(c1.b) == Mathf.RoundToInt(c2.b))
        {
            return true;
        }
        return false;
    }

}
