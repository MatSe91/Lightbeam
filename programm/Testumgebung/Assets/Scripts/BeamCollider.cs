using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DigitalRuby.FastLineRenderer
{
    public  class BeamCollider : MonoBehaviour
    {
       
        // Use this for initialization

        public static void AddColliderToLine(Vector3 startPos, Vector3 endPos, FastLineRenderer r)//,GameObject Prefab)
        {
            if (r != null)
            {
                GameObject go = new GameObject("BeamCollider");
                BoxCollider box = /*new GameObject("BeamCollider").*/go.AddComponent<BoxCollider>();
                box.transform.parent = r.transform;
                float lineLength = Vector3.Distance(startPos, endPos); // length of line

                box.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement

                Vector3 midPoint = (startPos + endPos) / 2;
                box.transform.position = midPoint; // setting position of collider object
                                                   // Following lines calculate the angle between startPos and endPos
                float xPos = Mathf.Abs(startPos.x - endPos.x) > 0 ? Mathf.Abs(startPos.x - endPos.x) : 1;

                float angle = (Mathf.Abs(startPos.y - endPos.y) / xPos);
                if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
                {
                    angle *= -1;
                }
                angle = Mathf.Rad2Deg * Mathf.Atan(angle);
                box.transform.eulerAngles = new Vector3(0, 0, angle);
                box.gameObject.layer = 9;
                box.tag = "BoxColliderTag";
                box.enabled = true;
                go.hideFlags = HideFlags.DontSave;
            }
        }

        public static void OnDestroy()
        {
            List<GameObject> ColliderList = new List<GameObject>();
            ColliderList = GameObject.FindGameObjectsWithTag("BoxColliderTag").ToList();

            foreach (var item in ColliderList)
            {
                Destroy(item);
            }
        }
    }
}