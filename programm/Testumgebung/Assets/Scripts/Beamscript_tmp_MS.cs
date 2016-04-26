using UnityEngine;
using System.Collections.Generic;
using DigitalRuby.FastLineRenderer;
using System;

public class Beamscript_tmp_MS : MonoBehaviour
{
    // necessary Tags
    private string mirrorTag;
    private string colorMirrorTag;
    private string doorKnopTag;
    private string colorChangerTag;
    private string checkPointTag;
    private string endPointTag;

    // necessary Layers
    private LayerMask waterLayer;

    // Lines
    private FastLineRenderer r;
    private FastLineRendererProperties property;
    private List<FastLineRendererProperties> properties;

    // Line proberties
    private Vector3 curPosition;
    private Vector3 dir;
    private bool beamIsInWater;
    public float lineRadius = 0.1f;

    // external Scripts
    private BeamRefraction br;
    private DoorOpener doorOpener;

    private GameObject oldCheckPoint;
    private GameObject oldDoorKnop;
    private GameObject endpoint;

    private bool isActive;
    private Color intensitive;
    private CustomColor.CustomizedColor previousColor;
    private bool touched = false;



    // Use this for initialization
    void Start()
    {
        r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());
        properties = new List<FastLineRendererProperties>();
        intensitive = new Color(0, 0, 0, 0.7f);
        property = new FastLineRendererProperties();

        // layer
        waterLayer = LayerMask.NameToLayer("Water");

