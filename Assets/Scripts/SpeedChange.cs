using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Parent class of the increase and decrease speed classes
/// </summary>
public class SpeedChange : MonoBehaviour
{
    //sets how much the speed is changed after each button press
    private static float increment = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// These methods increase and decrease the speed of the production line
    /// To INCREASE the speed "Time" is decreased because it is the time it takes to get to the end position
    /// To DECREASE the speed "Time" is increased
    /// </summary>
    public void increaseSpeed()
    {
        //checks if the time is still greater than 1 
        //if it is at 1 already, speed cannot be increased anymore
        if (Product.getTime() > 1)
        {
            Product.setTime(Product.getTime() - increment);
            print("New default Time: " + Product.getTime());

        }
        else
        {
            print("New default Time: " + Product.getTime());
        }

    }

    public void decreaseSpeed()
    {
        Product.setTime(Product.getTime() + increment);
        print("New default Time: " + Product.getTime());

    }

}
