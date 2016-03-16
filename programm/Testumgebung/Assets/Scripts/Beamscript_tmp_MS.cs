using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace DigitalRuby.FastLineRenderer
{
    public class Beamscript_tmp_MS : MonoBehaviour
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
        private bool beamIsInWater;
        Color intensitive;


        // Use this for initialization
        void Start()
        {
            r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());
            intensitive = new Color(0, 0, 0, 0.7f);            
        }

        // Update is called once per frame
        void Update()
        {
            if (r != null)
            {
                r.Reset();
                properties = new List<FastLineRendererProperties>();
                property = new FastLineRendererProperties();
                property.LineInWater = false;
                beamIsInWater = false;
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

                        properties.Add(property);
                        property = new FastLineRendererProperties();
                        standardColor();
                        property.Color = Color.green;
                        property.Start = curPosition;
                    }
                    else
                    {
                        isActive = false;
                    }

                    if(hit.transform.gameObject.layer == 4)
                    {
                        curPosition = hit.point;
                        property.End = curPosition;
                        properties.Add(property);
                        property = new FastLineRendererProperties();
                        standardColor();

                        if (!beamIsInWater)
                        {
                            BeamRefraction br = new BeamRefraction(dir, hit, Refraction_Medium.Refraction_Med.air, Refraction_Medium.Refraction_Med.water);
                            dir = br.getDir();
                            beamIsInWater = br.getLineInWater();
                        }
                        else
                        {
                            BeamRefraction br = new BeamRefraction(dir, hit, Refraction_Medium.Refraction_Med.water, Refraction_Medium.Refraction_Med.air);
                            dir = br.getDir();
                            beamIsInWater = br.getLineInWater();
                        }
                        property.Color = Color.blue;
                        property.Start = curPosition;
                        isActive = true;
                    }

                    // Prüfe ob die angegebene Maske mit der Maske im hit übereinstimmt
                    if ((checkPointLayer.value & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)
                    {
                        BeamConnectivity(hit.transform.gameObject, true);
                        sameObject = hit.transform.gameObject;
                    }

                    if (beamIsInWater)
                    {
                        property.LineInWater = true;
                    }
                    else
                    {
                        property.LineInWater = false;
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
            properties.Add(property);
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

        private void reduceBeamIntencity(FastLineRendererProperties pro)
        {
            if (pro.LineInWater)
                pro.Color = pro.Color - intensitive;
        }

        private void addLines()
        {
            foreach (var prop in properties)
            {
                reduceBeamIntencity(prop);
                r.AddLine(prop);
                //CreateCollider(prop);
            }
            r.Apply(true);
        }
    }
}