using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    [Header("Movement")]
    public Camera mainCamera;
    public float lerpSpeed = 5f;          // how fast it follows the mouse
    public float height = 1f;             // y-position of the black hole
    public bool useSceneObjectHeight = true;

    [Header("Pull")]
    public float pullRadius = 5f;
    public float pullForce = 50f;
    public LayerMask pullLayers;          // e.g., "Chicken"

    private Rigidbody rb;

    void Start()
    {
        if (useSceneObjectHeight)
        {
            height = transform.position.y;   
        }
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;            // controlled by script
        pullForce *= 100;
    }

    void Update()
    {
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
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }
    }

    void PullObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pullRadius, pullLayers);
        foreach (Collider col in colliders)
        {
            Debug.Log("name of collider pulled: " + col.name);
            Rigidbody objRb = col.attachedRigidbody;
            if (objRb != null)
            {
                Vector3 dir = (transform.position - objRb.position).normalized;
                Debug.Log("Got the attached rb");
                // optional: add slight upward force to look like "lifting"
                Vector3 pull = dir + Vector3.up * 0.5f;

                objRb.AddForce(pull.normalized * pullForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}