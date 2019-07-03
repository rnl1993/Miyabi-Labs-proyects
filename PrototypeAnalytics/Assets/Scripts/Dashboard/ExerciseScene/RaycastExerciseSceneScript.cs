using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaycastExerciseSceneScript : MonoBehaviour
{
    public RaycastHit hit;
    public float maxDistance = 500;
    public LayerMask layerMask;

    private ReturnButtonScript returnButtonScriptReference;

    // Use this for initialization
    void Start()
    {

        returnButtonScriptReference = GameObject.FindGameObjectWithTag("Boton").GetComponent<ReturnButtonScript>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

		if (Physics.Raycast (transform.position, direction, out hit, maxDistance, layerMask)) {

			if (hit.collider.GetComponent<ReturnButtonScript> () != null) {

				var button = hit.collider.gameObject.GetComponent<Renderer> ();

				button.material.GetColor ("_Color");
				button.material.SetColor ("_Color", Color.gray);

				if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger)) {

					returnButtonScriptReference.ReturnFuntion ();

				}

			}

		} else {

			returnButtonScriptReference.ChangeColor ();

		}
    }
}