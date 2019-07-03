using UnityEngine;

public class DeteleAllPlayerPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {

        PlayerPrefs.DeleteAll();
        print("Acabo de borrar todos los player pref");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
