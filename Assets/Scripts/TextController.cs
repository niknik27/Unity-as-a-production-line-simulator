using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the controller for the Text UI that shows the info of the current doors and windows, as well as the averages
/// </summary>
public class TextController : MonoBehaviour
{
    public UnityEngine.UI.Text information;
    private float doorHeight;
    private float doorWidth;
    private float windowHeight;
    private float windowWidth;
    private string windowTypeSelection;
    private float averageDoorHeight;
    private float averageDoorWidth;
    private float averageWindowHeight;
    private float averageWindowWidth;


    // Start is called before the first frame update
    void Start()
    {
        information = GetComponent<UnityEngine.UI.Text>();

    }

    // Update is called once per frame
    void Update()
    {
        ///Sets the information that shows in the text object
        information.text = "Door Height: " + doorHeight + "\nWindow Height: " + windowHeight
                         + "\nDoor Width: " + doorWidth + "\nWindow Width: " + windowWidth
                         + "\nWindow Type: " + windowTypeSelection
                         + "\nCurrent Average Door Height: " + averageDoorHeight
                         + "\nCurrent Average Door Width: " + averageDoorWidth
                         + "\nCurrent Average Window Height: " + averageWindowHeight
                         + "\nCurrent Average Window Width: " + averageWindowWidth;
    }

    //////////////SETTERS(Mutators)////////////////////
    public void setDoorHeightText(float doorHeight)
    {
        this.doorHeight = doorHeight;
    }

    public void setDoorWidthText(float doorWidth)
    {
        this.doorWidth = doorWidth;
    }

    public void setWindowHeightText(float windowHeight)
    {
        this.windowHeight = windowHeight;
    }

    public void setWindowWidthText(float windowWidth)
    {
        this.windowWidth = windowWidth;
    }

    public void setSelectedWindowType(string windowTypeSelection)
    {
        this.windowTypeSelection = windowTypeSelection;
    }

    public void setAverageDoorHeightText(float averageDoorHeight)
    {
        this.averageDoorHeight = averageDoorHeight;
    }

    public void setAverageDoorWidthText(float averageDoorWidth)
    {
        this.averageDoorWidth = averageDoorWidth;
    }

    public void setAverageWindowHeightText(float averageWindowHeight)
    {
        this.averageWindowHeight = averageWindowHeight;
    }

    public void setAverageWindowWidthText(float averageWindowWidth)
    {
        this.averageWindowWidth = averageWindowWidth;
    }
}
