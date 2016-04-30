using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MadLevelManager;

public static  class CollectibleManager
{
    private static List<string> collected = new List<string>();
    private static string collectibleTag = "Collectible";

    public static List<string> Collected
    {
        get
        {
            return collected;
        }

     
    }

    public static void AddCollectedItems()
    {
        foreach(GameObject collectible in GameObject.FindGameObjectsWithTag(collectibleTag))
        {
            if(collectible.GetComponent<Collectible>().collected)
            {
                Collected.Add(collectible.name);
                collectible.GetComponent<Rigidbody>().useGravity = true;
                collectible.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public static void SetCollectiblesToLevel()
    {
        foreach (var item in Collected)
        {
            MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, item, true);
        }
       
       
    }

   




}
