using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


/// <summary>
/// Klasse, die für die Lichtbrechung im Wasser zuständig ist.<para/>
/// Methode: getDir(): Sie enthält den neuen Richtungsvektor (gebrochen und Totalreflexion)
/// Methode: getLineInWater(): enthält true oder false, jenachdem ob Lichtstrahl im Wasser ist
/// </summary>
public class BeamRefraction
{

    private Vector3 dir;
    private bool inWater;
    Refraction_Medium medium = new Refraction_Medium();

    /// <summary>
    /// gibt den Richtungsvektor zurück
    /// </summary>
    /// <returns></returns>
    public Vector3 getDir()
    {
        return this.dir;
    }

    /// <summary>
    /// true, wenn sich die Linie unter Wasser befindet, sonst false
    /// </summary>
    /// <returns></returns>
    public bool getLineInWater()
    {
        return this.inWater;
    }

    /// <summary>
    /// Konstruktor ist die eigentliche working Klasse <para/>
    /// direction: Eintreffender Richtungsvektor<para/>
    /// hit: Raycasthit des Auftrittpunktes<para/>
    /// rm1: Das aktuelle Refractionmedium z.B. air, wenn der Lichtstrahl in der Luft ist<para/>
    /// rm2: Das neue Refractionmedium, in dem der Lichtstrahl eindringt
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="hit"></param>
    /// <param name="rm1"></param>
    /// <param name="rm2"></param>
    public BeamRefraction(Vector3 direction, RaycastHit hit, Refraction_Medium.Refraction_Med rm1, Refraction_Medium.Refraction_Med rm2)
    {

        Vector3 dir = new Vector3(0, 0, 0);
        float alpha = Vector3.Angle(-direction, hit.normal);
        Vector3 normale = -hit.normal;
        float n1 = medium.refraction_Index(rm1);    // ist das gut? evtl. Refactoring
        float n2 = medium.refraction_Index(rm2);    // ist das gut? evtl. Refactoring
        float beta = 0;

        //  n1 = dünner Stoff, n2 = dichter Stoff
        // wenn Strahl dünnen Stoff (z.B. Luft) in dichten Stoff eindringt (z.B. Wasser)
        if (n1 < n2)
        {
            beta = Mathf.Rad2Deg * (Mathf.Asin(((Mathf.Sin(alpha * Mathf.Deg2Rad)) * n1) / n2));

            // Wenn Richtungsvektor nach rechts zeigt (positiver x Wert) breche nach rechts weg
            if (direction.x > 0)
            {
                this.dir = Quaternion.Euler(0, 0, beta) * normale;
            }
            // Wenn Richtungsvektor nach links zeigt (negativer x Wert) breche nach links weg
            else
            {
                this.dir = Quaternion.Euler(0, 0, -beta) * normale;
            }
            this.inWater = true;        
        }
        else //  n1 = dichter Stoff, n2 = dünner Stoff
        {
            beta = Mathf.Rad2Deg * (Mathf.Asin(((Mathf.Sin(alpha * Mathf.Deg2Rad)) * n1) / n2));

            // Wenn Richtungsvektor nach rechts zeigt (positiver x Wert) breche nach rechts weg
            if (direction.x > 0)
            {
                // wenn beta > 0  --> Brechung, sonst Totalreflexion
                if (beta > 0) 
                {
                    this.dir = Quaternion.Euler(0, 0, -beta) * normale;
                    this.inWater = false;
                }
                else
                {
                    this.dir = Vector3.Reflect(direction, -normale);
                    this.inWater = true;
                }
            }
            // Wenn Richtungsvektor nach links zeigt(negativer x Wert) breche nach links weg
            else
            {
                // wenn beta > 0  --> Brechung, sonst Totalreflexion
                if (beta > 0) 
                {
                    this.dir = Quaternion.Euler(0, 0, beta) * normale;
                    this.inWater = false;
                }
                else
                {
                    this.dir = Vector3.Reflect(direction, -normale);
                    this.inWater = true;
                }
            }
        }
    }
}