using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// this controls all calculations that are in different threads that run in the background of the simulator
/// </summary>
public class MultiThreadCalculations : MonoBehaviour
{
    GameObject objectPoolContainer;
    ObjectPool objectPool;

    private List<Door> allDoors;
    private List<Window> allWindows;

    private List<Door> finishedDoors;
    private List<Window> finishedWindows;

    private float doorSumHeight;
    private float doorSumWidth;

    private float windowSumHeight;
    private float windowSumWidth;

    private float doorAverageHeight;
    private float doorAverageWidth;

    private float windowAverageHeight;
    private float windowAverageWidth;

    private float windowHeight;
    private float windowWidth;

    private float heightDifference = 4f; //4 inches (gap between the door and the window from the top)
    private float widthDifference = 3f; //3 inches (gap beween door and winow for sides)

    bool computationWindowDone = false;
    Thread computationWindowThread;

    bool doorAverageComputationDone = false;
    Thread doorAverageComputationThread;

    bool windowAverageComputationDone = false;
    Thread windowAverageComputationThread;

    bool calcBeltSpeedDone = false;
    Thread calcBeltSpeedThread;

    private bool initializingDone = false;

    GameObject textUI;
    TextController textUIController;

    GameObject speedTextUI;
    SpeedController speedTextUIController;

    private float newSpeed;
    private float time;
    private float distance;
    private float newCutTime;

    private bool allDone = false;
    private int finishedCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        objectPoolContainer = GameObject.Find("ObjectPool");
        objectPool = objectPoolContainer.GetComponent<ObjectPool>();

        textUI = GameObject.Find("TextInformation");
        textUIController = textUI.GetComponent<TextController>();

        speedTextUI = GameObject.Find("SpeedText");
        speedTextUIController = speedTextUI.GetComponent<SpeedController>();

        finishedDoors = new List<Door>();
        finishedWindows = new List<Window>();

