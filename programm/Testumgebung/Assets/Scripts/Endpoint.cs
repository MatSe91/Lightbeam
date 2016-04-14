using UnityEngine;
using System.Collections;
using MadLevelManager;
using DigitalRuby.FastLineRenderer;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Endpoint : MonoBehaviour {

    private bool isBeamconntected;
    public GameObject EndLevelMenu;
    public LevelManager manager;
    
   

    public void setBeamConnectivity(bool bol)
    {
        isBeamconntected = bol;
    }

    public void setActiveGameObject(bool value)
    {
        if (isBeamconntected && value)
        {
            LevelCompleted();
            SetCollectedUI();

        }
    }

    private void SetCollectedUI()
    {
       Image[] collectibles = EndLevelMenu.GetComponentsInChildren<Image>();

        foreach (var collectUi in collectibles)
        {
            foreach (var collect in CollectibleManager.Collected)
            {
                if (collectUi.name == collect + "_image")
                {

                    collectUi.color = new Color(255f, 255f, 255f, 255f);
                }
            }
        }   
        
    }

    private void LevelCompleted()
    {
        manager.Pause(EndLevelMenu);
        manager.SetCollectedItems();
    }


}
