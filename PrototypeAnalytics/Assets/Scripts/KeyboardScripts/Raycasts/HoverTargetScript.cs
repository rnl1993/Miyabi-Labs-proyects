using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverTargetScript : MonoBehaviour {

    // Variables
    private Vector3 OGSize;
    private bool testBool;

    // script references
    private HoverScript hover;
    private KeepSize keepSize;
    private ObjToPaint GetObjToPaint;
    private InstructionsScript GetInstructions;

    // Functions

    // esta función simplemente guarda el tamaño original de las teclas, para que puedan ser "reseteadas" cuando dejas de apuntarlas. 
    void Start () {

        keepSize = GetComponent<KeepSize>();
        hover = FindObjectOfType<HoverScript>();
        GetObjToPaint = GetComponent<ObjToPaint>();
        GetInstructions = FindObjectOfType<InstructionsScript>(); 
        OGSize = this.transform.localScale;
        DetectSizes();
    }

    void Update () {

        if (SceneManager.GetActiveScene().name != "ColorObjectsExercises" || FindObjectOfType<ColorPalleteScript>() !=null){

            if (this.transform.localScale != OGSize && hover.big == false){

                OriginalSize();
            }         }

        if (SceneManager.GetActiveScene().name == "ColorObjectsExercises" && testBool == true){
            
            if (this.transform.localScale != OGSize && hover.big == false){

            if (GetInstructions.Level >= 3){

                if (GetObjToPaint != null){

                    if (this.GetObjToPaint.IsBig == true && keepSize == null){
                            
                        this.transform.localScale = OGSize;

                    }
                    if (GetObjToPaint.IsBig == false && keepSize == null){
                            
                        this.transform.localScale = OGSize;
                        }
                    }
                }
            }
        }
    }

    /*  
        Esta función se utiliza en todas las escenas que utilicen el hover script.
        Tambien todos los objectos que no cambien su tamaño al azar.
     */
     
    public void OriginalSize(){ this.transform.localScale = OGSize; }

    /*  
        Esta función guarda el tamaño que se le asigna al azar a los objectos que se van a pintar.
        Esta función solo funciona cuando estamos en el juego de pintar los objectos de los colores
        que te piden.

     */
   public void DetectSizes(){
        StartCoroutine(test());
       
    }

    IEnumerator test(){

        yield return new WaitForSeconds(0.1f);

        if (GetInstructions != null && GetInstructions.Level >= 3){

            if (GetObjToPaint != null){

                if (this.GetObjToPaint.IsBig == true && keepSize == null){

                    OGSize = GetObjToPaint.ObjectIsBigVector;
                    this.transform.localScale = OGSize;
                }
                if (GetObjToPaint.IsBig == false && keepSize == null){

                    OGSize = GetObjToPaint.ObjectIsSmallVector;
                    this.transform.localScale = OGSize;
                }
            }
        }
        testBool = true;
    }
}