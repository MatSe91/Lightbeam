using UnityEngine;
using System.Collections;
using System;

namespace DigitalRuby.FastLineRenderer
{
    public class Beamscript : MonoBehaviour
    {
        [Tooltip("Tag an dem der Spiegel reflektiert.")]
        public string MirrorTag;

        [Tooltip("Hier wird der Layer des Collectable ausgewählt")]
        public LayerMask checkPointLayer;

        private FastLineRenderer r;
        private FastLineRendererProperties property;

        private GameObject sameObject;
        private bool isActive;
        private Vector3 curPosition;
        private Vector3 dir;


        // Use this for initialization
        void Start()
        {
            r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());       
            property = new FastLineRendererProperties();
        }

        // Update is called once per frame
        void Update()
        {
            if (r != null)
            {
                r.Reset();
            }

            standardColor();


            isActive = true;
            curPosition = transform.position;
            property.Start = curPosition;
            dir = transform.right;

            while (isActive)
            {
            RaycastHit hit;
                if (Physics.Raycast(curPosition, dir, out hit))
                {
                    if (hit.transform.gameObject.tag == MirrorTag)
                    {
                        curPosition = hit.point;
                        dir = Vector3.Reflect(dir, hit.normal);
                        //Debug.DrawRay(curPosition, dir * 20, Color.magenta);
                        property.End = curPosition;
                        BeamCollider.AddColliderToLine(property.Start, property.End, r);                    
                        r.AddLine(property);
                        property.Color = Color.green;
                        property.Start = curPosition;
                    }
                    else
                    {
                        isActive = false;
                    }

                    // Prüfe ob die angegebene Maske mit der Maske im hit übereinstimmt
                    if ((checkPointLayer.value & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)
                    {
                        BeamConnectivity(hit.transform.gameObject, true);
                        sameObject = hit.transform.gameObject;
                    }

                    property.End = hit.point;              

                }
                else
                {
                    isActive = false;
                    property.End = curPosition + dir * 20;

                    if (sameObject != null)
                    {
                        BeamConnectivity(sameObject, false);
                    }
                }
            }        
            addLines();
        }

        private void BeamConnectivity(GameObject checkP, bool bo)
        {
            checkP.GetComponent<CheckPointManager>().setBeamConnectivity(bo);
        }

        private void addLines()
        {
            BeamCollider.AddColliderToLine(property.Start, property.End, r);
            r.AddLine(property);
            r.Apply(true);

        }

        private void standardColor()
        {
            property.Radius = 0.1f;
            property.Color = Color.red;
        }
    }

}