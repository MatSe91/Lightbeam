using UnityEngine;
using System.Collections;

public class MirrorColorReflection : MonoBehaviour {

    public Farbe lichtFarbe = Farbe.Weiss;

    public enum Farbe{     Weiss,
                    Rot,
                    Orange,
                    Gelb,
                    Gruen,
                    Hellblau,
                    Indigo,
                    Violett,
                    All
    }

    public Farbe getFarbe()
    {
        return this.lichtFarbe;
    }
    
}
