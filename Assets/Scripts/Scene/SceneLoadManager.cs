using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager
{
    private static SceneLoadManager _instance;
    public static SceneLoadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new();
            }
            return _instance;
        }
    }

    public void LoadScene(string name)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
    }

    private IEnumerator LoadSceneCoroutine(string name)
    {
        yield return SceneManager.LoadSceneAsync(name);
    }
}
