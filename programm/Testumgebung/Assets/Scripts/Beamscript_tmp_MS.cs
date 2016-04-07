using UnityEngine;
using System.Collections.Generic;
using DigitalRuby.FastLineRenderer;

public class Beamscript_tmp_MS : MonoBehaviour
{
    // necessary Tags
    private string mirrorTag;
    private string colorMirrorTag;
    private string doorKnopTag;
    private string colorChangerTag;

    // necessary Layers
    private LayerMask checkPointLayer;
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
    private GameObject sameDoorKnop;
    private bool isActive;
    private Color intensitive;
    private int doorCounter = 0;
    private bool collideWithDoor;
    private CustomColor.CustomizedColor previousColor;


    // Use this for initialization
    void Start()
    {
        r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());
        properties = new List<FastLineRendererProperties>();
        intensitive = new Color(0, 0, 0, 0.7f);

        // layer
        checkPointLayer = LayerMask.NameToLayer("Checkpoint");
        waterLayer = LayerMask.NameToLayer("Water");

        // tags
        mirrorTag = "Mirror";
        colorMirrorTag = "ColorMirror";
        doorKnopTag = "Doorknop";
        colorChangerTag = "ColorChanger";
    }

    // Update is called once per frame
    void Update()
    {
        collideWithDoor = false;
        //if (InputManager.touchInput)
        //{
            if (r != null)
            {
            r.Reset();
            BeamCollider.OnDestroy();
            properties.Clear();

            property = new FastLineRendererProperties();
            beamIsInWater = false;
        }

        isActive = true;
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
                // if beam hits dorKnop    
                // TODO Fixen Roman :)          
                if (hit.transform.gameObject.tag == doorKnopTag)
                {
                    doorOpener = hit.transform.gameObject.GetComponent<DoorOpener>();
                    collideWithDoor = true;

                    setEndPointOfLine(hit, false);

                    if (doorCounter == doorOpener.counter - 1)
                    {
                        doorOpener.OpenDoor(CustomColor.GetCustomColor(property.Color));
                        collideWithDoor = false;
                        sameDoorKnop = hit.transform.gameObject;
                    }
                    setStartPointOfLine(previousColor);
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
                if (hit.transform.gameObject.layer.Equals(checkPointLayer))
                { 
                    BeamConnectivity(hit.transform.gameObject, true);
                    oldCheckPoint = hit.transform.gameObject;
                }
                else
                {
                    if (oldCheckPoint != null)
                        BeamConnectivity(oldCheckPoint, false);
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
                property.End = curPosition + dir * 20;

                if (oldCheckPoint != null)
                {
                    BeamConnectivity(oldCheckPoint, false);
                }

                if (sameDoorKnop != null)
                {
                    doorOpener = sameDoorKnop.GetComponent<DoorOpener>();
                    if (doorOpener.ConstantTrigger && doorOpener.dooIsOpen)
                    {
                        Debug.Log("Animation Door close");
                        doorOpener.dooIsOpen = false;
                    }
                    
                }
            }
        }
        properties.Add(property);
        addLines();
        setDoorCounter();

    }
    //}

    #region helpers
    private void setMirrorReflection(RaycastHit hit, bool isAcitve, Vector3 direction, CustomColor.CustomizedColor color)
    {
        setEndPointOfLine(hit, isAcitve);
        dir = direction;
        setStartPointOfLine(color);

    }

    private void setEndPointOfLine(RaycastHit hit, bool isActive)
    {
        this.isActive = isActive;
        curPosition = hit.point;
        property.End = curPosition;
    }
    private void setStartPointOfLine(CustomColor.CustomizedColor customColor)
    {
        properties.Add(property);
        property = new FastLineRendererProperties();
        standardPropertyOfBeam();
        property.Color = CustomColor.GetColor(customColor);
        property.Start = curPosition;
    }

    private void BeamConnectivity(GameObject checkP, bool bo)
    {
        checkP.GetComponent<CheckPointManager>().setBeamConnectivity(bo);
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
        foreach (var prop in properties)
        {
            reduceBeamIntencity(prop);
            BeamCollider.AddColliderToLine(prop.Start, prop.End, r);
            r.AddLine(prop);
        }
        r.Apply(true);
    }
    private void setDoorCounter()
    {
        if (collideWithDoor)
        {
            doorCounter++;
        }
        else
        {
            doorCounter = 0;
        }
    }
    #endregion
}
