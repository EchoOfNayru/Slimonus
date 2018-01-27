using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool onPlatform;
    public float xSpeed;

	// Use this for initialization
	void Start () {
        onPlatform = true;
	}

    // Update is called once per frame
    void Update()
    {

    }

	void FixedUpdate () {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        if (Input.GetAxis("Horizontal") == 1 && !onPlatform)
        {
            rb.AddForce(transform.right);
        }
        else if (Input.GetAxis("Horizontal") == -1 && !onPlatform)
        {
            rb.AddForce(transform.right *= -1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        onPlatform = true;
    }

    void OnCollisionExit(Collision coliision)
    {
        onPlatform = false;
        StartCoroutine(Slam());
    }

    private IEnumerator Slam()
    {
        while (!Input.GetKeyDown("space"))
        {
            yield return null;
            if (onPlatform)
            {
                break;
            }
        }
        Rigidbody rb = this.GetComponent<Rigidbody>();
        //slam direction always goes down
        Vector3 slamDir = new Vector3(0,0,0);
        xSpeed = rb.velocity.x;
        if (rb.velocity.y >= 0)
        {
            slamDir = new Vector3(0, rb.velocity.y + rb.velocity.x, 0);
        }
        if (rb.velocity.y < 0)
        {
            slamDir = new Vector3(0, (-1 * rb.velocity.y) + rb.velocity.x, 0);
        }
        rb.velocity = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(-slamDir, ForceMode.Impulse);
    }
}
