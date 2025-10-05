using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_DialogType
{
    MainRole,
    Mom,
    God
}
[CreateAssetMenu(fileName = "DialogData", menuName = "DialogData", order = 1)]
public class DialogData : ScriptableObject
{
    public int id;
    //当前说话的对象
    public E_DialogType type;
    public string content;
    public DialogData nextData;
    public bool isEnd;
    public bool isStart;
}