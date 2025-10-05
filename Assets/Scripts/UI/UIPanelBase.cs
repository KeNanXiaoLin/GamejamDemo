using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIPanelBase : MonoBehaviour
{
    /// <summary>
    /// 用于存储所有要用到的UI控件，用历史替换原则 父类装子类
    /// </summary>
    protected Dictionary<string, UIBehaviour> controlDic = new Dictionary<string, UIBehaviour>();
    private Canvas uiCanvas;

    protected virtual void Awake()
    {
        uiCanvas = GetComponent<Canvas>();
        //为了避免 某一个对象上存在两种控件的情况
        //我们应该优先查找重要的组件
        FindChildrenControl();
    }

    /// <summary>
    /// 面板显示时会调用的逻辑
    /// </summary>
    public abstract void ShowMe();

    /// <summary>
    /// 面板隐藏时会调用的逻辑
    /// </summary>
    public abstract void HideMe();

    /// <summary>
    /// 获取指定名字以及指定类型的组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="name">组件名字</param>
    /// <returns></returns>
    public T GetControl<T>(string name) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(name))
        {
            T control = controlDic[name] as T;
            if (control == null)
                Debug.LogError($"不存在对应名字{name}类型为{typeof(T)}的组件");
            return control;
        }
        else
        {
            Debug.LogError($"不存在对应名字{name}的组件");
            return null;
        }
    }

    protected virtual void ClickBtn(string btnName)
    {

    }

    protected virtual void SliderValueChange(string sliderName, float value)
    {

    }

    protected virtual void ToggleValueChange(string sliderName, bool value)
    {

    }

    private void FindChildrenControl()
    {
        Debug.Log(gameObject.GetInstanceID());
        UIBehaviour[] controls = this.GetComponentsInChildren<UIBehaviour>(true);
        for (int i = 0; i < controls.Length; i++)
        {
            //获取当前控件的名字
            string controlName = controls[i].gameObject.name;
            //根据控件的名字，解析控件
            if (controlName.Contains("[") && controlName.Contains("]"))
            {
                int nameIndex = controlName.IndexOf("]") + 1;
                string type = controlName.Substring(1, nameIndex - 2);
                string name = controlName.Substring(nameIndex);

                if (controls[i] is Button btn && !controlDic.ContainsKey(name))
                {
                    btn.onClick.AddListener(() =>
                    {
                        ClickBtn(name);
                    });
                    controlDic.Add(name, controls[i]);
                    Debug.Log($"类型是{type},名字是{name},加入字典");
                }
                else if (controls[i] is Toggle tog && !controlDic.ContainsKey(name))
                {
                    tog.onValueChanged.AddListener((value) =>
                    {
                        ToggleValueChange(name, value);
                    });
                    controlDic.Add(name, controls[i]);
                    Debug.Log($"类型是{type},名字是{name},加入字典");
                }
                else if (controls[i] is InputField ipd && !controlDic.ContainsKey(name))
                {
                    controlDic.Add(name, controls[i]);
                    Debug.Log($"类型是{type},名字是{name},加入字典");
                }
                else if (controls[i] is Slider sld && !controlDic.ContainsKey(name))
                {
                    sld.onValueChanged.AddListener((value) =>
                    {
                        SliderValueChange(name, value);
                    });
                    controlDic.Add(name, controls[i]);
                    Debug.Log($"类型是{type},名字是{name},加入字典");
                }
            }
        }
    }

    public void SetCamera(Camera camera)
    {
        uiCanvas.worldCamera = camera;
    }
}
