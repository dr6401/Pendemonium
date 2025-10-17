using System;
using UnityEngine;

public class Fence : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            Destroy(other.gameObject);
            Debug.Log("Animal escaped");
        }
    }
}
