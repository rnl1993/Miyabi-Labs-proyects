using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaEncojerObjecto : MonoBehaviour {
 
	private float timeToDestroy;


    // Use this for initialization
    void Start()
    {

        timeToDestroy = 8;
        Destroy(this.gameObject, timeToDestroy);
       

    }

    void Update()
    {

        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 2.0f)
        {

			this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - (0.25f * Time.deltaTime), gameObject.transform.localScale.y - (0.25f * Time.deltaTime), gameObject.transform.localScale.z - (0.25f * Time.deltaTime));
            


        }

    }

}
