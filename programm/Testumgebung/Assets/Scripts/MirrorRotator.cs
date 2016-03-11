using UnityEngine;

public class MirrorRotator : MonoBehaviour
{
    private bool active;
    private Vector3 pos;
    private Vector3 dir;
    private float angle;

    private float minZ;
    private float maxZ;
    private int heldDown = 0;



    // Use this for initialization
    void Start()
    {
        active = false;
        FindRotations();
    }
    
    /*
        Finde die Winkel (Grad), die das Objekt mindestens und maximal annehmen darf.
    */
    private void FindRotations()
    {
        minZ = transform.eulerAngles.z - 60;
        maxZ = transform.eulerAngles.z + 60;

        //   Debug.Log("Min Z: " + minZ);
        //   Debug.Log("Max Z: " + maxZ);

    }




   /// <summary>
   ///  Setzt das Objekt auf Aktiv oder Passiv,
   ///  hierbei wird der Methode true oder false übergeben.
   /// </summary>
   /// <param name="rot"></param>
    public void setActiveGameObject(bool rot)
    {
        active = rot;
    }


    void LateUpdate()
    {
        // Prüfe ob das Objekt Aktiv ist, wenn ja, dann darfst du agieren und bewegen
        // sonst setzte heldDown auf 0
        if (active)
        {

            // Wenn Linnke Maustaste gedrückt wird mache...
            if (Input.GetMouseButton(0))
            {
                // print(heldDown);

                // wenn heldDown == 7 dann kann man das Obejkt bewegen
                // problem hierbei ist das jedes Frame eine Abfrage gemacht wird und dadurch bei jedem Touch das Objekt bewegt werden würde
                // um das zu verhindert muss insgesamt 7 Frames gewartet werden, damit man das Objekt bewegen kann.
                if (heldDown < 7) heldDown++;
                else
                {
                    pos = Camera.main.WorldToScreenPoint(transform.position);
                    dir = Input.mousePosition - pos;
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    angle = SmoothAngle(angle);
                    // print("Winkel: "+angle);
                    transform.rotation = Quaternion.AngleAxis(ClampAngle(angle, minZ, maxZ), transform.forward);

                    //TODO: Spiegel können überdrehen, dadurch fangen sie erneut bei min / max an --> muss unterbunden werden
                }
            }
            // .. ansonsten setze den heldDown-Zähler zurück auf null (0)
            else
            {
                setHeldDownToZero();
            }
        }
        else
        {
            setHeldDownToZero();
        }


    }

    /*
        float angle: Winkel des Objektes in Bezug zum Touch
        float min : Kleinster Winkel, den das Objekt annimmt
        float max: größter Winkel, den das Objekt annimmt
        return float angle: Winkel, der vom Objekt angenommen wird (im Verhältnis zum Touch)
    */
    private float ClampAngle(float angle, float min, float max)
    {
        return Mathf.Clamp(angle, min, max);
    }

    /*
        Wenn der Winkel kleiner als 0 ist, dann addiere 360° hinzu --> für Berechnung
        float angle: Winkel des Inputtes
        return float angle: Korrekter Winkel des Inputtes
    */
    private float SmoothAngle(float anle)
    {
        if (angle < 0)
            angle += 360;
        return angle;
    }


    // Setze den heldDown auf 0
    private void setHeldDownToZero()
    {
        heldDown = 0;
    }

}
