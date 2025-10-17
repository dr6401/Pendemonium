using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentAliveAnimals;
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

    }

    void SetCurrentAliveAnimals(int aliveAnimals)
    {
        currentAliveAnimals = aliveAnimals;
    }
    
    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync("LevelScene");
    }

    private void OnEnable()
    {
        GameEvents.OnReportAliveAnimalsCount += SetCurrentAliveAnimals;
    }
    
    private void OnDisable()
    {
        GameEvents.OnReportAliveAnimalsCount -= SetCurrentAliveAnimals;
    }

}
