using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using System.Collections.Specialized;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMechanics : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rb;

    public float movementSpeed = 5;
    public GameObject original;

    public Transform referenciaDePosicion;

    public float rotationSpeed = 5;
    public float jumpingForce = 650;
    private float shootingSpeed = 0.5f;

    public float hp = 100;
    public float maxHp = 100;

    public double timeSurvived = 0;

    public Text UIText;
    public Text survivedText;

    private IEnumerator shotCoroutine;

    // Start is called before the first frame update
    void Start(){
        // COSA NUEVA
        // obtener referencia a un componente en el mismo gameObject
        // GetComponent en start o awake
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined;

        

    }

    // Update is called once per frame
    void Update() {

        timeSurvived += Time.deltaTime;

        survivedText.text = "Tiempo sobrevivido: " + timeSurvived.ToString();



        if (Input.GetKeyDown(KeyCode.Space))
        {
            //nota 1 de addforce: el vector de fuerza esta en espacio de mundo

            //3 vectores que nos sirven para tomar en cuenta orientacion local en espacio global

            //transform.up
            //transform.right
            //transform.toward

            //vector normalizado (tamanio 1)
            //unit vector
            //sirve para expresar direccion o sentido

            rb.AddForce(transform.up * jumpingForce, ForceMode.Impulse);
        }

        float sides = Input.GetAxis("Horizontal");
        float frontBack = Input.GetAxis("Vertical");

        //print(horizontal);
        transform.Translate(movementSpeed * sides * Time.deltaTime, 0, movementSpeed * frontBack * Time.deltaTime);

        float horizontalRotation = Input.GetAxis("Mouse Y");
        float verticalRotation = Input.GetAxis("Mouse Y");

        //transform.Rotate(-(horizontalRotation * rotationSpeed * Time.deltaTime),
        //(verticalRotation * rotationSpeed * Time.deltaTime), 0);

        

        if (Input.GetMouseButtonDown(0))
        {
            shotCoroutine = disparo();
            StartCoroutine(shotCoroutine);

        }

        if (Input.GetMouseButtonUp(0))
        {
            shotCoroutine = disparo();
            StopAllCoroutines();
        }

        if (Input.GetKeyDown(KeyCode.C)){
            return;
        }

        UIText.text = "HP: " + hp.ToString();

        if (hp <= 0)
        {
            //HACER ALGO AQUI PARA REINICIAR LA ESCENA
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        RaycastHit hit;


        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            float d = Vector3.Distance(transform.position, hit.transform.position);

            if (Input.GetKeyDown(KeyCode.F) && hit.transform.gameObject.layer == 9 && d <= 6.5)
            {
                hit.transform.GetComponent<Rigidbody>().useGravity = false;
                hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                hit.transform.rotation = gameObject.transform.rotation;
                hit.transform.parent = gameObject.transform;

            } else if (Input.GetKeyUp(KeyCode.F)){
                hit.transform.parent = null;
                hit.transform.GetComponent<Rigidbody>().useGravity = true;
                hit.transform.GetComponent<Rigidbody>().isKinematic = false;
                
            }

            
        }
    }

    void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            hp -= 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == 13 || other.gameObject.layer == 14 || other.gameObject.layer == 15 || other.gameObject.layer == 16))
        {
            if (other.gameObject.layer == 14 && hp < maxHp){
                hp += 10;
                Destroy(other.gameObject);
            } else if (other.gameObject.layer == 13 && hp < maxHp){
                hp += 25;
                Destroy(other.gameObject);
            } else if (other.gameObject.layer == 15 && maxHp < 250){
                maxHp += 20;
                Destroy(other.gameObject);
            } else if (other.gameObject.layer == 16 && shootingSpeed > 0.1f){
                shootingSpeed -= 0.05f;
                Destroy(other.gameObject);
            }

            if (hp > maxHp){
                hp = maxHp;
            }

            if (maxHp > 250){
                maxHp = 250;
            }

            if (shootingSpeed < 0.1f){
                shootingSpeed = 0.1f;
            }

            print("MAXHP: " + maxHp.ToString() + "SHOOTINGSPEED: " + shootingSpeed.ToString());

            

        }

        
    }

    void OnDrawGizmos()
    {
        // GIZMOS
        // elementos graficos visibles solo en editor
        // su fin es informar a los devs

        // para dibujar primero especificamos color
        Gizmos.color = Color.blue;

        // y luego dibujamos

        //Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 20);
    }


    IEnumerator disparo()
    {
        while (true)
        {
            
            Instantiate(original,
                new Vector3(referenciaDePosicion.transform.position.x,
                referenciaDePosicion.transform.position.y + 0.675f,
                referenciaDePosicion.transform.position.z), gameObject.transform.rotation);
           
            yield return new WaitForSeconds(shootingSpeed);
        }
    }

    IEnumerator interactBrick()
    {

        while (true)
        {
            

            yield return new WaitForSeconds(0.5f);
            
            
        }

        
    }
}
