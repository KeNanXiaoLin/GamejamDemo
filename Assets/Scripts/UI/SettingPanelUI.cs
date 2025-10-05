using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelUI : UIPanelBase
{
    public override void HideMe()
    {

    }

    public override void ShowMe()
    {

    }

    protected override void SliderValueChange(string sliderName, float value)
    {
        switch (sliderName)
        {
            case "Music":
                break;
            case "Sound":
                break;
        }
    }

    protected override void ToggleValueChange(string sliderName, bool value)
    {
        switch (sliderName)
        {
            case "Music":
                break;
            case "Sound":
                break;
        }
    }

    protected override void ClickBtn(string btnName)
    {
        Debug.Log("按钮被点击" + btnName);
        switch (btnName)
        {
            case "Close":
                UIManager.Instance.HidePanel<SettingPanelUI>();
                break;
        }
    }
}
