using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedButton : SpeedChange
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Increases speed on button click
    void OnMouseOver()
    {
        // FOR MOUSE INPUT ONLY
        if (Input.GetMouseButtonDown(0))
        {
            //from SpeedChange class
            increaseSpeed();
        }
        else
        {
        }

    }
}
