using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartUI : UIPanelBase
{
    private Button btnStart;
    private Button btnSetting;
    private Button btnQuit;

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
                //切换场景到游戏场景
                UIManager.Instance.HidePanel<GameStartUI>();
                SceneLoadManager.Instance.LoadScene("GameScene");
                break;
            case "Setting":
                //打开设置面板
                UIManager.Instance.ShowPanel<SettingPanelUI>();
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }

}
