using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the door laser object
/// </summary>
public class DoorLaser : Laser
{

    private bool doorLaserOn;

    private float doorMoveTime;
    private float laserTimer = 0;
    private float counter = 0;
    private float doorCutTime;

    Door doorObject;

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

        doorLaserOn = getlaserOn();
        setLaserOn(doorLaserOn);

    }

    // Update is called once per frame
    void Update()
    {
        doorMoveTime = getProductMoveTime();
        doorCutTime = getCutTime();

        //checks first if all doors and windows are listed into the multiThreadCalculations class before taking a door and window from the pool
        //this guarantees that the class will not be missing the first door and window in their list
        if (calculationThreadScript.getInitializingDone() == true)
        {
            if (startDone == false)
            {
                objectPoolScript.GetObjectForType("Door", true);
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
        if (col.gameObject.tag == "Door")
        {
            doorObject = col.gameObject.GetComponent<Door>();

            //calls this method to chage the UI text to the information of the detected door object
            setUIText();

            //print("DOOR IN FRONT OF LASER");
        }
        else
        {

        }
    }

    //sets text of the UI info 
    public void setUIText()
    {
        textUIController.setDoorHeightText(doorObject.getDoorHeight());
        textUIController.setDoorWidthText(doorObject.getDoorWidth());
        textUIController.setSelectedWindowType(doorObject.getSelectedWindowType());
    }

    //lasertimer counts up to the move time of the door
    //when that timer finishes it turns the laser on and it starts cutting
    //when cutting time is done, both timers are restarted to 0 to continue the cycle
    public void startLaser()
    {

        if (laserTimer < doorMoveTime)
        {
            laserTimer += Time.deltaTime;
            //print("door laser waiting: " + laserTimer);
        }
        else
        {
            if (counter <= doorCutTime)
            {
                counter += Time.deltaTime;
                setLaserOn(true);
                //print("Cutting out door: " + counter);

            }
            else
            {
                setLaserOn(false);

                //sets it to false so that the door can start moving to output
                doorObject.setAtStart(false);
                print("is Door at the start? " + doorObject.getAtStart());

                //gets another door from beginning of the pool
                objectPoolScript.GetObjectForType("Door", true);
                laserTimer = 0;
                counter = 0;
            }
        }
    }
}
