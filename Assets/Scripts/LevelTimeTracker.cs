using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimeTracker : MonoBehaviour
{
    public Slider timeSlider;
    public float levelDurationTime = 15f;
    private float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeSlider.value = (levelDurationTime - elapsedTime) / levelDurationTime;
        if (elapsedTime >= levelDurationTime)
        {
            GameEvents.OnLevelTimeRanOut?.Invoke();
        }
    }
}
