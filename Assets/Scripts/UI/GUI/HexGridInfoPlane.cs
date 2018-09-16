using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGridInfoPlane : MonoBehaviour {

    Text textLabel = null;
    Animator animator = null;

    public bool isShowing {
        get {
            return animator.GetBool("Showing");
        }

        set {
            animator.SetBool("Showing", value);
        }
    }

    public string text {
        get {
            return textLabel.text;
        }

        set {
            textLabel.text = value;
        }    
    }


	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        textLabel = GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	// void Update () { }
}
