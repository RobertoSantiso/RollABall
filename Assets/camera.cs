using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    private Vector3 offset;
    public GameObject player;

	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	void Update () {	
	}

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z);
    }
}
