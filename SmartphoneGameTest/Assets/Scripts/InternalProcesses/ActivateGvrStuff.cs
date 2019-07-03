using UnityEngine;

public class ActivateGvrStuff : MonoBehaviour {

    private void Awake()
    {
        this.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        this.gameObject.SetActive(true);
    }
}
