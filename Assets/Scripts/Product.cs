using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the parent class of the door and window classes
/// </summary>
public class Product : MonoBehaviour
{
    private static float defaultTime = 5f;
    private static float defaultSpeed;

    private static float defaultDistance;

    /// <summary>
    /// initialization of the distances to calculate the distance the door has to move
    /// end result will be the same value for the window
    /// </summary>
    //starting position of the door
    private static Vector3 startingDistance = new Vector3(-14.6f, -17.8f);
    //position of the laser
    private static Vector3 endingDistance = new Vector3(-14.6f, 0f);
    

    bool atStart = true;
    private string id;
    private int idNum;

    // Start is called before the first frame update
    void Start()
    {

        setAtStart(atStart);
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Methods get and set the boolean if the product is at the start of the product line (meaning NOT at the laser)
    public void setAtStart(bool atStart)
    {
        this.atStart = atStart;
    }

    public bool getAtStart()
    {
        return atStart;
    }

    ///Gets the speed of the movement from start to laser according to a specific amount of time
    ///this is only really called at the beginning when initialized
    public void calculateSpeed()
    {
        defaultDistance = Vector3.Distance(startingDistance, endingDistance);

        defaultSpeed = defaultDistance / defaultTime;
        //return defaultSpeed;
    }

    ///Move the gameobject towards the center of the belt where the laser is, at a certain speed
    public void moveToLaser(Vector3 vectorTowards)
    {
        transform.position = Vector3.MoveTowards(transform.position, vectorTowards, Time.deltaTime * defaultSpeed);
    }

    //After the product has gotten to the laser, this method is called to move to the output 
    public void moveToOutput(Vector3 vectorTowards)
    {
        transform.position = Vector3.MoveTowards(transform.position, vectorTowards, Time.deltaTime * defaultSpeed);
    }


    ///////SETTERS(Mutators)///////////////////////////
    public static void setTime(float newDefaultTime)
    {
        defaultTime = newDefaultTime;
    }

    public void setId(string id)
    {
        this.id = id;
    }

    public void setIdNum(int idNum)
    {
        this.idNum = idNum;
    }

    public static void setSpeed(float newSpeed)
    {
        defaultSpeed = newSpeed;
    }

    public static void setDistance(float newDistance)
    {
        defaultDistance = newDistance;
    }

    /////////GETTERS(Accessors)/////////////////////
    public static float getTime()
    {
        return defaultTime;
    }

    public string getId()
    {
        return id;
    }

    public int getIdNum()
    {
        return idNum;
    }

    public static float getSpeed()
    {
        return defaultSpeed;
    }

    public static float getDistance()
    {
        return defaultDistance;
    }

}
