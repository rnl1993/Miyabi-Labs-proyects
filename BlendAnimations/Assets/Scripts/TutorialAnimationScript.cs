using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAnimationScript : MonoBehaviour {

    // Variables

    public AnimationClip[] GetClips;
    public Button DanceButtonUI;
    public Button SaluteButtonUI;

    private Animator anim;
    private AnimatorStateInfo stateInfo;
    private float waitTime;

    // Functions
    void Start () {

        anim = GetComponentInChildren<Animator>();

	}

    //__________________________________________________________________________

    IEnumerator DanceRoutine(){

        waitTime = GetClips[0].length;
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("Dance_2",true);
        anim.Play("Dance_2", 0, 0.44f);
        waitTime = GetClips[1].length;
        yield return new WaitForSeconds(waitTime - (waitTime * 0.44f));
        anim.SetBool("Dance", false);
        anim.SetBool("Dance_2", false); 
        SaluteButtonUI.enabled = true;
        SaluteButtonUI.enabled = true;
    }

    //__________________________________________________________________________

    IEnumerator SaluteRoutine(){

        waitTime = GetClips[2].length;
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("Salute", true);
        anim.Play("Standing Greeting",0, 0.25f);
        waitTime = GetClips[3].length;
        yield return new WaitForSeconds(waitTime - (waitTime * 0.25f));
        waitTime = GetClips[4].length;
        anim.SetBool("Salute2", true);
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("Salute2", false);
        anim.SetBool("Salute", false);
        anim.SetBool("Bow", false);
        DanceButtonUI.enabled = true;
        SaluteButtonUI.enabled = true;
    }

    //__________________________________________________________________________

    public void DanceButton(){

        anim.SetBool("Dance", true);
        StartCoroutine(DanceRoutine());
        print("DanceButton Pressed");
        SaluteButtonUI.enabled = false;
        DanceButtonUI.enabled = false;
    }

    //__________________________________________________________________________

    public void SaluteButton(){

        anim.SetBool("Bow", true);
        StartCoroutine(SaluteRoutine());
        print("SaluteButton Pressed");
        DanceButtonUI.enabled = false;
        SaluteButtonUI.enabled = false;

    }

    //__________________________________________________________________________

    public void CancelButton(){

        print("Cancelar animaciones.");
        StopAllCoroutines();
        anim.SetBool("Dance", false);
        anim.SetBool("Dance_2", false);
        anim.SetBool("Salute2", false);
        anim.SetBool("Salute", false);
        anim.SetBool("Bow", false);
        DanceButtonUI.enabled = true;
        SaluteButtonUI.enabled = true;
    }
}
