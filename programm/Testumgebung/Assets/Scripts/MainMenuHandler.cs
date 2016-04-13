using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MadLevelManager;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject MainMenu;
    private GameObject activeMenu;

    void Start()
    {
        activeMenu = MainMenu;
        MadLevelProfile.profile = "default";
        Debug.Log("Current profile is " + MadLevelProfile.profile);      
    }

    public void ClickMusicButtton(Button musicButton)
    {
        if (!Settings.MusicVolume)
        {
            AudioListener.volume = 1f;
            Settings.MusicVolume = true;
            musicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Images/music");
        }
        else
        {
            AudioListener.volume = 0f;
            Settings.MusicVolume = false;
            musicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Images/music_off");
        }
    }

    public void ClickSfxButtton(Button sfxButton)
    {
        if(Settings.SfxVolume)
        { 
            Settings.SfxVolume = false;
            sfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Images/sfx_off");
        }
 
        else
        {
            Settings.SfxVolume = true;
            sfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Images/sfx");
        }
    }

    public void ClickNextMenu(GameObject nextMenu)
    {
        activeMenu.SetActive(false);
        nextMenu.SetActive(true);
        activeMenu = nextMenu;
    }

    public void ClickPlay()
    {
        MadLevel.LoadLevelByName("Select Level");
    }

    public void ClickReset()
    {
        MadLevelProfile.Reset();
        

    }

}
