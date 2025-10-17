using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<Transform> animalFolders;

    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AreAllSpawnersExtinct())
        {
            Debug.Log("YOU LOST");
        }
    }

    bool AreAllSpawnersExtinct()
    {
        foreach (var animalFolder in animalFolders)
        {
            if (animalFolder.childCount >= 0)
            {
                return false;
            }
        }
        return true;
    }
    
    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void LoadUpgradesScene()
    {
        SceneManager.LoadSceneAsync("UpgradesScene");
    }

    public void EndLevel()
    {
        Debug.Log("Timer ran out!");
    }

    private void OnEnable()
    {
        GameEvents.OnLevelTimeRanOut += EndLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelTimeRanOut -= EndLevel;
    }
}
