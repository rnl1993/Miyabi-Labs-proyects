using UnityEngine;
using UnityEngine.SceneManagement;

public class Returnkeyboard : MonoBehaviour {

    [HideInInspector] public Renderer rend;

    // Use this for initialization
    void Start(){

        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update(){
        
        ChangeColor();
    }

     void ChangeColor(){

        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", Color.white);
    }

    public void ChangeScene(){

        SceneManager.LoadScene("LoginScene");
    }
}

