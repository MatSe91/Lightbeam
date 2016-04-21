using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

    private static Music instance = null;

    public static Music Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            if (instance.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
            {
                instance.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
                instance.GetComponent<AudioSource>().volume = GetComponent<AudioSource>().volume;
                instance.GetComponent<AudioSource>().Play();
            }

            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this.gameObject);
    }
}
