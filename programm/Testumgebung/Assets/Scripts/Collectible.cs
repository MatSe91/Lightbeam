using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool collected = false;
    private bool connected;
    private Collider other;

    private Animator animator;
    private ParticleSystem sys;
    private AudioSource sfx;



    void Start()
    {
       animator =  gameObject.GetComponent<Animator>();
       sfx = gameObject.GetComponent<AudioSource>();
       sys = gameObject.GetComponent<ParticleSystem>();        
       sys.Stop();
    }

    public bool Collected
    {
        get
        {
            return collected;
        }

        set
        {
            collected = value;
        }
    }

    /// <summary>
    /// Collect the collectible  on with trigger
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    { 
        other = col;
       
    }

    void OnTriggerStay(Collider col)
    {
        connected = true;
      

    }
    /// <summary>
    /// Uncollect the collectible if the collider is not the collictble object
    /// </summary>
    void Update()
    {

        // Das hier ist der größte Schrott, aber anders funktioniert es einfach nicht bei mir
        // vorallem 2. und 3. If, fehle eine davon spackt es wieder abartig rum

        if (connected && !collected)
        {
            collected = true;
            if (sfx != null)
                sfx.Play();
            else Debug.Log("No SFX File on WaterDrop!");
        }

        if (other == null && !connected)
        {           
            collected = false;
            connected = false;
        }

        if (other == null && connected)
        {
            connected = false;
        }
        animateWater();
    }

    private void animateWater()
    {
        if (collected)
        {
            animator.SetBool("play", true);
            if (!sys.isPlaying) sys.Play();

            if (LevelManager.GameFinished)
            {
                animator.enabled = false;
            }
        }
        else
        {
            if (sys.isPlaying) sys.Stop();
            animator.SetBool("play", false);
        }        
    }
}
