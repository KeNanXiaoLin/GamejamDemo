using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// 管理所有UI面板的管理器
/// </summary>
public class UIManager
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new();
            return _instance;
        }
    }
    private Camera uiCamera;
    private Transform uiRoot;
    private EventSystem uiEventSystem;

    /// <summary>
    /// 用于存储所有的面板对象
    /// </summary>
    private Dictionary<string, UIPanelBase> panelDic = new Dictionary<string, UIPanelBase>();

    private UIManager()
    {
        //动态创建唯一的Canvas
        uiCamera = GameObject.Instantiate(Resources.Load<GameObject>("UI/UICamera")).GetComponent<Camera>();
        //ui摄像机过场景不移除 专门用来渲染UI面板
        GameObject.DontDestroyOnLoad(uiCamera.gameObject);
        //动态创建UI根节点，所有的UI都放在这个节点下
        uiRoot = GameObject.Instantiate(Resources.Load<GameObject>("UI/UIRoot")).transform;
        //ui摄像机过场景不移除 专门用来渲染UI面板
        GameObject.DontDestroyOnLoad(uiRoot.gameObject);

        //动态创建EventSystem
        uiEventSystem = GameObject.Instantiate(Resources.Load<GameObject>("UI/EventSystem")).GetComponent<EventSystem>();
        GameObject.DontDestroyOnLoad(uiEventSystem.gameObject);
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <param name="layer">面板显示的层级</param>
    /// <param name="callBack">由于可能是异步加载 因此通过委托回调的形式 将加载完成的面板传递出去进行使用</param>
    /// <param name="isSync">是否采用同步加载 默认为false</param>
    public void ShowPanel<T>(UnityAction<T> callBack = null) where T:UIPanelBase
    {
        //获取面板名 预设体名必须和面板类名一致 
        string panelName = typeof(T).Name;
        //存在面板
        if(panelDic.ContainsKey(panelName))
        {
            //如果要显示面板 会执行一次面板的默认显示逻辑
            panelDic[panelName].ShowMe();
            //如果存在回调 直接返回出去即可
            callBack?.Invoke(panelDic[panelName] as T);
            return;
        }

        //不存在面板 加载面板
        GameObject prefab = Resources.Load<GameObject>("UI/" + panelName);
        if (prefab == null)
        {
            Debug.LogError("面板不存在，请检测路径下是否存在面板,面板加载路径" + "UI/" + panelName);
        }
        GameObject panelObj = GameObject.Instantiate(prefab, uiRoot, false);
        //获取对应UI组件返回出去
        T panel = panelObj.GetComponent<T>();
        panel.SetCamera(uiCamera);
        //显示面板时执行的默认方法
        panel.ShowMe();
        //传出去使用
        callBack?.Invoke(panel);
        //存储panel
        panelDic.Add(panelName, panel);
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T">面板类型</typeparam>
    public void HidePanel<T>() where T : UIPanelBase
    {
        string panelName = typeof(T).Name;
        if(panelDic.ContainsKey(panelName))
        {
            //执行默认的隐藏面板想要做的事情
            panelDic[panelName].HideMe();
            //销毁面板
            GameObject.Destroy(panelDic[panelName].gameObject);
            //从容器中移除
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    public T GetPanel<T>() where T:UIPanelBase
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;
    }
}
