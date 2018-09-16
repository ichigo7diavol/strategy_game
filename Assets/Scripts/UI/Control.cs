using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Rigidbody))]
public class Control : MonoBehaviour {

    public float speed = 1;
    public GameObject selectorPrefab = null;

    [SerializeField]
    bool isDebug = false;

    Rigidbody rigidBody = null;
    Camera viewCamera = null;
    GameObject currentHex = null;
    MainCanvas mainUI = null;
    GameObject selector = null;

    Vector3 velocity;
    Ray ray;
    RaycastHit hit;

    public bool IsDebug { get { return isDebug; } set { isDebug = value; } }
    public HexCell CurrentHexAsHexCellRaw { get { return currentHex.GetComponent<HexCell>(); } }
    public HexCell CurrentHexAsHexCell { get { if (currentHex) return currentHex.GetComponent<HexCell>(); else return null; } }

    public GameObject CurrentHex { get { return currentHex; } }
    public GameObject Selector { get { return selector; } }
    
    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
        selector = Instantiate(selectorPrefab);

        mainUI = GetComponentInChildren<MainCanvas>();
    }

    // Before physics
    void FixedUpdate()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        rigidBody.MovePosition(rigidBody.position + velocity * Time.deltaTime);
    }

    void ProcessRaycast()
    {
        // TODO: remake this thing to something more logic
        ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 600.0f))
        {
            currentHex = hit.transform.gameObject;
            selector.SetActive(true);
            selector.transform.position = currentHex.transform.position;
        }
        else
        {
            currentHex = null;
            selector.SetActive(false);
        }
        DebugRoutine();
    }

    void DebugRoutine()
    {
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    }

    void Update()
    {
        // TODO: understand that why this thing are here; 
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Mouse ScrollWheel"), Input.GetAxisRaw("Vertical")).normalized * speed;

        if (Input.GetButton("Fire1"))
            ProcessRaycast();

        mainUI.UpdateUI();

        if (isDebug)
            DebugRoutine();
    }
}
