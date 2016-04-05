using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ChangeBeamColor : MonoBehaviour {

    public CustomColor.CustomizedColor newColor;

    public CustomColor.CustomizedColor getNewBeamColor()
    {
        return newColor;
    }
}
