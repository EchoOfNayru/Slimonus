using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;

    public GameObject DestroyThis;

    public float slamSpeed = 50;
    public float jumpSpeed = 25;
    public float maxSpeed = 15;
    public bool onLauncher;

    public bool canJump = false;
    private bool jump = false;
    private bool slam = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        onLauncher = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            rb.AddForce(10, 0, 0);
        }
        if (Input.GetKeyDown("space") && !onLauncher)
        {
            rb.velocity = Vector3.zero;
            //rb.useGravity = false;
            slam = true;

        }
        if(transform.position.y <= 0.2)
        {
            Destroy(DestroyThis);
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown("space") && canJump && !onLauncher)
        {
            slam = false;
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector3.right * horizontal * 30f);
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        if (jump)
            {
                rb.AddForce(new Vector3(10, jumpSpeed,0));
                jump = false;
            }
        
        if(slam)
        {
            
               // rb.useGravity = true;
                rb.AddForce(0, -slamSpeed, 0);
                //slam = false;
            
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Platform" || collision.collider.tag == "Launcher")
        {
      
            onLauncher = false;
            
        }
        canJump = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Platform")
        {
            canJump = true;
        }

    }

    void OntriggerEnter(Collision other)
    {
        if (other.collider.tag == "Despawner")
        {
            Destroy(DestroyThis);
        }
    }
}
