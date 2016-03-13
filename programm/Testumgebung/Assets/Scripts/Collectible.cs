using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Collectible : MonoBehaviour
{
    public bool collected = false;
    private Collider other;

    void OnTriggerEnter(Collider col)
    {

        collected = true;
        Debug.Log("enter");
        other = col;

    }

    void OnTriggerExit(Collider col)
    {

        collected = true;
        Debug.Log("exit");
        other = col;

    }
}
