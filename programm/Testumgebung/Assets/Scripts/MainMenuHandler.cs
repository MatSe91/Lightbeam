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
    public Button MusicButton;
    public Button SfxButton;

    void Start()
    {
        loadSettings();
        activeMenu = MainMenu;          
    }

    private void loadSettings()
    {
        // load Profile 
        MadLevelProfile.profile = "_default";

        LanguageManager lm = LanguageManager.Instance;
        // load language
        if (PlayerPrefs.HasKey("language"))
        {
            Settings.Language = PlayerPrefs.GetString("language");
         
            lm.ChangeLanguage(MadLevelProfile.GetProfileString("Language", Settings.Language));
        }
        else
        {
            PlayerPrefs.SetString("language", "de");
            lm.ChangeLanguage(MadLevelProfile.GetProfileString("Language", Settings.Language));
        }
        


        // loadMusicVolume
        if (PlayerPrefs.HasKey("music"))
        {
            Settings.MusicVolume = Convert.ToBoolean(PlayerPrefs.GetInt("music"));
            if(Settings.MusicVolume)
            {
                MusicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music");
            }
            else 
            {
                MusicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music_off");
            }
        }
        else
        {
            Settings.MusicVolume = true;
            PlayerPrefs.SetInt("music", 1);
        }

        // loadSfxVolume

        if (PlayerPrefs.HasKey("sfx"))
        {
            Settings.SfxVolume = Convert.ToBoolean(PlayerPrefs.GetInt("sfx"));
            if (Settings.SfxVolume)
            {
                SfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx");
            }
            else { SfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx_off"); }
        }
        else
        {
            Settings.SfxVolume = true;
            PlayerPrefs.SetInt("sfx", 1);
        }
    }

    public void ClickMusicButtton()
    {
        if (!Settings.MusicVolume)
        {
            AudioListener.volume = 1f;
            Settings.MusicVolume = true;
            MusicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music");
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            AudioListener.volume = 0f;
            Settings.MusicVolume = false;
            MusicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music_off");
            PlayerPrefs.SetInt("music", 0);
        }

        PlayerPrefs.Save();
    }

    public void ClickSfxButtton()
    {
        if(Settings.SfxVolume)
        { 
            Settings.SfxVolume = false;
            SfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx_off");
            PlayerPrefs.SetInt("sfx", 0);
        }
 
        else
        {
            Settings.SfxVolume = true;
            SfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx");
            PlayerPrefs.SetInt("sfx", 1);
        }

        PlayerPrefs.Save();
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
        PlayerPrefs.SetString("language", lang);
        PlayerPrefs.Save();

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
