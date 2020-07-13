using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr Instance;

    private void Awake()
    {
        if(Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        //인스턴스가 없을때
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

}
