using UnityEngine;

public class SideDistictionScript : MonoBehaviour {

    private ObjToPaint GetObjToPaint;
    private InstructionsScript GetInstructions;

    // Functions

    void Start()
    {
        GetObjToPaint = GetComponent<ObjToPaint>();
        GetInstructions = FindObjectOfType<InstructionsScript>();
        Side();
    }

    void Side(){

        if(GetInstructions.Level == 1){

            this.gameObject.AddComponent<Center>();
        }

        if (GetInstructions.Level >= 2)
        {
            if (this.transform.position.x < 0)
            {
                this.gameObject.AddComponent<Left>();
            }
        }

        if(GetInstructions.Level >= 2){

            if (this.transform.position.x > 0)
            {
                this.gameObject.AddComponent<Right>();
            }

        }

        if (GetInstructions.Level == 5)
        {
            if (this.transform.position.x < 0 && this.GetObjToPaint.ObjectPositions.Length <=1)
            {
                GetObjToPaint.ObjectPositions = "Izquierdo";
            }

            if (this.transform.position.x > 0 && this.GetObjToPaint.ObjectPositions.Length <= 1)
            {
                GetObjToPaint.ObjectPositions = "Derecho";
            }
        }
    }
}
