using UnityEngine;

public class Left : MonoBehaviour {

    [HideInInspector] public bool leftBillboardCounted;
    [HideInInspector] public bool isCountedLeft;
    [HideInInspector] public Renderer[] rend;

    private ObjToPaint GetObjToPaint;

    private void Start()
    {
        GetObjToPaint = GetComponent<ObjToPaint>();
        rend = GetComponentsInChildren<Renderer>();
    }

    public void IsCounted(){

        leftBillboardCounted = GetObjToPaint.colored;
    }
}
