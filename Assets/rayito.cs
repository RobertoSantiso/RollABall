using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rayito : MonoBehaviour {

    public GameObject pokemon;
    public  GameObject pokeball;
    public GameObject padre;
    Vector3 giro;
    Vector3 giroY;
    float anguloX;//no se usa
    float anguloZ;
    float anguloY;
    float distanciaXZ;
    float distanciaX;
    float distanciaZ;
    float distanciaY;
    public int capturando=0;

	// Use this for initialization
	void Start () {
        giro = new Vector3 (0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
        //calcula la distancia entre la pokeball y el pokemon
        distanciaZ = -(pokeball.transform.position.z - pokemon.transform.position.z);
        distanciaX = (pokeball.transform.position.x - pokemon.transform.position.x);
        distanciaY = (pokeball.transform.position.y - pokemon.transform.position.y);
        distanciaXZ = Mathf.Sqrt(Mathf.Pow(distanciaX, 2) + Mathf.Pow(distanciaZ, 2)); //hipotenusa entre X y Z
        
        if (distanciaX>0) {//calcula el ángulo que debe tener el rayito con respecto ambos objetos 
            anguloZ = -Mathf.Acos(distanciaY / Mathf.Sqrt(Mathf.Pow(distanciaXZ, 2) + Mathf.Pow(distanciaY, 2))) * 180 / (float)Math.PI;
            anguloY = -Mathf.Asin(distanciaZ / Mathf.Sqrt(Mathf.Pow(distanciaX, 2) + Mathf.Pow(distanciaZ, 2))) * 180 / (float)Math.PI;
        }
        else
        {
            anguloZ = Mathf.Acos(distanciaY / Mathf.Sqrt(Mathf.Pow(distanciaXZ, 2) + Mathf.Pow(distanciaY, 2))) * 180 / (float)Math.PI;
            anguloY = Mathf.Asin(distanciaZ / Mathf.Sqrt(Mathf.Pow(distanciaX, 2) + Mathf.Pow(distanciaZ, 2))) * 180 / (float)Math.PI;
        }
        
        giro = new Vector3 (transform.rotation.x,-anguloY,anguloZ); //establece el angulo Z
        giroY = new Vector3(0,anguloY,0); //establece angulo Y
        if (capturando == 1) {//se escala para ser visible
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Sqrt(Mathf.Pow(distanciaXZ, 2) + Mathf.Pow(distanciaY, 2)) / 2, transform.localScale.z);
        }
        //se posiciona entra ambos objetos mediante el objeto padre
        padre.transform.position = (pokemon.transform.position + pokeball.transform.position)/2;
	}

    private void LateUpdate()
    { //ajusta la rotación del rayito mediante el eje Z y en el padre el eje Y
        padre.transform.localEulerAngles=giroY;
        transform.eulerAngles = giro;
    }
}
