using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ButtonBehaviour : MonoBehaviour
{
    public Material materialverde;
    public Material materialrojo;
    public GameObject door;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (rend.sharedMaterial != materialverde)
            {
                rend.sharedMaterial = materialverde;
                Destroy(collision.gameObject);
                door.transform.Translate(0, 0, -3000 * Time.deltaTime);

            } else if (rend.sharedMaterial != materialrojo)
            {
                rend.sharedMaterial = materialrojo;
                Destroy(collision.gameObject);
                door.transform.Translate(0, 0, 3000 * Time.deltaTime);
            }
            
        }
        
    }
}
