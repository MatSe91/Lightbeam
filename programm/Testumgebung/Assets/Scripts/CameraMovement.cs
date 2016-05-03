using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour {

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

    // Hardcodierter Kack --> müsste aus der Kamera ausgelesen werden
    private float getValue()
    {
        if (numberOfAreas.Equals(Area.two))
        {
            return 27f;
        }
        else if (numberOfAreas.Equals(Area.three))
        {
            return 54f;
        }
        return 0f;
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

