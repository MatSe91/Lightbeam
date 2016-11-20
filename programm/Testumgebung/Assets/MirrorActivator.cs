using UnityEngine;
using System.Collections;

public class MirrorActivator : MonoBehaviour
{

   

    void OnMouseDown()
    {
        this.gameObject.GetComponentInParent<MirrorRotator>().setActiveGameObject(true);
        Destroy(this.gameObject);
    }


}
