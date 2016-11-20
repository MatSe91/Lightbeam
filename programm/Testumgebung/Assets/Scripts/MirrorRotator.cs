using UnityEngine;

public class MirrorRotator : MonoBehaviour
{
    private bool active;
    private Vector3 pos;
    private Vector3 dir;
    private float angle;
    public GameObject touchAnimGameObject;

    private float minZ;
    private float maxZ;
    private int heldDown = 0;
    public int framesTillMovable = 8;

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
    }

   /// <summary>
   ///  Setzt das Objekt auf Aktiv oder Passiv,
   ///  hierbei wird der Methode true oder false übergeben.
   /// </summary>
   /// <param name="rot"></param>
    public void setActiveGameObject(bool rot)
    {
        active = rot;
        if (active)
        {
            if (touchAnimGameObject != null)
            {
                touchAnimGameObject.SetActive(true);
            }
        }
        else
        {
            touchAnimGameObject.SetActive(false);
        }
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
                // wenn heldDown == 7 dann kann man das Obejkt bewegen
                if (heldDown < framesTillMovable) heldDown++;
                else
                {
                    pos = Camera.main.WorldToScreenPoint(transform.position);
                    dir = Input.mousePosition - pos;
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    angle = SmoothAngle(angle);

                    transform.localEulerAngles = new Vector3(0, 0, ClampAngle(angle, minZ, maxZ));
                }
            }
            else
            {
                heldDown = 0;
            }
        }     
    }

    private float SmoothAngle(float anle)
    {
        if (angle < 0)
            angle += 360;
        return angle;
    }

    private float ClampAngle(float angle, float min, float max)
    {

        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }

        angle = Mathf.Clamp(angle, min, max);

        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }

    //private float ClampAngle(float angle, float min, float max)
    //{
    //    while (max < min) max += 360f;
    //    while (angle > max) angle -= 360f;
    //    while (angle < min) angle += 360f;

    //    if (angle > max)
    //    {
    //        if (angle - (max + min) * 0.5 < 180.0)
    //            return max;
    //        else
    //            return min;
    //    }
    //    else
    //        return angle;
    //}

}
