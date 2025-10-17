using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimeTracker : MonoBehaviour
{
    public Slider timeSlider;
    public float levelDurationTime = 15f;
    public GameObject roundOverTextObject;
    public GameObject endLevelButtons;
    
    private float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeSlider.value = (levelDurationTime - elapsedTime) / levelDurationTime;
        if (elapsedTime >= levelDurationTime)
        {
            timeSlider.fillRect.gameObject.SetActive(false);
            GameEvents.OnLevelTimeRanOut?.Invoke();
            GameEvents.OnDisableInput?.Invoke();
            roundOverTextObject.SetActive(true);
            endLevelButtons.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
}
