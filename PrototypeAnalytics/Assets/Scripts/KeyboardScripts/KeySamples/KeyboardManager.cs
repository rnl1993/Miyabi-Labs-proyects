using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {
    
    public GameObject[] SpecialKeysTransform;
    public int positions;

    public SpecialKeysSample[] SKR;

    private int i;

    // Use this for initialization
    public void Start () {

        positions = i;

        for ( i = 0; i < SpecialKeysTransform.Length; i++){

            SKR[i] = SpecialKeysTransform[i].GetComponent<SpecialKeysSample>();

        }
        	
	}
	
}
