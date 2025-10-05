using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogPanel : UIPanelBase
{
    public TextMeshProUGUI contentTMP;

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
            case "Next":
                //通知对话系统播放下一段对话
                DialogSystem.Instance.TriggerNextDialog();
                break;
        }
    }

    public void UpdateDialogText(string content)
    {
        contentTMP.text = content;
    }
}
