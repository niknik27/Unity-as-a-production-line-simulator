using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private static float cutTime = 0f;
    private static float cutTimePercent = 0.95f;
    private bool laserOn = false;

    private float productMoveTime;

    private int productIdNum = 0;

    /// <summary>
    /// NOTE: start, update, and onTriggerEnter do NOT get inherited to child classes
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {

        setLaserOn(laserOn);
        getProductMoveTime();

    }

    // Update is called once per frame
    void Update()
    {
    }


    //Calculates the time it takes for the laser to cut by:
    //  *getting the time it takes for the product to get to the laser
    //  *multiplying that by a certain percent
    public static void calculateCutTime()
    {
        cutTime = Product.getTime() * cutTimePercent;
    }

    //Methods get and set the boolean if the laser is on or off
    public void setLaserOn(bool laserOn)
    {
        this.laserOn = laserOn;
    }

    ///SETTERS/////////
    public static void setCutTime(float newCutTime)
    {
        cutTime = newCutTime;
    }

    public static void setCutTimePercent(float newCutTimePercent)
    {
        cutTimePercent = newCutTimePercent;
    }

    ///GETTERS/////
    public bool getlaserOn()
    {
        return laserOn;
    }

    public float getProductMoveTime()
    {
        //Get the time it takes for the products to move towards the laser
        productMoveTime = Product.getTime();

        return productMoveTime;
    }

    public static float getCutTime()
    {
        return cutTime;
    }

    public static float getCutTimePercent()
    {
        return cutTimePercent;
    }
}
