using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    public GameObject originalball;

    public GameObject hp10;
    public GameObject hp25;

    public GameObject maxHPIncrement;
    public GameObject shootingSpeedIncrementer;
    
    public Transform referenciaDePosicion;
    public float hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        referenciaDePosicion = gameObject.transform;
        StartCoroutine(ataqueEnemigo());
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;


        //print("ENEMY HP: " + hp.ToString());
        

        if (hp == 0)
        {
            float prob = Random.Range(1, 100);
            if (prob >= 95){
                Instantiate(hp25, new Vector3(referenciaDePosicion.transform.position.x,
                referenciaDePosicion.transform.position.y,
                referenciaDePosicion.transform.position.z), gameObject.transform.rotation);
            } else if (prob <= 70){
                Instantiate(hp10, new Vector3(referenciaDePosicion.transform.position.x,
                referenciaDePosicion.transform.position.y,
                referenciaDePosicion.transform.position.z), gameObject.transform.rotation);
            } else if (prob > 70 && prob <= 80){
                Instantiate(maxHPIncrement, new Vector3(referenciaDePosicion.transform.position.x,
                referenciaDePosicion.transform.position.y,
                referenciaDePosicion.transform.position.z), gameObject.transform.rotation);
            } else if (prob > 78 && prob < 95){
                Instantiate(shootingSpeedIncrementer, new Vector3(referenciaDePosicion.transform.position.x,
                referenciaDePosicion.transform.position.y,
                referenciaDePosicion.transform.position.z), gameObject.transform.rotation);
            }
            
            Destroy(gameObject);
            
        }
        
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            hp -= 10;
        }
    }

    void OnDrawGizmos()
    {
        // GIZMOS
        // elementos graficos visibles solo en editor
        // su fin es informar a los devs

        // para dibujar primero especificamos color
        Gizmos.color = Color.red;

        // y luego dibujamos

        //Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }

    IEnumerator ataqueEnemigo()
    {
        while (true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                float d = Vector3.Distance(transform.position, hit.transform.position);

                if (hit.transform.name == "Player" && d <= 20)
                {
                    Instantiate(originalball,
                new Vector3(referenciaDePosicion.transform.position.x,
                referenciaDePosicion.transform.position.y + 1.1f,
                referenciaDePosicion.transform.position.z), gameObject.transform.rotation);

                }
            }

            yield return new WaitForSeconds(0.55f);
        }
    }

}
