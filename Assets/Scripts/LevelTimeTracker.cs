using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTimeTracker : MonoBehaviour
{
    public Slider timeSlider;
    public float levelDurationTime = 15f;
    public GameObject roundOverTextObject;
    public GameObject endLevelButtons;
    
    
    private float elapsedTime = 0f;
    List<GameObject> spawners = new List<GameObject>();
    public Transform animalsFoldersFolder;


    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeSlider.value = (levelDurationTime - elapsedTime) / levelDurationTime;
        if (elapsedTime >= levelDurationTime)
        {
            timeSlider.fillRect.gameObject.SetActive(false);
            GameEvents.OnLevelTimeRanOut?.Invoke();
            TriggerEndOfLevel();
        }
    }
    private int GetTotalAliveAnimals()
    {
        if (animalsFoldersFolder == null) return 0;
        
        int total = 0;
        foreach (Transform animalFolder in animalsFoldersFolder)
        {
            total += animalFolder.childCount;
        }

        return total;
    }
    void TriggerEndOfLevel()
    {
        GameEvents.OnDisableInput?.Invoke();
        GameEvents.OnReportAliveAnimalsCount?.Invoke(GetTotalAliveAnimals());
        roundOverTextObject.SetActive(true);
        endLevelButtons.SetActive(true);
        Time.timeScale = 0f;
    }
    

    
    
    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync("LevelScene");
    }

    public void LoadUpgradesScene()
    {
        SceneManager.LoadSceneAsync("UpgradesScene");
    }
    
}
