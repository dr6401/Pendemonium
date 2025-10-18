using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Transform[] cameraPositions;
    public float moveSpeed = 5f;
    public float zoomSpeed = 20f;
    public float minFOV = 5f;
    public float maxFOV = 60f;
    private float scroll;
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
        scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView -= scroll * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);
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
