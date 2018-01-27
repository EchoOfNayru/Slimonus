using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public float launchForce;
    //public float launchDelay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(LaunchDelay(collision.collider));
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        //rb.useGravity = false;
    }

    void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        //rb.useGravity = true;
    }

    private IEnumerator LaunchDelay (Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 LaunchVector = new Vector3(transform.right.x, transform.up.y, 0);
        rb.velocity = new Vector3(0, 0, 0);
        while (!Input.GetKeyDown("space"))
        {
            Debug.Log("in Loop");
            yield return null;
        }
        rb.AddForce(LaunchVector * launchForce, ForceMode.Impulse);
    }
}
