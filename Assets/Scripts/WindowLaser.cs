using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the door laser object
/// </summary>
public class WindowLaser : Laser
{

    private bool windowLaserOn;

    private float windowMoveTime;
    private float laserTimer = 0;
    private float counter = 0;
    private float windowCutTime;

    Window windowObject;

    GameObject objectPool;
    ObjectPool objectPoolScript;

    GameObject calculationThread;
    MultiThreadCalculations calculationThreadScript;

    GameObject textUI;
    TextController textUIController;

    private bool startDone = false;

    // Start is called before the first frame update
    void Start()
    {
        //Finds each gameobject and then gets its script to be able to use the script's methods
        objectPool = GameObject.Find("ObjectPool");
        objectPoolScript = objectPool.GetComponent<ObjectPool>();

        calculationThread = GameObject.Find("Calculations");
        calculationThreadScript = calculationThread.GetComponent<MultiThreadCalculations>();

        textUI = GameObject.Find("TextInformation");
        textUIController = textUI.GetComponent<TextController>();

        //Calculates the time it takes for laser to cut on start
        calculateCutTime();

        windowLaserOn = getlaserOn();
        setLaserOn(windowLaserOn);


    }

    // Update is called once per frame
    void Update()
    {
        windowMoveTime = getProductMoveTime();
        windowCutTime = getCutTime();

        //checks first if all doors and windows are listed into the multiThreadCalculations class before taking a door and window from the pool
        //this guarantees that the class will not be missing the first door and window in their list
        if (calculationThreadScript.getInitializingDone() == true)
        {
            if (startDone == false)
            {
                objectPoolScript.GetObjectForType("Window", true);
                startDone = true;
            }
            else
            {

            }
        }
        else
        {

        }

        startLaser();
    }

    //This method is connected to the trigger bounds made in unity editor
    //it detects the gameobject that enters the bounds and checks the corresponding tag
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Window")
        {
            windowObject = col.gameObject.GetComponent<Window>();

            //calls this method to chage the UI text to the information of the detected door object
            setUIText();

            //print("WINDOW IN FRONT OF LASER");
        }
        else
        {

        }
    }

    //sets text of the UI info 
    public void setUIText()
    {
        textUIController.setWindowHeightText(windowObject.getWindowHeight());
        textUIController.setWindowWidthText(windowObject.getWindowWidth());
    }

    //lasertimer counts up to the move time of the window
    //when that timer finishes it turns the laser on and it starts cutting
    //when cutting time is done, both timers are restarted to 0 to continue the cycle
    public void startLaser()
    {

        if (laserTimer < windowMoveTime)
        {
            laserTimer += Time.deltaTime;
            //print("window laser waiting: " + laserTimer);
        }
        else
        {
            if (counter <= windowCutTime)
            {
                counter += Time.deltaTime;
                setLaserOn(true);
                //print("Cutting out window: " + counter);

            }
            else
            {
                setLaserOn(false);
                windowObject.setAtStart(false);
                print("is Window at the start? " + windowObject.getAtStart());
                objectPoolScript.GetObjectForType("Window", true);
                laserTimer = 0;
                counter = 0;
            }
        }
    }
}
