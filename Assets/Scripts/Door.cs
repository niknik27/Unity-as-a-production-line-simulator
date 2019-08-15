using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the door objects
/// </summary>
public class Door : Product
{
    private float doorHeight;
    private float doorWidth;
    //private float speed;

    private List<string> windowTypeList = new List<string>();

    private string selectedWindowType;

    GameObject doorBelt;
    GameObject doorOutput;

    // Start is called before the first frame update
    void Start()
    {
        doorBelt = GameObject.Find("DoorBelt");
        doorOutput = GameObject.Find("DoorOutput");

        //inherited from Product class
        calculateSpeed();

    }

    /// Awake is called when the gameObject is created
    void Awake()
    {
        //Populate windowTypeList ArrayList
        windowTypeList.Add("Full");
        windowTypeList.Add("Half");
        windowTypeList.Add("Quarter");

        //////ORDER SIMULATION://////

        //Pick a random option from the windowTypeList
        setSelectedWindowType(windowTypeList[Random.Range(0, windowTypeList.Count)]);

        //Provide Random sizes for doors in a given range
        //In Inches
        setDoorHeight(Random.Range(84f, 120f));
        setDoorWidth(Random.Range(36f, 60f));

        //for checking measurements
        //print(getId() + "\nHeight: " + getDoorHeight() + "\nWidth: " + getDoorWidth() + "\nWindow Type: " + getSelectedWindowType());

    }

    // Update is called once per frame
    void Update()
    {
        checkIfDoorAtStart();
    }

    //checks if the door has just recently been instantiated (which means the door is at the start of the conveyor belt)
    public void checkIfDoorAtStart()
    {
        if (getAtStart() == false)
        {
            //Method inherited from Product class
            moveToOutput(doorOutput.transform.position);
            //print("moving door to output because atStart is: " + getAtStart());
        }
        else
        {
            //Method inherited from Product class
            moveToLaser(doorBelt.transform.position);
            //print("moving door to Laser because atStart is: " + getAtStart());

        }
    }

    //////////////SETTERS(Mutators)////////////////////
    public void setDoorHeight(float doorHeight)
    {
        this.doorHeight = doorHeight;
    }

    public void setDoorWidth(float doorWidth)
    {
        this.doorWidth = doorWidth;
    }

    public void setSelectedWindowType(string selectedWindowType)
    {
        this.selectedWindowType = selectedWindowType;
    }

    //////////////GETTERS(Accessors)///////////////////
    public float getDoorHeight()
    {
        return doorHeight;
    }
    public float getDoorWidth()
    {
        return doorWidth;
    }

    public string getSelectedWindowType()
    {
        return selectedWindowType;
    }
}