        allDoors = new List<Door>();
        allWindows = new List<Window>();

    }

    // Update is called once per frame
    void Update()
    {
        //This guarantees that the pooledObjects on the ObjectPool class is not null when this is called
        if (objectPool.getLengthOfPooledObjects() > 0)
        {
            if (initializingDone == false)
            {
                allDoors = objectPool.getAllDoors();
                allWindows = objectPool.getAllWindows();
                initializingDone = true;
            }
            else
            {

            }
        }
        else
        {

        }
        
        //Runs the computations for the window measurements
        //When all computations are finished it stops the thread
        if (computationWindowDone == false)
        {
            computationWindowThread = new Thread(new ThreadStart(calculateWindowMeasurements));
            computationWindowThread.Start();
        }
        else
        {

        }

        //Computes the averages of the door measurements on a separate thread
        //One the thread is done computing it starts computing again
        if (doorAverageComputationDone == false)
        {
            doorAverageComputationThread = new Thread(new ThreadStart(calcAverageDoorMeasurements));
            doorAverageComputationThread.Start();
            print("Door Average Height: " + windowAverageHeight);

        }
        else
        {
            doorAverageComputationDone = false;
        }

        //Computes the averages of the window measurements on a separate thread
        //One the thread is done computing it starts computing again
        if (windowAverageComputationDone == false)
        {
            windowAverageComputationThread = new Thread(new ThreadStart(calcAverageWindowMeasurements));
            windowAverageComputationThread.Start();

            print("Window Average Height: " + windowAverageHeight);
        }
        else
        {
            windowAverageComputationDone = false;
        }

        //Computes the speed of the belt
        if (calcBeltSpeedDone == false)
        {
            calcBeltSpeedThread = new Thread(new ThreadStart(calculateProductionSpeeds));
            calcBeltSpeedThread.Start();
        }
        else
        {
            calcBeltSpeedDone = false;
        }

        //checks if the finishedDoors list is equal to the amount buffered
        //if equal it means all doors (and windows) have finished
        if (finishedDoors.Count < objectPool.getBufferAmount())
        {
            allDone = false;
        }
        else
        {
            allDone = true;
        }

        setUIText();
        //checkLists();

    }

    //for testing purposes
    //checks if all door and windows were stored properly
    public void checkLists()
    {
        foreach (Door door in allDoors)
        {
            print(door.getId());
        }

        foreach (Window window in allWindows)
        {
            print(window.getId());

        }

    }

    //sets the on screen information
    public void setUIText()
    {
        textUIController.setAverageDoorHeightText(doorAverageHeight);
        textUIController.setAverageDoorWidthText(doorAverageWidth);
        textUIController.setAverageWindowHeightText(windowAverageHeight);
        textUIController.setAverageWindowWidthText(windowAverageWidth);
        speedTextUIController.setSpeedText(newSpeed);
        speedTextUIController.setCutTimeText(newCutTime);
    }

    /// <summary>
    /// Gets all doors and matches each door with their corresponding window using their IdNumber
    /// The measurements are then calculated by:
    ///     *Getting the height and width of the door
    ///     *Subtracting the difference to create a gap
    ///     *Do calcuations depending on the selected window type
    /// </summary>
    public void calculateWindowMeasurements()
    {

        foreach (Door door in allDoors)
        {

            foreach (Window window in allWindows)
            {

                if (door.getIdNum() == window.getIdNum())
                {
                    if (door.getSelectedWindowType().Equals("Full"))
                    {
                        //calculates height of a full window by subtracting the height of door from the gaps 
                        //multiplied by 2 to account for both top and bottom gaps
                        windowHeight = door.getDoorHeight() - (heightDifference * 2);

                        //calculates width of a full window by subtracting the width of door from the gaps 
                        //multiplied by 2 to account for gaps on both sides
                        windowWidth = door.getDoorWidth() - (widthDifference * 2);

                        //set the window measurements
                        window.setWindowHeight(windowHeight);
                        window.setWindowWidth(windowWidth);

                    }
                    else if (door.getSelectedWindowType().Equals("Half"))
                    {
                        //calculates height of a half window by dividing the height of door by 2 and subtracting the gap
                        windowHeight = (door.getDoorHeight() / 2) - heightDifference;

                        //calculates width of a full window by subtracting the width of door from the gaps 
                        //multiplied by 2 to account for gaps on both sides
                        windowWidth = door.getDoorWidth() - (widthDifference * 2);

                        //set the window measurements
                        window.setWindowHeight(windowHeight);
                        window.setWindowWidth(windowWidth);

                    }
                    else if (door.getSelectedWindowType().Equals("Quarter"))
                    {
                        //calculates height of a quarter window by dividing the height of door by 4 and subtracting the gap
                        windowHeight = (door.getDoorHeight() / 4) - heightDifference;

                        //calculates width of a full window by subtracting the width of door from the gaps 
                        //multiplied by 2 to account for gaps on both sides
                        windowWidth = door.getDoorWidth() - (widthDifference * 2);

                        //set the window measurements
                        window.setWindowHeight(windowHeight);
                        window.setWindowWidth(windowWidth);
                    }
                    else
                    {

                    }

                    //print(window.getId() + "\nHeight:" + window.getWindowHeight() + "\nWidth:" + window.getWindowWidth() + "\nWindow Type:" + door.getSelectedWindowType());
                }
                else
                {

                }
            }
        }

        computationWindowDone = true;
    }


    //calculates the average height and width of the doors
    //this is run constantly and recalculated whenever a new door is finished and stored in "finishedDoors"
    public void calcAverageDoorMeasurements()
    {
        doorSumHeight = 0;
        doorSumWidth = 0;

        if (objectPool.getAllFinishedDoors().Count > 0)
        {
            finishedDoors = objectPool.getAllFinishedDoors();

            foreach (Door door in finishedDoors)
            {
                doorSumHeight += door.getDoorHeight();
                doorSumWidth += door.getDoorWidth();
            }

            doorAverageHeight = doorSumHeight / finishedDoors.Count;
            doorAverageWidth = doorSumWidth / finishedDoors.Count;

            doorAverageComputationDone = true;
            
        }
        else
        {

        }
    }

    //calculates the average height and width of the windows
    //this is run constantly and recalculated whenever a new window is finished and stored in "finishedWindows"
    public void calcAverageWindowMeasurements()
    {
        windowSumHeight = 0;
        windowSumWidth = 0;

        if (objectPool.getAllFinishedWindows().Count > 0)
        {
            finishedWindows = objectPool.getAllFinishedWindows();

            foreach (Window window in finishedWindows)
            {
                windowSumHeight += window.getWindowHeight();
                windowSumWidth += window.getWindowWidth();
            }

            windowAverageHeight = windowSumHeight / finishedWindows.Count;
            windowAverageWidth = windowSumWidth / finishedWindows.Count;

            windowAverageComputationDone = true;
        }
        else
        {

        }
    }

    //constantly calculates the speed of the door and window
    //this is so that when the speed change buttons are pressed the change is instantaneous 
    public void calculateProductionSpeeds()
    {
        time = Product.getTime();
        distance = Product.getDistance();

        newSpeed = distance / time;
        newCutTime = time * Laser.getCutTimePercent();

        //print("new speed: " + newSpeed);
        //print("new laser cut time: " + newCutTime);

        Product.setSpeed(newSpeed);
        Laser.setCutTime(newCutTime);

    }

    /////////GETTERS(Accessors)/////////////////////
    public float getDoorAverageHeight()
    {
        return doorAverageHeight;
    }

    public float getDoorAverageWidth()
    {
        return doorAverageWidth;
    }

    public float getWindowAverageHeight()
    {
        return windowAverageHeight;
    }

    public float getWindowAverageWidth()
    {
        return windowAverageWidth;
    }

    public bool getDoorAveCompDone()
    {
        return doorAverageComputationDone;
    }

    public bool getWindowAveCompDone()
    {
        return windowAverageComputationDone;
    }

    public bool getAllDone()
    {
        return allDone;
    }

    public bool getInitializingDone()
    {
        return initializingDone;
    }

}
