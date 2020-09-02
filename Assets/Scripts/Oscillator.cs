using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [Range(0, 1)]
    [SerializeField] float oscillationAmplitude=1f;
    [SerializeField] float oscillationPeriodInSeconds=5f;
    [SerializeField] Vector3 oscillationDirection= Vector3.one*10;
    [SerializeField] bool isOscillationOneSided=false;


    Vector3 initialPosition;
    float currentOscillationAmplitude;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Mathf.Approximately(oscillationPeriodInSeconds,0)) { return; }

        currentOscillationAmplitude =oscillationAmplitude* Mathf.Sin((2 * Mathf.PI / oscillationPeriodInSeconds) * Time.time);  // e(t) = A.sin(2.pi.w.t) =  A.sin(2.pi.1/T.t)  
        if (isOscillationOneSided && currentOscillationAmplitude<0)
        {
            currentOscillationAmplitude = Mathf.Abs( currentOscillationAmplitude);
        }
        Vector3 offset = oscillationDirection * currentOscillationAmplitude;
        transform.position = initialPosition + offset;
        
    }
}
