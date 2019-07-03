using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_B : MonoBehaviour {

    // public variables

    public float speed;
    public bool touching;
    public RaycastHit hit;
    public Color HoverColor1;
    public Color HoverColor2;
    public Color HoverColor3;
    public Color HoverColor4;
    public LayerMask layerMask;
    public GameObject GrabbedObject;
    public GameObject GrabberPosition;

    // private variables

    private float maxDistance = 500;

    // Functions

    void Start()
    {
        touching = false;
    }

    void Update()
    {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
        {

            if (hit.collider.GetComponent<Cube1BScript>() != null || hit.collider.GetComponent<Cube2BScript>() != null || hit.collider.GetComponent<Cube3BScript>() != null || hit.collider.GetComponent<Cube4BScript>() != null)
            {

                if (hit.collider.GetComponent<Cube1BScript>() != null)
                {

                    var cube = hit.collider.gameObject.GetComponent<Renderer>();
                    touching = true;
                    cube.material.GetColor("_Color");
                    cube.material.SetColor("_Color", HoverColor1);

                }

                if (hit.collider.GetComponent<Cube2BScript>() != null)
                {

                    var cube = hit.collider.gameObject.GetComponent<Renderer>();
                    touching = true;
                    cube.material.GetColor("_Color");
                    cube.material.SetColor("_Color", HoverColor2);

                }

                if (hit.collider.GetComponent<Cube3BScript>() != null)
                {

                    var cube = hit.collider.gameObject.GetComponent<Renderer>();
                    touching = true;
                    cube.material.GetColor("_Color");
                    cube.material.SetColor("_Color", HoverColor3);

                }

                if (hit.collider.GetComponent<Cube4BScript>() != null)
                {

                    var cube = hit.collider.gameObject.GetComponent<Renderer>();
                    touching = true;
                    cube.material.GetColor("_Color");
                    cube.material.SetColor("_Color", HoverColor4);
                }

                GrabbedObject = hit.collider.gameObject;

                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {

                    float step = Time.deltaTime * speed;

                    GrabbedObject.transform.position = Vector3.Lerp(GrabbedObject.transform.position, GrabberPosition.transform.position, step);
                }

            }

        }

        else
        {

            touching = false;
        }
    }
}
