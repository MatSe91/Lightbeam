using UnityEngine;
using System.Collections;
using System;

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

        private GameObject sameObject;
        private bool isActive;
        private Vector3 curPosition;
        private Vector3 dir;


        // Use this for initialization
        void Start()
        {
            r = FastLineRenderer.CreateWithParent(null, GetComponent<FastLineRenderer>());
            property = new FastLineRendererProperties();
            br = new BeamRefraction();
            //Refraction_Medium.air;
            
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
                        r.AddLine(property);
                        property.Color = Color.green;
                        property.Start = curPosition;
                    }
                    else
                    {
                        isActive = false;
                    }

                 if(hit.transform.gameObject.layer== 4)
                    {
                        isActive = true;
                        print("Do something");

                        Vector3 beta = br.refraction_Angle(Vector3.Angle(dir, hit.normal), -hit.normal, Refraction_Medium.air, Refraction_Medium.water);
                        Debug.Log("Alpha winkel: " + Vector3.Angle(-dir, hit.normal));
                        Debug.DrawRay(hit.point, beta);
                        Debug.DrawRay(hit.point, -hit.normal, Color.black);
                        Debug.Log(-hit.normal);

                        curPosition = hit.point;
                        property.End = curPosition;
                        //BeamCollider.AddColliderToLine(property.Start, property.End, r);
                        r.AddLine(property);
                        property.Color = Color.blue;
                        property.Start = curPosition;
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
            //BeamCollider.AddColliderToLine(property.Start, property.End, r);
            r.AddLine(property);
            r.Apply(true);

        }

        private void standardColor()
        {
            property.Radius = 0.1f;
            property.Color = Color.red;
        //   BeamRefraction br = new BeamRefraction();
          // br.refraction_Angle(45,Refraction_Medium.water,Refraction_Medium.air);
        }
    }

}

public class BeamRefraction
{ 
    public const float REFRACTION_INDEX_WATER = 1.33f;
    public const float REFRACTION_INDEX_PRISMA = 2.41f;  // Prisma ist in diesem Fall der Diamant aus Latex Dikument (S.20)
    public const float REFRACTION_INDEX_GLASS = 1.52f;
    public const float REFRACTION_INDEX_AIR = 1f;

    private float alpha;                     // Winkel, wie der Lichtstarhl auf das Objekt auftrifft
    private float beta;                      // Winkel, wie der Lichtstrahl abgeleitet wird







    /// <summary>
    /// Der Alpha - Winkel, welcher der Eintreffende Lichtstrahl ist. <para/>
    /// n1 ist immer das aktuelle Refraction_Medium.<para/>
    /// n2 ist immer das neue Refraction_Medium, also wohin das Licht geht. <para/>
    /// return Austrittswinkel beta, oder 0 wenn keine Brechung stattfindet.
    /// </summary>
    /// <param name="alpha"> Alpha - Winkel</param>
    /// <param name="n1"> ist immer das aktuelle Refraction_Medium</param>
    /// <param name="n2"> ist immer das neue Refraction_Medium, also wohin das Licht geht</param>
    /// <returns></returns>
    public Vector3 refraction_Angle(float alpha, Vector3 normale, Refraction_Medium rm1, Refraction_Medium rm2)
    {
        Vector3 dir;
        float n1 = refraction_Index(rm1);
        float n2 = refraction_Index(rm2);
        float beta = 0;
        float totalReflection;

        //  n1 = dünner Stoff, n2 = dichter Stoff
        if (n1 < n2)
        {
            beta = Mathf.Rad2Deg * (Mathf.Asin(((Mathf.Sin(alpha * Mathf.Deg2Rad)) * n1) / n2));
        }
        else //  n1 = dichter Stoff, n2 = dünner Stoff
        {
            // TODO Prüfen ob der max winkel noch nicht erreicht ist --> wenn doch keine Brechung, nur reflexion

            totalReflection = Mathf.Asin(n2 / n1) * Mathf.Rad2Deg;
            if (totalReflection < alpha)
            {
                return new Vector3(0,0,0);
            }
            beta = Mathf.Rad2Deg * (Mathf.Asin(((Mathf.Sin(alpha * Mathf.Deg2Rad)) * n1) / n2));
        }

        Quaternion a = Quaternion.AngleAxis(beta, normale);
        Debug.Log("Winkel Beta: " + beta);
        return dir = a * Vector3.forward;
    }

    /// <summary>
    /// Gibt den Brechungsindex von Wasser, Glas, Luft und Prisma zurück. <para/>
    /// Wenn nichts passt oder keine Brechung stattfindet --> return -1f (sollte nicht passieren)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private float refraction_Index(Refraction_Medium index)
    {
        if (index == Refraction_Medium.water)
            return REFRACTION_INDEX_WATER;
        else if (index == Refraction_Medium.prism)
            return REFRACTION_INDEX_PRISMA;
        else if (index == Refraction_Medium.glass)
            return REFRACTION_INDEX_GLASS;
        else if (index == Refraction_Medium.air)
            return REFRACTION_INDEX_AIR;
        else
            return -1f;
    }
}

public enum Refraction_Medium
{
    water,
    air,
    glass,
    prism
}


