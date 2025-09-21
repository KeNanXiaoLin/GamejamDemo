using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 一个测试脚本
/// </summary>
public class Test : MonoBehaviour
{
    public Transform cubeTransform;
    void Start()
    {
        #region 测试移动、旋转、缩放动画
        // 移动到目标位置，持续1秒
        cubeTransform.DOMove(new Vector3(0, 5, 0), 1f);
        cubeTransform.DOScale(new Vector3(2, 2, 2), 1f);
        cubeTransform.DORotate(new Vector3(0, 90, 90), 1f);
        #endregion
        Debug.Log("测试");
    }
}
