using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Coll_Pflanze_Script : MonoBehaviour {

    public List<GameObject> layers;

    private List<GameObject> gameElements = new List<GameObject>();
   

    void Start()
    {
        foreach (var layer in layers)
        {
            for (int i = 0; i < layer.transform.childCount; i++)
            {
                gameElements.Add(layer.transform.GetChild(i).gameObject);
            }
        }

        gameElements.Add(this.gameObject);
        SetGameElementsColor(false);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Collectible"))
        {
           // print("Do Something awesome like an animation!");
            ParticleSystem part = this.gameObject.GetComponent<ParticleSystem>();
            if (part != null && !part.isPlaying) part.Play();
            Destroy(col.gameObject);

            if (this.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                SetColorBySprite(true, this.gameObject);
            }
            else
            {
                SetColorBy3DObject(true, this.gameObject);
            }

           

            SetGameElementsColor(true);
           // print(col.transform.gameObject);

        }
    }

    private void SetGameElementsColor(bool bright)
    {
            foreach (var element in gameElements)
            {
                if (element.GetComponent<SpriteRenderer>() != null)
            {
                SetColorBySprite(bright, element);
            }
            else
            {
                SetColorBy3DObject(bright, element);
            }

        }

    }

    private static void SetColorBy3DObject(bool bright, GameObject element)
    {
    
        Renderer rend = element.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Standard");
        rend.material.SetColor("_Color", GetColorByObject(bright, element));
    }

    private static void SetColorBySprite(bool bright, GameObject element)
    {
        element.GetComponent<SpriteRenderer>().color = GetColorByObject(bright, element);
    }

    private static Color GetColorByObject(bool bright, GameObject element)
    {
       if(element.GetComponent<ColorProperty>() != null)
        {
            if (bright)
            {
               return element.GetComponent<ColorProperty>().Bright;
            }
            else
            {
                return element.GetComponent<ColorProperty>().Dark;
            }
        }
        throw new NullReferenceException("There is no ColorProperty on Object.");
    }
}
 
