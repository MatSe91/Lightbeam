using UnityEngine;
using System.Collections;

public class Coll_Pflanze_Script : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        print("Do Something awesome like an animation!");
        Destroy(col.gameObject);

        print(col.transform.gameObject);
    }
}
