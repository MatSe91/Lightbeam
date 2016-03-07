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
            property = new FastLineRendererProperties();
            r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());       
        }

        // Update is called once per frame
        void Update()
        {
            if (r != null)
            {
                r.Reset();
            }

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
                        standardColor();
                        
                        r.AddLine(property);
                        
                        property.Start = curPosition;
                    }
                    else
                    {
                        isActive = false;
                    }

                    // Prüfe ob die angegebene Maske mit der Maske im hit übereinstimmt
                    if ((checkPointLayer.value & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)
                    {
                        ActivateCheckPoint(hit.transform.gameObject, true);
                        sameObject = hit.transform.gameObject;
                    }


                    standardColor();
                    property.End = hit.point;

                    //Debug.DrawLine(curPosition, hit.point, Color.green);
                    //Debug.Log(hit.transform.gameObject);
                    

                }
                else
                {
                    isActive = false;
                   // Debug.DrawRay(curPosition, dir * 20, Color.blue);
                    property.End = curPosition + dir * 20;

                    if (sameObject != null)
                    {
                        ActivateCheckPoint(sameObject, false);
                    }
                }
            }
           

            addLines();
        }

        private void ActivateCheckPoint(GameObject checkP, bool bo)
        {
            checkP.GetComponent<CheckPTouchCollider>().setBeamConnectivity(bo);
        }

        private void addLines()
        {
            r.AddLine(property);
            r.Apply(true);

        }

        private void standardColor()
        {
            property.GlowIntensityMultiplier = 0;
            property.GlowWidthMultiplier = 0;
            property.Radius = 0.1f;
            property.Color = Color.red;
        }



    }

}