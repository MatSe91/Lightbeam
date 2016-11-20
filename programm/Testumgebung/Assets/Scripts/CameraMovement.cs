using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour {

    private const float X_VALUE_AREA_ONE = 0f;
    private const float X_VALUE_AREA_TWO = 27f;
    private const float X_VALUE_AREA_THREE = 54f;

    public Area numberOfAreas;
    private float minX = 0;
    public float sensX = 100.0f;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 tmp = transform.position + new Vector3(Input.GetAxis("Mouse X") * sensX * Time.deltaTime, 0, 0);

            if (tmp.x >= minX && tmp.x <= getValue())
            {
                transform.position += new Vector3(Input.GetAxis("Mouse X") * sensX * Time.deltaTime, 0, 0);
            }
        }
    }

    // Hardcodierter Kack --> m�sste aus der Kamera ausgelesen werden
    private float getValue()
    {
        if (numberOfAreas.Equals(Area.two))
        {
            return X_VALUE_AREA_TWO;
        }
        else if (numberOfAreas.Equals(Area.three))
        {
            return X_VALUE_AREA_THREE;
        }
        return X_VALUE_AREA_ONE;
    }


    /// <summary>
    /// Wenn value = true, dann wird der Animator disabled. Die Kamera ist frei beweglich.
    /// </summary>
    /// <param name="value"></param>
    private void disableCameraAnimator(bool value)
    {
        gameObject.GetComponent<Animator>().enabled = value;
    }
}

public enum Area
{
    one,
    two,
    three
}

