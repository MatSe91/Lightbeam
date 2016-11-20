using UnityEngine;
using System.Collections;

public static class Settings {

    private static bool sfxVolume = true;
    private static bool musicVolume = true;
    private static string language = "de";

    public static bool MusicVolume
    {
        get
        {
            return musicVolume;
        }

        set
        {
            musicVolume = value;
        }
    }

    public static bool SfxVolume
    {
        get
        {
            return sfxVolume;
        }

        set
        {
            sfxVolume = value;
        }
    }

    public static string Language
    {
        get
        {
            return language;
        }

        set
        {
            language = value;
        }
    }
}
