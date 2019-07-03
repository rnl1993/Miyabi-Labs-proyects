using UnityEngine;

public class PasswordScript : MonoBehaviour {

    public bool click;

    private AudioSource aSource;

    private void Start()
    {
        
        click = false;
        aSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if(click == true){

            aSource.Play();

            click = false;

        }

    }

 
}
