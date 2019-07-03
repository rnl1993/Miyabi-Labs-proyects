using UnityEngine;

public class DistractionsScript : MonoBehaviour
{
    // public variables.

    [HideInInspector] public bool Change;
    [HideInInspector] public Renderer rend;
    public Color color = new Color(1, 1, 1, 1);

    // Script references.
    DistractionManagerScript distractionManager;
    Manager_Animals animals;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        distractionManager = FindObjectOfType<DistractionManagerScript>();
    }

    void Update()
    {
        if (FindObjectOfType<Manager_Animals>() == null)
        {

            distractionManager.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    public void ContactColor(){

        if (Change == true){
            rend.material.color = color;
        }
    }

    public void ChangeColor(){

       // if (Change == false){

            rend.material.color = Color.red;
        //}
    }

}