        // tags
        mirrorTag = "Mirror";
        colorMirrorTag = "ColorMirror";
        doorKnopTag = "Doorknop";
        colorChangerTag = "ColorChanger";
        checkPointTag = "Checkpoint";
        endPointTag = "Endpoint";
    }

    // Update is called once per frame
    void Update()
    {
        touched = InputManager.touchInput;

        if (r != null)
        {
            r.Reset();
            BeamCollider.OnDestroy();
            properties.Clear();

            property = new FastLineRendererProperties();
            beamIsInWater = false;
            isActive = true;
        }

        curPosition = transform.position;
        property.Start = curPosition;
        standardPropertyOfBeam();
        property.Color = CustomColor.GetColor(CustomColor.CustomizedColor.red);
        dir = transform.right;

        while (isActive)
        {
            previousColor = CustomColor.GetCustomColor(property.Color);
            RaycastHit hit;
            if (Physics.Raycast(curPosition, dir, out hit))
            {
                // if beam hits a mirror in general
                #region mirror
                if (hit.transform.gameObject.tag == mirrorTag)
                {
                    setMirrorReflection(hit, true, Vector3.Reflect(dir, hit.normal), previousColor);
                }
                // if beam hits a colored Mirror
                else if (hit.transform.gameObject.tag == colorMirrorTag)
                {
                    dir = hit.transform.gameObject.GetComponentInParent<ColorMirror>().GetReflection(CustomColor.GetCustomColor(property.Color), dir, hit);
                    setMirrorReflection(hit, true, dir, previousColor);
                }                
                else
                {
                    isActive = false;
                }

                #endregion
                // if beam hit Color Changer Gem
                if (hit.transform.gameObject.tag == colorChangerTag)
                {
                    setMirrorReflection(hit, true, dir, hit.transform.gameObject.GetComponent<ChangeBeamColor>().getNewBeamColor());
                }

                #region Door 
                // if beam hits doorKnop       
                if (hit.transform.gameObject.tag == doorKnopTag)
                {
                    BeamConnectivity(hit.transform.gameObject, true, doorKnopTag);
                    oldDoorKnop = hit.transform.gameObject;
                    doorOpener = hit.transform.gameObject.GetComponent<DoorOpener>();
                    doorOpener.CollisionColor = CustomColor.GetCustomColor(property.Color);
                }

                else
                {
                    if (oldDoorKnop != null)
                    {
                        BeamConnectivity(oldDoorKnop, false, doorKnopTag);
                    }
                }
                #endregion

                // If beam hit Water surface
                if (hit.transform.gameObject.layer.Equals(waterLayer))
                {
                    setEndPointOfLine(hit, true);

                    if (!beamIsInWater)
                    {
                        br = new BeamRefraction(dir, hit, Refraction_Medium.Refraction_Med.air, Refraction_Medium.Refraction_Med.water);
                        dir = br.getDir();
                        beamIsInWater = br.getLineInWater();
                    }
                    else
                    {
                        br = new BeamRefraction(dir, hit, Refraction_Medium.Refraction_Med.water, Refraction_Medium.Refraction_Med.air);
                        dir = br.getDir();
                        beamIsInWater = br.getLineInWater();
                    }
                    setStartPointOfLine(previousColor);
                }

                // if beam hit checkPoint
                if (hit.transform.gameObject.tag == checkPointTag)
                { 
                    BeamConnectivity(hit.transform.gameObject, true, checkPointTag);
                    oldCheckPoint = hit.transform.gameObject;
                }
                else
                {
                    if (oldCheckPoint != null)
                        BeamConnectivity(oldCheckPoint, false, checkPointTag);
                }

                // is beam in water?
                if (beamIsInWater)
                {
                    property.lineInWater = true;
                }
                property.End = hit.point;

            }
            else
            {
                isActive = false;
                property.End = curPosition + dir * 30;

                if (oldCheckPoint != null)
                {
                    BeamConnectivity(oldCheckPoint, false, checkPointTag);
                }

                if (oldDoorKnop != null)
                {
                    BeamConnectivity(oldDoorKnop, false, doorKnopTag);
                }

                if (endpoint != null)
                {
                    BeamConnectivity(endpoint, false, endPointTag);
                }
            }
        }
        properties.Add(property);
        addLines();

    }
    //}

    #region helpers

    public List<FastLineRendererProperties> Properties
    {
        get
        {
            return properties;
        }

        set
        {
            properties = value;
        }
    }

    public FastLineRenderer R
    {
        get
        {
            return r;
        }

        set
        {
            r = value;
        }
    }

    private void setMirrorReflection(RaycastHit hit, bool isAcitve, Vector3 direction, CustomColor.CustomizedColor color)
    {
        setEndPointOfLine(hit, isAcitve);
        dir = direction;
        setStartPointOfLine(color);
    }

    private void setEndPointOfLine(RaycastHit hit, bool isActive)
    {
        if (touched)
        {
            this.isActive = isActive;
            curPosition = hit.point;
            property.End = curPosition;
        }
    }
    private void setStartPointOfLine(CustomColor.CustomizedColor customColor)
    {
        if (touched)
        {
            Properties.Add(property);
            property = new FastLineRendererProperties();
            standardPropertyOfBeam();
            property.Color = CustomColor.GetColor(customColor);
            property.Start = curPosition;
        }
    }

    private void BeamConnectivity(GameObject gObject, bool value, string tag)
    {
        if (tag == endPointTag)
        {
            gObject.GetComponent<Endpoint>().setBeamConnectivity(value);

        }
        else if (tag == doorKnopTag)
        {
            gObject.GetComponent<DoorOpener>().SetBeamConnected(value);
        }
        else if (tag == checkPointTag)
        {
            gObject.GetComponent<CheckPointManager>().setBeamConnectivity(value);
        }

    }

    private void standardPropertyOfBeam()
    {
        property.Radius = lineRadius;
    }

    private void reduceBeamIntencity(FastLineRendererProperties pro)
    {
        if (pro.lineInWater)
        {
            pro.Color -= intensitive;
        }
    }
    private void addLines()
    {
        if (touched)
        {
            foreach (var prop in Properties)
            {
                reduceBeamIntencity(prop);
                BeamCollider.AddColliderToLine(prop.Start, prop.End, r);
                r.AddLine(prop);
            }
            r.Apply(true);
        }
    }   
    #endregion
}
