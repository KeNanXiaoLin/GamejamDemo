using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 一个测试脚本
/// </summary>
public class Test : MonoBehaviour
{
    float x, y;
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        transform.Translate(Time.deltaTime * x * 2f, Time.deltaTime * y * 2f, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            SceneManager.UnloadScene("GameScene");
            SceneManager.LoadScene("GameScene2",LoadSceneMode.Additive);
        }
    }
}
