using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    private void Update()
    {
        Debug.Log("blabla");
    }

    public void ReloadAScene()
    {
        Debug.Log("loading scene");
        SceneManager.LoadSceneAsync("LevelScene");
    }
    
}
