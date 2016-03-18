using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Collectible : MonoBehaviour
{
    [Tooltip ("Angabe ob das Sammelobjekt bereits eingesammelt wurde")]
    public bool collected = false;
    private Collider other;

   /// <summary>
   /// Collect the collectible  on with trigger
   /// </summary>
   /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {

        collected = true;
        other = col;

    }
    /// <summary>
    /// Uncollect the collectible if the collider is not the collictble object
    /// </summary>
    void Update()
    {
        if (collected && !other)
        {
            collected = false;
        }
       

    }
}
