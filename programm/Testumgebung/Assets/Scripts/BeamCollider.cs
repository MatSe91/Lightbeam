using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

namespace DigitalRuby.FastLineRenderer
{
    public  class BeamCollider : MonoBehaviour
    {
       
        // Use this for initialization

        public static void     AddColliderToLine(Vector3 startPos, Vector3 endPos, FastLineRenderer r)//,GameObject Prefab)
        {

            // GameObject Beamcollider = Instantiate(Prefab);
            // GameObject Beamcollider = new GameObject("Beamcollider");
            BoxCollider box = new GameObject("Collider").AddComponent<BoxCollider>();
            box.transform.parent = r.transform;
            //BoxCollider box = Beamcollider.AddComponent<BoxCollider>();
            // BoxCollider box = Beamcollider.GetComponent<BoxCollider>();
            //Vector3 midPoint = (startPos + endPos) / 2;
            // box.transform.position = midPoint; // Collider is added as child object of line
            float lineLength = Vector3.Distance(startPos, endPos); // length of line
            
            
           box.size = new Vector3(lineLength , 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
           // box.center = new Vector3(box.size.x / 2, 0.0f, 0f);
           
            Vector3 midPoint = (startPos + endPos) / 2;
            box.transform.position = midPoint; // setting position of collider object
                                               // Following lines calculate the angle between startPos and endPos
            float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
            if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
            {
                angle *= -1;
            }
            angle = Mathf.Rad2Deg * Mathf.Atan(angle);

            
            box.transform.eulerAngles = new Vector3(0, 0, angle);
            // box.tag = "BeamCollider";
            box.gameObject.layer = 9;
            box.enabled = true;
            //Debug.Log("size" + box.size);
            //Debug.Log("center" + box.center);
            //Debug.Log("position" + box.transform.position);
            //Debug.Log("rotation" + box.transform.rotation);
            //Debug.Log("angle" + angle);
            
            //Debug.Log(box.tag);
            //return Beamcollider;

        }

        //public  static void DeleteCollider(IEnumerable  collider)
        //{
        //    foreach (GameObject coll in collider)
        //    {
        //        Destroy(coll);
        //    }
        //}
    }
}