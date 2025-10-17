using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BlackHoleController : MonoBehaviour
{
    [Header("Movement")]
    public Camera mainCamera;
    public float moveSpeed = 5f;          // how fast it follows the mouse
    public float height = 1f;             // y-position of the black hole
    public bool useSceneObjectHeight = true;

    [Header("Pull")] public bool useSizeAsRadius = true;
    public float pullRadius = 5f;
    public float pullForce = 50f;
    public LayerMask pullLayers;          // e.g., "Chicken"

    private bool inputBlocked = false;
    
    void Start()
    {
        if (useSceneObjectHeight)
        {
            height = transform.position.y;   
        }
        if (useSizeAsRadius)
        {
            pullRadius = transform.localScale.y * 2f;   
        }
    }

    void Update()
    {
        if (inputBlocked) return;
        FollowMouse();
    }

    void FixedUpdate()
    {
        PullObjects();
    }

    void FollowMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * height);
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPos = ray.GetPoint(enter);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    void PullObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pullRadius, pullLayers);
        foreach (Collider col in colliders)
        {
            Rigidbody objRb = col.attachedRigidbody;
            if (objRb != null)
            {
                Vector3 dir = (transform.position - objRb.position).normalized;
                // optional: add slight upward force to look like "lifting"
                Vector3 pull = dir + Vector3.up * 0.25f;

                objRb.AddForce(pull.normalized * (pullForce * Time.fixedDeltaTime), ForceMode.VelocityChange);
            }
        }
    }

    void DisableInput()
    {
        inputBlocked = true;
    }

    private void OnEnable()
    {
        GameEvents.OnDisableInput += DisableInput;
    }

    private void OnDisable()
    {
        GameEvents.OnDisableInput -= DisableInput;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}