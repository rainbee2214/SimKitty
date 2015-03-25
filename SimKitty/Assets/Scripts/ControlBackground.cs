using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Skybox))]
public class ControlBackground : MonoBehaviour
{
    Skybox skybox;
    Material[] skies;
    public int currentSky = 0;
    
    public float speed = 1f;
    public Text text;

    float nextTickTime;
    float delay = 1f;

    bool autoRotate;
    bool rotateLeft;
    bool rotateRight;
    float manualSpeed = 20f;
    public float lookSpeed = 10f;

    void Start()
    {
        LoadSkies();
        nextTickTime = Time.time;
    }

    void LoadSkies()
    {
        skybox = Camera.main.GetComponent<Skybox>();
        skies = new Material[5];
        skies[0] = Resources.Load("Skyboxes/Skies/Morning", typeof(Material)) as Material;
        skies[1] = Resources.Load("Skyboxes/Skies/Afternoon", typeof(Material)) as Material;
        skies[2] = Resources.Load("Skyboxes/Skies/LateAfternoon", typeof(Material)) as Material;
        skies[3] = Resources.Load("Skyboxes/Skies/Evening", typeof(Material)) as Material;
        skies[4] = Resources.Load("Skyboxes/Skies/Night", typeof(Material)) as Material;
    }

    void Update()
    {
        if (Time.time > nextTickTime)
        {
            string[] date = GetRealTime();
            nextTickTime = Time.time + delay;
            SetSky(date[10], date[8]);
        }
        Rotate();

        if (Input.GetAxis("Vertical") != 0) LookAround(Input.GetAxis("Vertical"));
    }

    void SetSky(string hour, string timeOfDay)
    {
        //Debug.Log(timeOfDay);
        switch (Int32.Parse(hour))
        {
            case 1:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 4;
                    else currentSky = 1;
                    break;
                }
            case 2:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 4;
                    else currentSky = 1;
                    break;
                }
            case 3:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 4;
                    else currentSky = 2;
                    break;
                }
            case 4:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 4;
                    else currentSky = 2;
                    break;
                }
            case 5:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 0;
                    else currentSky = 2;
                    break;
                }
            case 6:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 0;
                    else currentSky = 2;
                    break;
                }
            case 7:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 0;
                    else currentSky = 3;
                    break;
                }
            case 8:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 0;
                    else currentSky = 3;
                    break;
                }
            case 9:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 0;
                    else currentSky = 3;
                    break;
                }
           case 10:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 0;
                    else currentSky = 3;
                    break;
                }
            case 11:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 1;
                    else currentSky = 4;
                    break;
                }
            case 12:
                {
                    if (timeOfDay.ToLower() == "am") currentSky = 1;
                    else currentSky = 4;
                    break;
                }
        }
        //Debug.Log("Current sky: " + currentSky);
        skybox.material = skies[currentSky];

    }

    void LookAround(float value)
    {
        Vector3 currentAngle = transform.localEulerAngles;
        if (value == 0) currentAngle.x = 0;
        else currentAngle.x -= value * lookSpeed;
        transform.localEulerAngles = currentAngle;
    }

    void Rotate()
    {
        LookAround(0f);
        if (Input.GetButton("RotateLeft") || (autoRotate && rotateLeft))
        {
            rotateRight = false;
            rotateLeft = true;
        }
        else if (Input.GetButton("RotateRight") || (autoRotate && rotateRight))
        {
            rotateRight = true;
            rotateLeft = false;
        }
        else
        {
            rotateRight = false;
            rotateLeft = false;
        }
        if (rotateLeft)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * manualSpeed);
        }
        else if (rotateRight)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * manualSpeed);
        }
    }

    public void AutoRotate()
    {
        autoRotate = !autoRotate;
    }

    public void RotateLeft()
    {
        rotateRight = false;
        rotateLeft = true;
        AutoRotate();
    }

    public void RotateRight()
    {
        rotateRight = true;
        rotateLeft = false;
        AutoRotate();
    }
    //Returns an array of date data: month/day/year, day/month/year, day, month, year, now
    string[] GetRealTime()
    {
        DateTime time = DateTime.Now;

        string mdy = time.Date.ToString().Substring(0, 10);
        string day = "0";
        string month = "0";
        string year = "2015";
        if (mdy.Substring(1, 1) == "/")
        {
            month += mdy.Substring(0, 1);
            //Debug.Log("One digit month");
            if (mdy.Substring(3, 1) == "/")
            {
                //Debug.Log("One digit day ");
                day += mdy.Substring(2, 1);
                year = mdy.Substring(4, 4);
            }
            else
            {
                day = mdy.Substring(2, 2);
                year = mdy.Substring(5, 4);
            }
        }
        else
        {
            month = mdy.Substring(0, 2);
            //Debug.Log("One digit month");
            if (mdy.Substring(4, 1) == "/")
            {
                //Debug.Log("One digit day ");
                day += mdy.Substring(3, 1);
                year = mdy.Substring(5, 4);
            }
            else
            {
                day = mdy.Substring(3, 2);
                year = mdy.Substring(6, 4);
            }
        };

        string now = DateTime.Now.ToString();
        now = now.Substring(now.Length - 11, 11);
        string timeOfDay = now.Substring(now.Length - 2, 2);
        string nowWithoutMinutes = now.Substring(0, 5);
        string hour = nowWithoutMinutes.Substring(0,2);
        string dmy = day + "/" + month + "/" + year;

        string monthAlpha = "";
        switch (month)
        {
            case "01": monthAlpha = "January"; break;
            case "02": monthAlpha = "February"; break;
            case "03": monthAlpha = "March"; break;
            case "04": monthAlpha = "April"; break;
            case "05": monthAlpha = "May"; break;
            case "06": monthAlpha = "June"; break;
            case "07": monthAlpha = "July"; break;
            case "08": monthAlpha = "August"; break;
            case "09": monthAlpha = "September"; break;
            case "10": monthAlpha = "October"; break;
            case "11": monthAlpha = "November"; break;
            case "12": monthAlpha = "December"; break;
        }
        string dmyAlpha = monthAlpha + " " + day + ", " + year;
        string[] dates = { mdy, dmy, day, month, year, now, dmyAlpha, monthAlpha, timeOfDay, nowWithoutMinutes, hour };
        text.text = nowWithoutMinutes + " " + timeOfDay.ToLower() + Environment.NewLine + dmyAlpha;
        return dates;
    }
}
