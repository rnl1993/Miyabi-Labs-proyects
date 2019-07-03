using UnityEngine;

public class DeleteAll : MonoBehaviour {

	// Use this for initialization
	void Start () {

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        	
	}
	
	
}
