using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [Range(0, 1)]
    [SerializeField] float oscillationAmplitude=1f;
    [SerializeField] float oscillationPeriodInSeconds=5f;
    [SerializeField] Vector3 oscillationDirection= Vector3.one*10;


    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentOscilaltionAmplitude =oscillationAmplitude* Mathf.Sin((2 * Mathf.PI / oscillationPeriodInSeconds) * Time.time);  // e(t) = A.sin(2.pi.w.t) =  A.sin(2.pi.1/T.t) 

        Vector3 offset = oscillationDirection * currentOscilaltionAmplitude;
        transform.position = initialPosition + offset;
        
    }
}
