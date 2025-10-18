using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] cameraPositions;
    public float moveSpeed = 5f;
    private int index = 0;
    public PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            Vector3.Slerp(transform.position, cameraPositions[index].position, moveSpeed * Time.deltaTime);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, cameraPositions[index].rotation, moveSpeed * Time.deltaTime);
    }

    public void MoveCameraForward()
    {
        index = (index + 1) % cameraPositions.Length;
    }
    
    public void MoveCameraBackwards()
    {
        index = (index - 1 + cameraPositions.Length) % cameraPositions.Length;
    }

    private void OnEnable()
    {
        controls.Player.MoveCameraForward.performed += ctx => MoveCameraForward();
        controls.Player.MoveCameraBackwards.performed += ctx => MoveCameraBackwards();
    }
}
