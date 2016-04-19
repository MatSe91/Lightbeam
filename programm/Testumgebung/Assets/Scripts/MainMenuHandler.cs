using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MadLevelManager;
using System;
using SmartLocalization;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject MainMenu;
    private GameObject activeMenu;

    void Start()
    {
        loadSettings();
        activeMenu = MainMenu;          
    }

    private void loadSettings()
    {
        // load Profile 
        MadLevelProfile.profile = "default";

        // load language
        LanguageManager lm = LanguageManager.Instance;
        lm.ChangeLanguage(MadLevelProfile.GetProfileString("Language", "de"));


        // loadMusicVolume
        

        // loadSfxVolume
    }

    public void ClickMusicButtton(Button musicButton)
    {
        if (!Settings.MusicVolume)
        {
            AudioListener.volume = 1f;
            Settings.MusicVolume = true;
            musicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music");
        }
        else
        {
            AudioListener.volume = 0f;
            Settings.MusicVolume = false;
            musicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music_off");
        }
    }

    public void ClickSfxButtton(Button sfxButton)
    {
        if(Settings.SfxVolume)
        { 
            Settings.SfxVolume = false;
            sfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx_off");
        }
 
        else
        {
            Settings.SfxVolume = true;
            sfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx");
        }
    }


    public void ClickNextMenu(GameObject nextMenu)
    {
        activeMenu.SetActive(false);
        nextMenu.SetActive(true);
        activeMenu = nextMenu;
    }

    public void ChangeLanguage(String lang)
    {
        LanguageManager.Instance.ChangeLanguage(lang);
        MadLevelProfile.SetProfileString("Language", lang);
    }


    public void ClickPreviousMenu(GameObject previousMenu)
    {
        activeMenu.SetActive(false);
        previousMenu.SetActive(true);
        activeMenu = previousMenu;
    }

    public void ClickPlay()
    {
        MadLevel.LoadLevelByName("Select Level");
    }

    public void ClickReset(GameObject previousMenu)
    {
        activeMenu.SetActive(false);
        previousMenu.SetActive(true);
        activeMenu = previousMenu;
        MadLevelProfile.Reset();        
    }

}
