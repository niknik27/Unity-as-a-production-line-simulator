using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generates the report when all the doors and windows are finished
/// </summary>
public class ReportGenerator : MonoBehaviour
{
    //gets the objectPool object from unity editor and its script
    GameObject objectPoolContainer;
    ObjectPool objectPool;

    //gets the calculation object from unity editer and its script
    GameObject threadCalculationsContainer;
    MultiThreadCalculations threadCalculations;

    //this is the variable to store the number of items the objectPool buffer has initialized
    private float numberOfItems;

    private List<Door> finishedDoors;
    private List<Window> finishedWindows;

    
    private string reportText;
    private bool reportDone = false;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find looks for the gameobject with the corresponding name in the unity editor 
        objectPoolContainer = GameObject.Find("ObjectPool");
        objectPool = objectPoolContainer.GetComponent<ObjectPool>();

        threadCalculationsContainer = GameObject.Find("Calculations");
        threadCalculations = threadCalculationsContainer.GetComponent<MultiThreadCalculations>();

        //gets the amount buffer from the objectPool class
        numberOfItems = objectPool.getBufferAmount();

    }

    // Update is called once per frame
    void Update()
    {
        //checks if the report generation is done
        //once report is done, "reportDone" is made true and method does not run again
        if (reportDone == false)
        {
            generateReport();
        }
        else
        {
            print("REPORT GENERATED");
        }
    }

    /// <summary>
    /// checks if all the doors (and by extension, the windows) are finished by checking the list from objectPool if it now equals the buffer amount
    /// buffer amount was the number of items the objectPool created
    /// it also checks if all the calculations for the average are finished in the "multiThreadCalculations" class to make sure it gets the finished result
    /// 
    /// once all doors and window are finished, the method loops through the lists and stores it in a string variable
    /// string variable is then written into a .txt file
    /// </summary>
    public void generateReport()
    {
        if (objectPool.getAllFinishedDoors().Count >= numberOfItems && threadCalculations.getAllDone() == true)
        {
            finishedDoors = objectPool.getAllFinishedDoors();
            finishedWindows = objectPool.getAllFinishedWindows();

            foreach (Door door in finishedDoors)
            {
                foreach (Window window in finishedWindows)
                {
                    if (door.getIdNum() == window.getIdNum())
                    {
                        print(door.getId() + window.getId());

                        reportText += door.getId() + "\t\t\t\t" + window.getId()
                                      + "\nDoor Height: " + door.getDoorHeight() + "\t\t" + "Window Height: " + window.getWindowHeight()
                                      + "\nDoor Width: " + door.getDoorWidth() + "\t\t" + "Window Width: " + window.getWindowWidth()
                                      + "\nWindow Type: " + door.getSelectedWindowType() + "\n\n";
                    }
                }
            }


            reportText += "Averages: \n\nDoor Height: " + threadCalculations.getDoorAverageHeight()
                            + "\nDoor Width: " + threadCalculations.getDoorAverageWidth()
                            + "\nWindow Height: " + threadCalculations.getWindowAverageHeight()
                            + "\nWindow Width: " + threadCalculations.getWindowAverageWidth();

            System.IO.File.WriteAllText(@"C:\Users\Nicole\Documents\DoorAndWindowProduction\Assets\ProductReport.txt", reportText);
            reportDone = true;
        }
        else
        {
            print("All products not finished " + objectPool.getAllDoors().Count);
        }
    }
}
