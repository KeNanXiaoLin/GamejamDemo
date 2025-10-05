using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : UIPanelBase
{
    public DialogData data;
    public override void HideMe()
    {

    }

    public override void ShowMe()
    {

    }

    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Start":
                DialogSystem.Instance.TriggerStartDialog(data);
                break;
        }
    }
}
