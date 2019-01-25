using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public Rigidbody rb;
    public float fuerza;
    public float salto;
    public Text texto;
    int numSaltos;
    Vector3 direccionX;
    Vector3 direccionZ;
    public GameObject rayito;
    int captura = 0;

    void Start () {
        rb = this.GetComponent<Rigidbody>();
        direccionX = new Vector3(1, 0, 0);
        direccionZ = new Vector3(0, 0, 1);
    }

    void moverse()
    {//controlles para moverse y saltar.
        if (Input.GetKey("w"))
        {
            rb.AddForce(direccionZ*fuerza);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-direccionX* fuerza);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(-direccionZ * fuerza);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(direccionX * fuerza);
        }
        if (Input.GetKeyDown("space")&& numSaltos==0)
        {
            rb.velocity = new Vector3(rb.velocity.x,salto, rb.velocity.z);
            numSaltos++;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("suelo"))
        {//permite saltar al tocar 'suelo'
            numSaltos = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {//se inicia la captura de pikachu
        rb.velocity = new Vector3(-rb.velocity.x/2, 4.5f, -rb.velocity.z/2); //la pokeball salta en dirección contraria
        captura = 1;
        rayito.transform.localScale = new Vector3(0.09f, 0, 0.09f); //se muestra el rayito de captura
        rayito.GetComponent<rayito>().capturando = 1; //llama al script de rayito
        rb.angularVelocity = new Vector3(0,0,0); //pierde velocidad angular (al girar)
        StartCoroutine(capturando());  // función para esperar unos segundos
    }

    IEnumerator capturando()
    {
        print("waiting");
        yield return new WaitForSeconds(2);  //despues de esperar termina la captura
        captura = 2;
        rayito.transform.localScale = new Vector3(0,0,0); 
        texto.text = "¡Atrapado!";
    }

    void Update () {    
    }

    private void FixedUpdate()
    {
        if (captura==0) {
            moverse();
        }
    }
    private void LateUpdate()
    {
         if (captura==1)
        { //mientras se está capturando la pokeball sufre fuerzas iguales y opuestas parandose (9.8=gravedad)
            rb.AddForce(-rb.velocity.x, -rb.velocity.y+9.8f, -rb.velocity.z);

        }
    }
}
