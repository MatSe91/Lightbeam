using UnityEngine;
using System.Collections.Generic;

public class TUTLevel1 : MonoBehaviour {

    public GameObject collectables;
    public GameObject background;
    private GameObject endpoint;
    public new Light light;

    void Start()
    {
        endpoint = GameObject.Find("Endpoint");
    }

    private List<SpriteRenderer> getCompleteList()
    {
        try
        {
            var list = new List<SpriteRenderer>();

            foreach (var item in collectables.GetComponentsInChildren<SpriteRenderer>())
            {
                list.Add(item);
            }

            foreach (var item in background.GetComponentsInChildren<SpriteRenderer>())
            {
                list.Add(item);
            }
            return list;

        }
        catch (System.NullReferenceException e)
        {
            throw e;
        }
    }

    public void ChangeLight(string onoff)
    {
        if (onoff == "off")
        {
            light.intensity = 0;
            endpoint.layer = LayerMask.NameToLayer("Wall");

            foreach (var item in getCompleteList())
            {
                item.color = new Color(0.392f, 0.392f, 0.392f, 1);
            }

        }
       
        if (onoff == "on")
        {
            light.intensity = 1;
            endpoint.layer = LayerMask.NameToLayer("Touch");
            foreach (var item in getCompleteList())
            {
                item.color = new Color(1, 1, 1, 1);
            }
        }
    }  
}
