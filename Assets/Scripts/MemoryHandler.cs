using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
