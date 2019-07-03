using UnityEngine;

public class FlusherScript : MonoBehaviour{

    // Script References
    private RaycastPalleteScript GetRaycastPalleteScript;

    private MinReactTime GetMinReactTime;
    private MaxReactTime GetMaxReactTime;
    private CorrectColor GetCorrectColor;
    private ReactionTime GetReactionTime;
    private CorrectObject GetCorrectObject;
    private IncorrectColor GetIncorrectColor;
    private IncorrectObject GetIncorrectObject;
    private CorrectInstruction GetCorrectInstruction;
    private BillboardsCompleted GetBillboardsCompleted;
    private IncorrectInstruction GetIncorrectInstruction;
    private CorrectColorIncorrectObject GetCorrectColorIncorrectObject;
    private CorrectObjectIncorrectColor GetCorrectObjectIncorrectColor;

    // Use this for initialization
    void Start(){

        GetRaycastPalleteScript = FindObjectOfType<RaycastPalleteScript>();

        GetMinReactTime = FindObjectOfType<MinReactTime>();
        GetMaxReactTime = FindObjectOfType<MaxReactTime>();
        GetCorrectColor = FindObjectOfType<CorrectColor>();
        GetReactionTime = FindObjectOfType<ReactionTime>();
        GetCorrectObject = FindObjectOfType<CorrectObject>();
        GetIncorrectColor = FindObjectOfType<IncorrectColor>();
        GetIncorrectObject = FindObjectOfType<IncorrectObject>();
        GetCorrectInstruction = FindObjectOfType<CorrectInstruction>();
        GetBillboardsCompleted = FindObjectOfType<BillboardsCompleted>();
        GetIncorrectInstruction = FindObjectOfType<IncorrectInstruction>();
        GetCorrectColorIncorrectObject = FindObjectOfType<CorrectColorIncorrectObject>();
        GetCorrectObjectIncorrectColor = FindObjectOfType<CorrectObjectIncorrectColor>();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función va a llamar las funciones de las metricas que trabajen cuando el raycast selecciona un objeto.
    public void CheckObjectMetrics(){

        GetReactionTime.ReactionTimePerInstruction();
        GetMinReactTime.ReactionTimePerInstruction();
        GetMaxReactTime.ReactionTimePerInstruction();
        GetBillboardsCompleted.CheckIfBillboardIsCompleted();
        GetCorrectColor.ColorIsCorrect(GetRaycastPalleteScript.choseColor, GetRaycastPalleteScript.choseColor2);
        GetIncorrectColor.ColorIsIncorrect(GetRaycastPalleteScript.choseColor, GetRaycastPalleteScript.choseColor2, GetRaycastPalleteScript.ChoseFigure);
        GetCorrectObject.ObjectIsCorrect(GetRaycastPalleteScript.ChoseFigure, GetRaycastPalleteScript.ChoseSize, GetRaycastPalleteScript.ChoseSide);
        GetIncorrectObject.ObjectIsIncorrect(GetRaycastPalleteScript.ChoseFigure, GetRaycastPalleteScript.ChoseSize, GetRaycastPalleteScript.ChoseSide);
        GetCorrectInstruction.InstructionIsIncorrect(GetRaycastPalleteScript.ChoseFigure, GetRaycastPalleteScript.ChoseSize, GetRaycastPalleteScript.ChoseSide, GetRaycastPalleteScript.choseColor, GetRaycastPalleteScript.choseColor2);
        GetIncorrectInstruction.InstructionIsIncorrect(GetRaycastPalleteScript.ChoseFigure, GetRaycastPalleteScript.ChoseSize, GetRaycastPalleteScript.ChoseSide, GetRaycastPalleteScript.choseColor, GetRaycastPalleteScript.choseColor2);
        GetCorrectColorIncorrectObject.ColorIsCorrectObjectIsIncorrect(GetRaycastPalleteScript.ChoseFigure, GetRaycastPalleteScript.ChoseSize, GetRaycastPalleteScript.ChoseSide, GetRaycastPalleteScript.choseColor, GetRaycastPalleteScript.choseColor2);
        GetCorrectObjectIncorrectColor.ObjectIsCorrectColorIsIncorrect(GetRaycastPalleteScript.ChoseFigure, GetRaycastPalleteScript.ChoseSize, GetRaycastPalleteScript.ChoseSide, GetRaycastPalleteScript.choseColor, GetRaycastPalleteScript.choseColor2);
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    /* Esta función va a recolectar la cuenta final de todas las metricas al final de cada instrucción, para que 
    esa información se pueda escribir en el documento de texto. */
    public void FlushPerInstructionsValues(){

        GetMinReactTime.CopyCountMetricPerInstruction();
        GetMaxReactTime.CopyCountMetricPerInstruction();
        GetCorrectColor.CopyCountMetricPerInstruction();
        GetReactionTime.CopyCountMetricPerInstruction();
        GetCorrectObject.CopyCountMetricPerInstruction();
        GetIncorrectColor.CopyCountMetricPerInstruction();
        GetIncorrectObject.CopyCountMetricPerInstruction();
        GetCorrectInstruction.CopyCountMetricPerInstruction();
        GetIncorrectInstruction.CopyCountMetricPerInstruction();
        GetCorrectColorIncorrectObject.CopyCountMetricPerInstruction();
        GetCorrectObjectIncorrectColor.CopyCountMetricPerInstruction();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función va a llamar la cuenta final de todas las metricas para poder hacer el “write” final.
    public void FlushFinalResults(){

        GetMinReactTime.EndGameMetricValues();
        GetMaxReactTime.EndGameMetricValues();
        GetCorrectColor.EndGameMetricValues();
        GetReactionTime.EndGameMetricValues();
        GetCorrectObject.EndGameMetricValues();
        GetIncorrectColor.EndGameMetricValues();
        GetIncorrectObject.EndGameMetricValues();
        GetCorrectInstruction.EndGameMetricValues();
        GetBillboardsCompleted.EndGameMetricValues();
        GetIncorrectInstruction.EndGameMetricValues();
        GetCorrectColorIncorrectObject.EndGameMetricValues();
        GetCorrectObjectIncorrectColor.EndGameMetricValues();
    }
}