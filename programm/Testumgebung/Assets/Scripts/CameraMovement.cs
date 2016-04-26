using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {


    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float sensX = 100.0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 tmp = transform.position + new Vector3(Input.GetAxis("Mouse X") * sensX * Time.deltaTime, 0, 0);

            if (tmp.x >=minX && tmp.x <=maxX)
            {
                transform.position += new Vector3(Input.GetAxis("Mouse X") * sensX * Time.deltaTime, 0, 0);
            }
        }
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
