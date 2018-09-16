using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour {

    HexGridInfoPlane hexGridInfoPlane = null;
    Control control = null;

    bool debug = true;

    public bool Debug {
        get { return debug; }

        set {
            debug = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        control = GetComponentInParent<Control>();
        hexGridInfoPlane = GetComponentInChildren<HexGridInfoPlane>();
    }
	
	// Update is called once per frame
	//void Update () { }

    public void UpdateUI()
    {
        if (control.CurrentHex != null) {

            hexGridInfoPlane.text = control.CurrentHex.name + "\n" + control.CurrentHexAsHexCell.cellInfo.perlinValue + "\n" + control.CurrentHexAsHexCell.cellInfo.Type + "\n";
            hexGridInfoPlane.isShowing = true;
        }
        else {
            //hexGridInfoPlane.text = "";
            hexGridInfoPlane.isShowing = false;
        }
    }
}
