using System.Collections;
using UnityEngine.XR;
using UnityEngine;

public class VR_EnablerScript : MonoBehaviour {
    
	// Functions
    void Start () {

        StartCoroutine(ActivatorVr(PlayerPrefs.GetString(VRdevicesManagerScript.VR)));
	}

    public IEnumerator ActivatorVr(string v)
    {
        XRSettings.LoadDeviceByName(v);

        yield return null;

        XRSettings.enabled = true;
    }
}
