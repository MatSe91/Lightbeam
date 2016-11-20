using UnityEngine;

public class ChangeBeamColor : MonoBehaviour {

    public CustomColor.CustomizedColor newColor;
    private bool isChanged;

    public bool IsChanged
    {
        get
        {
            return isChanged;
        }

        set
        {
            isChanged = value;
        }
    }

    public CustomColor.CustomizedColor getNewBeamColor()
    {
        return newColor;
    }

    public ChangeBeamColor Reflect(CustomColor.CustomizedColor collisionColor)
    {
       
        IsChanged = false;
        if (collisionColor == newColor)
        {
            isChanged = true;
        }

        return this;
    }
}
