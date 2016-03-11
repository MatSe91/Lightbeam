using UnityEngine;
using System.Collections;

namespace DigitalRuby.FastLineRenderer
{
    public class BeamCollider : MonoBehaviour
    {

        // Use this for initialization

        private static BoxCollider box;



        public static void AddColliderToLine(Vector3 startPos, Vector3 endPos, FastLineRenderer r)
        {
            BoxCollider box = new GameObject("Collider").AddComponent<BoxCollider>();
            box.transform.parent = r.transform; // Collider is added as child object of line
            float lineLength = Vector3.Distance(startPos, endPos); // length of line
            box.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
            box.center = new Vector3(lineLength / 2, 0.0f, 0f);
            Vector3 midPoint = (startPos + endPos) / 2;
            box.transform.position = midPoint; // setting position of collider object
                                               // Following lines calculate the angle between startPos and endPos
            float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
            if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
            {
                angle *= -1;
            }
            angle = Mathf.Rad2Deg * Mathf.Atan(angle);
            box.transform.Rotate(0, 0, angle);
            //box.tag = "BeamCollider";
            box.enabled = true;
            Debug.Log(box.size);
            Debug.Log(box.center);
            //Debug.Log(box.tag);


        }
    }
}