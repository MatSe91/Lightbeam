using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadPlayerForGame : MonoBehaviour {

    private List<GameObject> childs;

    void Start()
    {
        childs = new List<GameObject>();
        getChilds();
        manageParticle();
    }

    private void manageParticle(bool bo = false)
    {
        foreach (var item in childs)
        {
            var sys = item.GetComponent<ParticleSystem>();
            if (sys != null)
            {
                if (sys.isPlaying) sys.Stop();
                else if (!sys.isPlaying && bo) sys.Play();
            }
        }
    }

    private void getChilds()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childs.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void ManageStart()
    {
        manageParticle(true);
        StartCoroutine(WaitAndActivate((int)getObjectWithParticle().GetComponent<ParticleSystem>().duration));
    }


    #region muss refactored werden!
    private GameObject getObjectWithParticle()
    {
        foreach (var item in childs)
        {
            if (item.GetComponent<ParticleSystem>() != null)
            {
                return item;
            }
        }
        throw new MissingParticleException("Particle System (ActivatePlayerParticle) is missing on Player!");
    }

    private void activateBeam()
    {
        foreach (var item in childs)
        {
            if (item.GetComponent<PlayerRotator>() != null)
            {
                item.GetComponent<PlayerRotator>().enabled = true;
            }
            if (item.GetComponent<SpriteRenderer>() != null)
            {
                item.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        LevelManager.GameStarted = true;
        InputManager.touchInput = true;
    }

    #endregion
    private IEnumerator WaitAndActivate(int sec)
    {
        yield return new WaitForSeconds(sec);
        activateBeam();
    }
}
