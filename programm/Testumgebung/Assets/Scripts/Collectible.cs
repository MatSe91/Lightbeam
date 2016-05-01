using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool collected = false;
    private bool connected;
    private Collider other;

    public bool Collected
    {
        get
        {
            return collected;
        }

        set
        {
            collected = value;
        }
    }

    /// <summary>
    /// Collect the collectible  on with trigger
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    { 
        other = col;
    }

    void OnTriggerStay(Collider col)
    {
        connected = true;
    }
    /// <summary>
    /// Uncollect the collectible if the collider is not the collictble object
    /// </summary>
    void Update()
    {

        // Das hier ist der größte Schrott, aber anders funktioniert es einfach nicht bei mir
        // vorallem 2. und 3. If, fehle eine davon spackt es wieder abartig rum

        if (connected && !collected)
        {
            collected = true;
        }

        if (other == null && !connected)
        {           
            collected = false;
            connected = false;
        }

        if (other == null && connected)
        {
            connected = false;
        }
    }
}
