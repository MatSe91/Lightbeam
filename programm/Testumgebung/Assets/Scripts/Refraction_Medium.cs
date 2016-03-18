using UnityEngine;
using System.Collections;

/// <summary>
/// Die Brechungsmedien, welche es bei uns im Spiel gibt: water, air, glass, prism
/// </summary>
public class Refraction_Medium
{
    public const float REFRACTION_INDEX_WATER = 1.33f;
    public const float REFRACTION_INDEX_PRISMA = 2.41f;  // Prisma ist in diesem Fall der Diamant aus Latex Dokument (S.20)
    public const float REFRACTION_INDEX_GLASS = 1.52f;
    public const float REFRACTION_INDEX_AIR = 1f;

    public enum Refraction_Med {
        water,
        air,
        glass,
        prism
    }


    /// <summary>
    /// Gibt den Brechungsindex von Wasser, Glas, Luft und Prisma zurück. <para/>
    /// Wenn nichts passt oder keine Brechung stattfindet --> return -1f (sollte nicht passieren)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float refraction_Index(Refraction_Med index)
    {
        if (index == Refraction_Med.water)
            return REFRACTION_INDEX_WATER;
        else if (index == Refraction_Med.prism)
            return REFRACTION_INDEX_PRISMA;
        else if (index == Refraction_Med.glass)
            return REFRACTION_INDEX_GLASS;
        else if (index == Refraction_Med.air)
            return REFRACTION_INDEX_AIR;
        else
            return -1f;
    }

}
