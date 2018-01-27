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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("done a thing");
        StartCoroutine(WaveDelay(waveDelayTime, other));
    }

    private IEnumerator WaveDelay(float delayTime, Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 VelocityStorage = rb.velocity;
        VelocityStorage.y *= -0.83f;
        rb.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(delayTime);
        rb.AddForce(VelocityStorage, ForceMode.Impulse);
        Debug.Log(VelocityStorage);
    }
}
