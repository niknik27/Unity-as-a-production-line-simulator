using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the window objects
/// </summary>
public class Window : Product
{
    private float windowHeight;
    private float windowWidth;

    private float speed;

    GameObject windowBelt;
    GameObject windowOutput;


    /// Start is called before the first frame update
    void Start()
    {
        windowBelt = GameObject.Find("WindowBelt");
        windowOutput = GameObject.Find("WindowOutput");


        //inherited from Product class
        calculateSpeed();
        
    }

    /// Awake is called when the gameObject is created
    void Awake()
    {
        setWindowHeight(windowHeight);
        setWindowWidth(windowWidth);
    }

    ////////// Update is called once per frame
    void Update()
    {
        checkIfWindowAtStart();
    }

    //checks if the window has just recently been instantiated (which means the window is at the start of the conveyor belt)
    public void checkIfWindowAtStart()
    {
        if (getAtStart() == false)
        {
            //Method inherited from Product class
            moveToOutput(windowOutput.transform.position);
            //print("moving window to output because atStart is: " + getAtStart());
        }
        else
        {
            //Method inherited from Product class
            moveToLaser(windowBelt.transform.position);
            //print("moving window to Laser because atStart is: " + getAtStart());

        }
    }

    ///////SETTERS(Mutators)///////////////////////////
    public void setWindowHeight(float windowHeight)
    {
        this.windowHeight = windowHeight;
    }

    public void setWindowWidth(float windowWidth)
    {
        this.windowWidth = windowWidth;
    }

    /////////GETTERS(Accessors)/////////////////////
    public float getWindowHeight()
    {
        return windowHeight;
    }
    public float getWindowWidth()
    {
        return windowWidth;
    }
}
