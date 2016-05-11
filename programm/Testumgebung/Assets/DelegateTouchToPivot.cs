using UnityEngine;
using System.Collections;

public class DelegateTouchToPivot : MonoBehaviour {

    private MirrorRotator mirRot;

	// Use this for initialization
	void Start () {

        mirRot = GetComponentInParent<MirrorRotator>();
	
	}

    public void setActiveGameObject(bool rot)
    {
        mirRot.setActiveGameObject(rot);
    }

}
