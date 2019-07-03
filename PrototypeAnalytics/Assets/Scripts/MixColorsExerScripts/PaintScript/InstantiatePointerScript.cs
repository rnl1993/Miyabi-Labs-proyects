using UnityEngine;

public class InstantiatePointerScript : MonoBehaviour {

    public GameObject ColorPointer;

    // Instancio el pointer de los colores que ha seleccionado el niño.
    public void Pointer(){

        Instantiate(ColorPointer,this.transform.position,Quaternion.identity);

    }

}
