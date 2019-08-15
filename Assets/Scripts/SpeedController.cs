using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the UI text that shows the current speed 
/// </summary>
public class SpeedController : MonoBehaviour
{
    public UnityEngine.UI.Text speedInfo;

    private float speed;
    private string speedText;

    private float laserCutTime;
    private string laserCutTimeText;

    // Start is called before the first frame update
    void Start()
    {
        speedInfo = GetComponent<UnityEngine.UI.Text>();

    }

    // Update is called once per frame
    void Update()
    {
        ///Sets the information that shows in the text object
        speedText = speed.ToString("n2");
        laserCutTimeText = laserCutTime.ToString("n2");

        speedInfo.text = "Current Speed: " + speedText + " u/s"
                        + "\nCut Time: " + laserCutTimeText + "s";
                         
    }

    ///SETTERS/////
    public void setSpeedText(float speed)
    {
        this.speed = speed;
    }

    public void setCutTimeText(float laserCutTime)
    {
        this.laserCutTime = laserCutTime;
    }
}
