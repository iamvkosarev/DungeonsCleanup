using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Button
{
    [SerializeField] private MovementButtonsManager mBM;
    [SerializeField] private Vector2 throwingResults;

    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        mBM.AddValueInResult(throwingResults.x, throwingResults.y);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        mBM.AddValueInResult(-throwingResults.x, -throwingResults.y);
    }
}
