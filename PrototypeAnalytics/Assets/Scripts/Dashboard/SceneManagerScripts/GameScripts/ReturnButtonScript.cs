using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonScript : MonoBehaviour {
    
    public Renderer rend;

	// Use this for initialization
	void Start () {

        rend = GetComponent<Renderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeColor()
    {

        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", Color.white);

    }

    public void ReturnFuntion(){

        SceneManager.LoadScene("DashBoard");

    }
}
