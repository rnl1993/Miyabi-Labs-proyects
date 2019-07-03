using System.Collections;
using UnityEngine;

public class SelectedColorEffectTutorialScript : MonoBehaviour {

    private AudioSource GetAudioSource;
    private Vector3 InitialSize;

	// Use this for initialization
	void Start () {
        GetAudioSource = GetComponent<AudioSource>();
        InitialSize = this.transform.localScale;
	}
	
    public void SelectedEffect()
    {

        this.transform.localScale = InitialSize * 1.1f;
        GetAudioSource.Play();
        StartCoroutine(BackToNormalSize());
    }

    IEnumerator BackToNormalSize(){

        yield return new WaitForSeconds(0.7f);
        this.transform.localScale = InitialSize;
    }
}
