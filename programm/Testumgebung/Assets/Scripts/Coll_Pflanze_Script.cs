using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Coll_Pflanze_Script : MonoBehaviour {

    public GameObject layer;
    private List<GameObject> gameElements = new List<GameObject>();
 
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Collectible"))
        {
            print("Do Something awesome like an animation!");
            Destroy(col.gameObject);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

            
            for (int i = 0; i < layer.transform.childCount; i++)
            {
                gameElements.Add(layer.transform.GetChild(i).gameObject);
            }



            foreach (var element in gameElements)
            {
               
                if (element.GetComponent<SpriteRenderer>() != null)
                {
                    element.GetComponent<SpriteRenderer>().color = Color.white;
                }

                else
                {
                    Renderer rend = element.GetComponent<Renderer>();

                    rend.material.shader = Shader.Find("Standard");
                    rend.material.SetColor("_Color", Color.grey);
                }
              
            }
           
          
            print(col.transform.gameObject);
        }
    }
}
