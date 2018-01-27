using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVelocity : MonoBehaviour
{

    public float waveDelayTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        WaveDelay(waveDelayTime, collision.collider);
    }

    private IEnumerator WaveDelay(float delayTime, Collider other)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 VelocityStorage = rb.velocity;
        yield return new WaitForSeconds(delayTime);
        rb.AddForce(VelocityStorage);
    }
}
