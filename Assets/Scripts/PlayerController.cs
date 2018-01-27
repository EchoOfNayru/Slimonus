using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        if (Input.GetAxis("Horizontal") == 1)
        {
            Debug.Log("this way");
        }
	}
}
