using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class V_EnablerIOS : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {

        StartCoroutine(ActivatorVr("Cardboard"));

    }

    public IEnumerator ActivatorVr(string v)
    {
        XRSettings.LoadDeviceByName(v);

        yield return new WaitForSeconds(0.1f);

        XRSettings.enabled = true;
    }
}