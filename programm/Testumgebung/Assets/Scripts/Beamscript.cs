using UnityEngine;
using System.Collections.Generic;
using DigitalRuby.FastLineRenderer;
using System;

public class Beamscript : MonoBehaviour
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
    public float lineRadius = 0.06f;

    [Range(0.3f, 0.5f)]
    public float jitterMultiplier = 0.4f;
    public float glowUVXScale = 1;
    public float glowUVYScale = 1;
    public Color glowColor = new Color(1, 1, 1, 0.392f);
    public float globalGlowIntensityMultiplier;
    public float globalGlowWidthMultiplier;
    public float singleLineGlowIntensityMultiplier;
    public float singleLineGlowWidthMultiplier;
    public CustomColor.CustomizedColor startColor = CustomColor.CustomizedColor.white;

    // external Scripts
    private BeamRefraction br;
    private DoorOpener doorOpener;

    private GameObject oldCheckPoint;
    private GameObject oldDoorKnop;
    private GameObject endpoint;

    private bool isActive;
    private Color intensitive = new Color(0, 0, 0, 0.3f);
    private CustomColor.CustomizedColor previousColor;
    private bool touched = false;



    // Use this for initialization
    void Start()
    {
        initialAll();
    }

    private void initialAll()
    {
        initialLine();
        initialLayers();
        initialTags();
        initialGlobalProperties();
    }

    private void initialLayers()
    {
        waterLayer = LayerMask.NameToLayer("Water");
    }

    private void initialLine()
    {
        r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());
        properties = new List<FastLineRendererProperties>();
        property = new FastLineRendererProperties();
    }

    private void initialGlobalProperties()
    {
        r.GlowUVXScale = glowUVXScale;
        r.GlowUVYScale = glowUVYScale;
        r.JitterMultiplier = jitterMultiplier;
        r.GlowColor = glowColor;
        r.GlowIntensityMultiplier = globalGlowIntensityMultiplier;
        r.GlowWidthMultiplier = globalGlowWidthMultiplier;
    }

    private void initialTags()
    {
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
       // touched = true;

        if (r != null && touched)
        {
            resetLine();
        }
        curPosition = transform.position;
        property.Start = curPosition;
        standardPropertyOfBeam();
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
                    setMirrorReflection(hit, true, hit.transform.gameObject.GetComponentInParent<ColorMirror>().GetReflection(previousColor, dir, hit), previousColor);
                }
               // if beam hit Color Changer Gem
               else if (hit.transform.gameObject.tag == colorChangerTag)
                {
                    setMirrorReflection(hit, true, dir, hit.transform.gameObject.GetComponent<ChangeBeamColor>().getNewBeamColor());
                }
                else
                {
                    isActive = false;
                }

                #endregion

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

                #region endPoint
                if (hit.transform.gameObject.tag == endPointTag)
                {
                    BeamConnectivity(hit.transform.gameObject, true, endPointTag);
                    endpoint = hit.transform.gameObject;
                }
                else
                {
                    if (endpoint != null)
                    {
                        BeamConnectivity(endpoint, false, endPointTag);
                    }
                }
                #endregion

                // If beam hit Water surface
                if (hit.transform.gameObject.layer.Equals(waterLayer))
                {
                    setEndPointOfLine(hit, true);
                    refractBeam(hit);
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

                checkBeamConectivity();
            }
        } // end while
        properties.Add(property);
        addLines();

    }

    private void refractBeam(RaycastHit hit)
    {
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
    }

    private void checkBeamConectivity()
    {
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

    private void resetLine()
    {
        r.Reset();
        BeamCollider.OnDestroy();
        properties.Clear();

        property = new FastLineRendererProperties();
        property.LineType = FastLineRendererLineSegmentType.StartCap;
        property.Color = CustomColor.GetColor(startColor);

        isActive = true;
        beamIsInWater = false;
    }

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
            property.Color = CustomColor.GetColor(customColor);
            standardPropertyOfBeam();
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
            pro.GlowIntensityMultiplier = singleLineGlowIntensityMultiplier;
            pro.GlowWidthMultiplier = singleLineGlowWidthMultiplier;
        }
    }
    private void addLines()
    {
        if (touched)
        {
            createLine();
            r.Apply(true);
        }
    }

    private void createLine()
    {
        lineProbs(0);
        properties[0].LineType = FastLineRendererLineSegmentType.StartCap;
        r.StartLine(properties[0]);
        appendLine();
    }

    private void appendLine()
    {
        for (int i = 1; i < properties.Count; i++)
        {
            lineProbs(i);
            properties[i].LineType = FastLineRendererLineSegmentType.Full;
            properties[i].Start = properties[i].End;

            r.AppendLine(properties[i]);

        }
    }

    private void lineProbs(int i)
    {
        reduceBeamIntencity(properties[i]);
        BeamCollider.AddColliderToLine(properties[i].Start, properties[i].End, r);
    }
    #endregion
}
