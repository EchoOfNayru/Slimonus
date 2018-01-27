using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
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

    void OnTriggerEnter(Collider collider)
    {
        WaveDelay(waveDelayTime, collider);
    }

    private IEnumerator WaveDelay(float delayTime, Collider collider)
    {

        yield return new WaitForSeconds(delayTime);
    }
}
