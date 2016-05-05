using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TUTChangeButtonValue : MonoBehaviour {
    TUT_Manager tutRoot;
    private int klicked = 1;

	// Use this for initialization
	void Start () {
        // button.GetComponent<Button>();
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { method(klicked); });
        tutRoot = gameObject.GetComponentInParent<TUT_Manager>();

	}

    void Update()
    {

    }

    private void method(int v)
    {
         tutRoot.TUT_0(v);
         klicked++;
    }
}
