using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MadLevelManager;
using System;
using SmartLocalization;
using UnityEngine.Audio;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject MainMenu;
    private GameObject activeMenu;
    public Button MusicButton;
    public Button SfxButton;
    public AudioMixerGroup sounds;
    

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
                SetMusicOn();
            }
            else 
            {
                SetMusicOff();
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
                SetSfxOn();
            }
            else { SetSfxOff(); }
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
            SetMusicOn();
        }
        else
        {
            SetMusicOff();
        }

        PlayerPrefs.Save();
    }

    private void SetMusicOff()
    {
        
        Settings.MusicVolume = false;
        sounds.audioMixer.SetFloat("MusicVolume", -80f);
        MusicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music_off");
        PlayerPrefs.SetInt("music", 0);
    }

    private void SetMusicOn()
    {
       
        Settings.MusicVolume = true;
        sounds.audioMixer.SetFloat("MusicVolume", 0f);
        MusicButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/music");
        PlayerPrefs.SetInt("music", 1);
        
    }

    public void ClickSfxButtton()
    {
        if(Settings.SfxVolume)
        {
            SetSfxOff();
        }

        else
        {
            SetSfxOn();
        }

        PlayerPrefs.Save();
    }

    private void SetSfxOn()
    {
        Settings.SfxVolume = true;
        sounds.audioMixer.SetFloat("SfxVolume", -0f);
        SfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx");
        PlayerPrefs.SetInt("sfx", 1);
    }

    private void SetSfxOff()
    {
        Settings.SfxVolume = false;
        sounds.audioMixer.SetFloat("SfxVolume", -80f);
        SfxButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Icons/sfx_off");
        PlayerPrefs.SetInt("sfx", 0);
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

    public void ClickLeave()
    {
        Application.Quit();
    }

    public void ClickMainMenu(GameObject active)
    {
        active.SetActive(false);
        activeMenu = active;
        MainMenu.SetActive(true);
    }

}
