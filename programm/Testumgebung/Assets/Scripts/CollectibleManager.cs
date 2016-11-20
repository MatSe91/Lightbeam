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
            if(collectible.GetComponent<Collectible>().Collected)
            {
                Collected.Add(collectible.name);
                collectible.GetComponent<Rigidbody>().useGravity = true;
                collectible.GetComponent<Rigidbody>().isKinematic = false;
                collectible.GetComponent<Animator>().enabled = false;
            }
        }
    }

    public static void SetCollectiblesToLevel()
    {
        foreach (var item in Collected)
        {
            MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, item, true);

            IsNextLevelAvailable();

        }


    }

    public static bool IsNextLevelAvailable()
    {
        int waterdropGained = new MadLevelQuery()
                    .ForGroup(MadLevel.currentGroupName)
                    .OfLevelType(MadLevel.Type.Level)
                    .SelectProperty("Waterdrop_1", "Waterdrop_2", "Waterdrop_3", "Waterdrop_4", "Waterdrop_5", "Waterdrop_6")
                    .CountEnabled();
        //Debug.Log(waterdropGained);

        int value = System.Convert.ToInt32(MadLevel.arguments);
        if (waterdropGained >= value)
        {
            MadLevelProfile.SetCompleted(MadLevel.currentLevelName, true);
            return true;
        }
        else
        {
            return false;
        }

    }





}
