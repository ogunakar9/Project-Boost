using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    private float movementFactor;
    [SerializeField] private float period = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Mathf.Epsilon is a really small number and for floats its better than x==0 comparison
        //since floats might not be equal on the most minute decimal
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // continually growing over time
        
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
