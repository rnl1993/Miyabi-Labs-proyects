using UnityEngine;

public class Right : MonoBehaviour {

    [HideInInspector] public bool RightBillboardCounted;
    [HideInInspector] public bool isCountedRight;
    [HideInInspector] public Renderer[] rend;

    private ObjToPaint GetObjToPaint;

    private void Start()
    {
        GetObjToPaint = GetComponent<ObjToPaint>();
        rend = GetComponentsInChildren<Renderer>();
    }

    public void IsCounted()
    {
        RightBillboardCounted = GetObjToPaint.colored;
    }
}
