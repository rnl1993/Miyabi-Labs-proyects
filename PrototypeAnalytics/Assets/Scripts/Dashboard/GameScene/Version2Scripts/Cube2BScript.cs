using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2BScript : MonoBehaviour {

    //public variables

    public Renderer rend;
    public AudioClip[] AudioClips;
    public Color color;

    // script References

    private AudioSource Audio;
    private Raycast_B raycastGameScene;
    private ScoreManagerScript scoreManagerScriptReference;

    // Functions

    void Start()
    {

        rend = GetComponent<Renderer>();
        Audio = GetComponent<AudioSource>();
        raycastGameScene = FindObjectOfType<Raycast_B>();
        scoreManagerScriptReference = FindObjectOfType<ScoreManagerScript>();

        Audio.clip = AudioClips[0];
        Audio.Play();
    }

    void Update()
    {

        if (raycastGameScene.touching == false)
        {

            changeColor();
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GoalPoint_2B_Script>() != null)
        {

            scoreManagerScriptReference.ScoreFuntion();
            Audio.clip = AudioClips[1];
            Audio.Play();

            yield return new WaitForSeconds(0.15f);

            Destroy(this.gameObject);
        }

        if (other.gameObject.GetComponent<GoalPoint_1B_Script>() != null || other.gameObject.GetComponent<GoalPoint_3B_Script>() != null || other.gameObject.GetComponent<GoalPoint_4B_Script>() != null)
        {

            scoreManagerScriptReference.ScoreFuntion2();
            Audio.clip = AudioClips[2];
            Audio.Play();

            yield return new WaitForSeconds(0.15f);

            Destroy(this.gameObject);

        }

    }

    public void changeColor()
    {

        rend.sharedMaterial.GetColor("_Color");
        rend.sharedMaterial.SetColor("_Color", color);

    }

}

