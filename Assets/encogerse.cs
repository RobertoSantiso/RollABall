using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class encogerse : MonoBehaviour {


    int capturando = 0;
	void Start () {

	}

    private void OnTriggerEnter(Collider other)
    {//si entra en colision (no física) con un objeto pokeball, empieza la captura
        print("colisiono");
        if (other.gameObject.tag.Equals("pokeball"))
        {
            capturando = 1;            
        }        
    }
    // Update is called once per frame
    void Update () {
        //se encogerá hasta que no se vea.
        if (capturando==1)
        {
            transform.localScale = new Vector3(transform.localScale.x-0.0038f, transform.localScale.y - 0.0038f, transform.localScale.z - 0.0038f);
        }
        if(transform.localScale.x<0) {
            gameObject.SetActive(false);//desactiva el objeto
        }
	}
}
