using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public GameObject button;
    public Material materialverde;
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
        if (rend.sharedMaterial == materialverde)
        {
            transform.Translate(0, -200, 0);
        }



    }

    void OnTriggerEnter(Collider other) {

                

    }

}
