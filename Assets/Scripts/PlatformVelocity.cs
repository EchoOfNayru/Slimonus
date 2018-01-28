using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVelocity : MonoBehaviour
{

    public float waveDelayTime;

    private Vector3 velocityStorage;
    //private

    // Use this for initialization
    void Start()
    {
        velocityStorage = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        collision.collider.GetComponent<Rigidbody>();
    }
}
