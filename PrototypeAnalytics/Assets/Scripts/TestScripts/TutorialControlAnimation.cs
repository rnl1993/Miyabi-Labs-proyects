using System.Collections;
using UnityEngine;

public class TutorialControlAnimation : MonoBehaviour {

    public float AnimationDelay;
    public float AudioTiming;

    private Renderer Rend;
    private Animator animator;
    private AudioSource GetAudioSource;

    // 
    IEnumerator LateStart(){
        
        yield return new WaitForSeconds(AnimationDelay);
        animator.SetBool("PointAtColors", true);

        yield return new WaitForSeconds(AudioTiming);
        FindObjectOfType<SelectedColorEffectTutorialScript>().SelectedEffect();
        GetAudioSource.Play();

    }

    void OnEnable()
    {
        GetAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Rend = GetComponent<Renderer>();
        StartCoroutine(FadeIn());
        StartCoroutine(LateStart());
    }

    IEnumerator FadeIn()
    {
        for (float i = 0; i < 1; i+=0.1f){

            Rend.material.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, i));

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SecondClick(){

        yield return new WaitForSeconds(AudioTiming);
        FindObjectOfType<SelectedObjectEffectTutorialScript>().SelectedEffect();
        GetAudioSource.Play();
    }

    public void SecondAnimation(){

        animator.SetBool("PointAtObject", true);
        StartCoroutine(SecondClick());
    }
}
