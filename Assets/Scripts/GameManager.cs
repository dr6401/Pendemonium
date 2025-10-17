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

    }
    

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
}
