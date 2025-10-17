using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : MonoBehaviour
{
    public float wanderForceMin = 10f;
    public float wanderForceMax = 20f;
    //public float rotationJitter = 30f;      // How much it randomly rotates per second
    public float changeDirectionIntervalMin = 5f;
    public float changeDirectionIntervalMax = 10f;
    public float minWaitTimer = 0.5f;
    public float maxWaitTimer = 3f;
    public float panicDistance = 5f;        // Distance to vacuum before panicking
    public float panicForce = 10f;          // Extra force when near vacuum
    public Transform vacuum;                // Assign your vacuum here

    private Rigidbody rb;
    private Vector3 wanderDir;
    private float timer;
    private float wanderTimer;
    private float wanderDuration;
    private float waitDuration;
    private float waitTimer;
    private bool isWaiting = false;
    private float wanderStrength;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PickNewDirection();
        SetNextWanderDuration();
        SetNextWaitDuration();
        PickNewWanderForce();
        Debug.Log($"New direction: {wanderDir}, Wander duration: {wanderDuration}, New wander Force: {wanderStrength}");
    }

    void FixedUpdate()
    {
        if (isWaiting)
        {
            waitTimer += Time.fixedDeltaTime;
            if (waitTimer >= waitDuration)
            {
                isWaiting = false;
                SetNextWanderDuration();
                PickNewDirection();
                PickNewWanderForce();
                wanderTimer = 0f;
                //Debug.Log("Finished waiting, picking new direction");
            }
            return;
        }

        // Apply wandering force
        rb.AddForce(wanderDir * wanderStrength, ForceMode.Acceleration);

        // Panic check
        if (vacuum != null)
        {
            Vector3 toVacuum = vacuum.position - transform.position;
            if (toVacuum.magnitude < panicDistance)
            {
                rb.AddForce(-toVacuum.normalized * panicForce, ForceMode.Force);
            }
        }

        // Wander timer
        wanderTimer += Time.fixedDeltaTime;
        if (wanderTimer >= wanderDuration)
        {
            isWaiting = true;
            waitTimer = 0f;
            SetNextWaitDuration();
            Debug.Log($"Entering wait phase for {waitDuration} seconds");
        }
    }

    void PickNewDirection()
    {
        // Pick a completely random horizontal direction
        wanderDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        Debug.Log($"New wander direction: {wanderDir}");
    }

    void PickNewWanderForce()
    {
        wanderStrength = Random.Range(wanderForceMin, wanderForceMax);
        Debug.Log($"New wander strength: {wanderStrength}");
    }

    void SetNextWanderDuration()
    {
        wanderDuration = Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
        Debug.Log($"New wander duration: {wanderDuration}");
    }
    void SetNextWaitDuration()
    {
        waitDuration = Random.Range(minWaitTimer, maxWaitTimer);
        Debug.Log($"New wait duration: {waitDuration}");
    }

    /*void OnDrawGizmos()
    {
        // Set color
        Gizmos.color = Color.red;

        // Draw a circle in XZ plane around the chicken
        Gizmos.DrawSphere(transform.position, panicDistance);
    }*/
}
