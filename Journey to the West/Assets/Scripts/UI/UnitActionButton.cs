using System;
using TMPro;
using UnityEngine;

public class UnitActionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text ActionName;
    private Action buttonAction;


    public void SetUpButton(string text, Action buttonAction)
    {
        ActionName.text = text;
        this.buttonAction = buttonAction;
    }

    public void PreformAction()
    {
        buttonAction?.Invoke();
        UnitActionSystem.Instance.GetSelectedUnit().GetComponent<Unit>().CreateActions();
    }
}
