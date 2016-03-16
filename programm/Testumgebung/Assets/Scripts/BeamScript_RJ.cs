using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace DigitalRuby.FastLineRenderer
{
    public class BeamScript_RJ : MonoBehaviour
    {
        [Tooltip("Tag an dem der Spiegel reflektiert.")]
        public string MirrorTag;
       
        [Tooltip("Hier wird der Layer des Collectable ausgewählt")]
        public LayerMask checkPointLayer;

        private FastLineRenderer r;
        private FastLineRendererProperties property;
        private BeamRefraction br;
        private List<FastLineRendererProperties> properties;

        private GameObject sameObject;
        private bool isActive;
        private Vector3 curPosition;
        private Vector3 dir;
        private bool beamIsInWater = false;
        Color intensitive;
      

        // Use this for initialization
        void Start()
        {
            r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());
            property = new FastLineRendererProperties();
          ;
           
            intensitive = new Color(0, 0, 0, 0.7f);
            br = new BeamRefraction();

        }

        // Update is called once per frame
        void Update()
        {
            if (r != null)
            {
                r.Reset();
                properties = new List<FastLineRendererProperties>();
                
            }



            isActive = true;
            curPosition = transform.position;
            property.Start = curPosition;
            standardColor();
            dir = transform.right;

            while (isActive)
            {
                RaycastHit hit;
                if (Physics.Raycast(curPosition, dir, out hit))
                {
                    if (hit.transform.gameObject.tag == MirrorTag)
                    {
                        isActive = true;
                        curPosition = hit.point;
                        dir = Vector3.Reflect(dir, hit.normal);
                        property.End = curPosition;
                        //BeamCollider.AddColliderToLine(property.Start, property.End, r);
                        properties.Add(property);
                        property = new FastLineRendererProperties();
                        standardColor();
                        // r.AddLine(property);
                        property.Color = Color.green;
                        if (beamIsInWater)
                        {
                            reduceBeamIntencity();
                        }
                        property.Start = curPosition;
                    }
                    else
                    {
                        isActive = false;
                    }

                    if (hit.transform.gameObject.layer == 4)
                    {
                        //Beam befindet sich noch nicht im Wasser
                        if (!beamIsInWater)
                        {

                            dir = br.refraction_Angle(Vector3.Angle(dir, hit.normal), hit, Refraction_Medium.air, Refraction_Medium.water);

                            curPosition = hit.point;
                            property.End = curPosition;
                            //BeamCollider.AddColliderToLine(property.Start, property.End, r);
                            properties.Add(property);
                            property = new FastLineRendererProperties();
                            standardColor();
                            // r.AddLine(property);
                            property.Color = Color.blue;

                            //property.Color = property.Color - intensitive;
                            property.Start = curPosition;

                            beamIsInWater = true;

                        }
                        else    // Beam befindet sich bereits im Wasser
                        {
                            beamIsInWater = false;
                        }

                        isActive = true;
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
                    //properties.Add(property);

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


        private void standardColor()
        {
            property.Radius = 0.1f;
            property.Color = Color.red;
        }

       

        private void reduceBeamIntencity()
        {
            property.Color = property.Color - intensitive;
        }

        private void addLines()
        {
            properties.Add(property);

            foreach (var prop in properties)
            {
                r.AddLine(prop);
               
                 BeamCollider.AddColliderToLine(prop.Start,prop.End,r);
               
            }
            r.Apply(true);

        }

      



    }

}



