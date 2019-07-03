using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VR_DisablerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
        StartCoroutine(DeActivatorVr("none"));

	}

    IEnumerator DeActivatorVr(string v)
    {
        XRSettings.LoadDeviceByName(v);

        yield return null;

        XRSettings.enabled = false;
    }

}
