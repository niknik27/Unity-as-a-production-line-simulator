using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseSpeedButton : SpeedChange
{
    float bufferRate = 0.0f;
    private float nextClick = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Decreases speed on button click
    void OnMouseOver()
    {
        // FOR MOUSE INPUT ONLY
        if (Input.GetMouseButtonDown(0))
        {
            //from SpeedChange class
            decreaseSpeed();
        }
        else
        {
        }

    }
}
