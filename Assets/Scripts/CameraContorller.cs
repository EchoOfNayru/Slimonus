using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorller : MonoBehaviour {

    public float offset;
    public GameObject target;
    
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.transform.position.x + offset, 5 , -10); 
        //if(target.transform.position.y <= 5)
        //{
        //    transform.position = new Vector3(target.transform.position.x + offset, 5, -10);
        //}
	}
}
