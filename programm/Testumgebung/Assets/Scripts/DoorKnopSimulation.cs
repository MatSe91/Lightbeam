using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorKnopSimulation : MonoBehaviour {

    public List<GameObject> particles;
    private List<ParticleSystem> systems;
    private List<Animator> animators;

	// Use this for initialization
	void Start () {
        systems = new List<ParticleSystem>();
        animators = new List<Animator>();
        foreach (var item in particles)
        {
            systems.Add(item.GetComponent<ParticleSystem>());
            animators.Add(item.GetComponent<Animator>());
        }
	
	}
	
    public void activate()
    {
        foreach (var anim in animators)
        {
            anim.SetBool("isActivated", true);
        }

        foreach (var sys in systems)
        {
            if (!sys.isPlaying) sys.Play();
        }
    }

    public void deactivate()
    {
        foreach (var anim in animators)
        {
            anim.SetBool("isActivated", false);
        }

        foreach (var sys in systems)
        {
            if (sys.isPlaying) sys.Stop();
        }
    }
}
