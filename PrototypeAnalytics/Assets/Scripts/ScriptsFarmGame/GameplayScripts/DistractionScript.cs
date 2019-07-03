using UnityEngine;

public class DistractionScript : MonoBehaviour {

    public Renderer rend;
    public Color color = new Color(1, 1, 1, 1);

    SpawnAnimalsScript animals;
    DistractionManagerScript distractionManager;
    RaycastDistractionsScript raycastDistractions;


    private void Start()
    {
        
        rend = this.GetComponent<Renderer>();
        distractionManager = FindObjectOfType<DistractionManagerScript>();
        raycastDistractions = FindObjectOfType<RaycastDistractionsScript>();
    }

    void Update()
    {

        if(raycastDistractions.Contanct == false){

            ChangeColor();

        }

        if (FindObjectOfType<SpawnAnimalsScript>() == null)
        {

            distractionManager.enabled = false;
            this.gameObject.SetActive(false);

        }

    }

    public void ContactColor()
    {
        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", color);

    }

    public void ChangeColor()
    {


        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", Color.red);

    }

}

