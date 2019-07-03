using UnityEngine;

public class SwitchBackgroundsScript : MonoBehaviour {

    public Texture[] textures;
    public GameObject Background;

    private Renderer Rend;
    private int SelectedTexture;
    //private static bool created = false;
    private static bool[] Bgs = { false, false, false };
    private static SwitchBackgroundsScript instace;

    // script References

    private FlipNormals GetBackground;
    private SwitchBackgroundsScript backgroundsScript;

    //Functions

     void Awake(){
        
        if(instace!= null){

            Destroy(gameObject);

        }
        else{

            instace = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start () {

        if (Background != null){

            Rend = Background.GetComponent<Renderer>();
            ChangeBackgrounds();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(Background == null && FindObjectOfType<FindBackground>() != null){

            Background = FindObjectOfType<FlipNormals>().gameObject;
            Rend = Background.GetComponent<Renderer>();
            ChangeBackgrounds();
        }
		
	}

    void ChangeBackgrounds(){

        SelectRandomTexture();

        if(Bgs[SelectedTexture] == false){

            Bgs[SelectedTexture] = true;

            Rend.material.mainTexture = textures[SelectedTexture];

            if(Bgs[0] == true && Bgs[1] == true && Bgs[2] == true){

                Bgs[0] = false; 
                Bgs[1] = false; 
                Bgs[2] = false;
            }
        }

        else{

            ChangeBackgrounds();

        }
    }

    void SelectRandomTexture(){

        SelectedTexture = Random.Range(0, textures.Length);

    }
}