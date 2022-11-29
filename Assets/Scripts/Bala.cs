using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bala : MonoBehaviour
{

    [SerializeField]

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        // NO es obligatorio destrucción por tiempo
        // PERO es indispensable alguna estrategia de destrucción 
        // para objetos que se vayan a instanciar dinámicamente
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {



    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * 2, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}