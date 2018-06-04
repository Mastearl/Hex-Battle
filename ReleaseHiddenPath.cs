using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseHiddenPath : MonoBehaviour {

    public Transform Hidden01;
    public Transform Hidden02;

    public bool Reveal;
    
    // Use this for initialization
	void Start ()
    {
        Reveal = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Reveal)
        {
            Hidden01.gameObject.SetActive(true);
            Hidden02.gameObject.SetActive(true);
        }
        else
        {
            Hidden01.gameObject.SetActive(false);
            Hidden02.gameObject.SetActive(false);
        }
	}
}
