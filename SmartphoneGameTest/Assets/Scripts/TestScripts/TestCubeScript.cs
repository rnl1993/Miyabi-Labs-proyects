using UnityEngine;

public class TestCubeScript : MonoBehaviour {

    [HideInInspector] public Renderer rend;

	// Use this for initialization
	void Start () {

        rend = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
