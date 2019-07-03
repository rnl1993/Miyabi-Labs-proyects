using System.Collections;
using UnityEngine;

public class Cube2Script : MonoBehaviour {
 
    // public variables

    public AudioClip [] AudioClips;

    // scripts References

    private AudioSource Audio;
    private ScoreManagerScript scoreManagerScriptReference;
   
	// Functions

	void Start () {

        Audio = GetComponent<AudioSource>();
        scoreManagerScriptReference = FindObjectOfType<ScoreManagerScript>();

        Audio.clip = AudioClips[0];
        Audio.Play();

    }
	

    private IEnumerator OnTriggerEnter(Collider other)
    {

        if(other.gameObject.GetComponent<GoalPoint_2_Script>() != null)
        {

            scoreManagerScriptReference.ScoreFuntion();
            Audio.clip = AudioClips[1];
            Audio.Play();

            yield return new WaitForSeconds(0.15f);

            Destroy(this.gameObject);
        }

        if (other.gameObject.GetComponent<GoalPoint_1_Script>() != null || other.gameObject.GetComponent<GoalPoint_3_Script>() != null || other.gameObject.GetComponent<GoalPoint_4_Script>() != null)
        {

            scoreManagerScriptReference.ScoreFuntion2();
            Audio.clip = AudioClips[2];
            Audio.Play();

            yield return new WaitForSeconds(0.15f);

            Destroy(this.gameObject);

        }

    }

   
}
