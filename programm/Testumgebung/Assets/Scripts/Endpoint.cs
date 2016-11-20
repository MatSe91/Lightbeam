using UnityEngine;
using UnityEngine.UI;


public class Endpoint : MonoBehaviour {

    private bool isBeamconntected;
    public GameObject EndLevelMenu;
    public GameObject CollectedUI;
    public LevelManager manager;
    
    public bool getBeamConnectivity()
    {
        return isBeamconntected;
    }
   

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
       Image[] collectibles = CollectedUI.GetComponentsInChildren<Image>();

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
        manager.SetCollectedItems();
        EndLevelMenu.transform.parent.localPosition = new Vector3(GameObject.Find("Main Camera").transform.position.x, 0, 0);
        manager.Pause(EndLevelMenu);
        LevelManager.GameFinished = true;
    }

    void Update()
    {
        if (!isBeamconntected && LevelManager.GameStarted)
        {
            if (gameObject.GetComponent<AudioSource>() != null)
                this.gameObject.GetComponent<AudioSource>().Play();
            else
                Debug.Log("Missing Audiosource on EndPoint!");
        }
       
    }
}
