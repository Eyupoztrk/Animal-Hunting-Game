using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayLoop : MonoBehaviour
{

    [SerializeField] private float timeMutliplier;
    [SerializeField] private float startHour;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Light sunLight;
    [SerializeField] private float sunsireHour;
    [SerializeField] private float sunsetHour;


    private TimeSpan sunsireTime;
    private TimeSpan sunsetTime;

    public DateTime currentTime;

    [SerializeField]
    private Color dayAmbientLight; 
    
    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    [SerializeField]
    private Material nightSkybox;

    [SerializeField]
    private Material daySkybox; 
    
    [SerializeField]
    private Material nearNight;
    
    [SerializeField]
    private Material nearDay;
  
    void Start()
    {
        if(!PlayerPrefs.HasKey("Time"))
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        else
            currentTime = DateTime.Now.Date + TimeSpan.FromHours(PlayerPrefs.GetInt("Time"));

        sunsireTime = TimeSpan.FromHours(sunsireHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        SetFog();

        
    }

    public void SetFog()
    {
        if (currentTime.Hour >= 19.5 && currentTime.Hour < 20.5) // gün batýmý
        {
            RenderSettings.skybox = nearNight;
            RenderSettings.fogColor = Color.black;
            RenderSettings.fogDensity = 0.001f;
        }

        else if (currentTime.Hour >= 20.5 || currentTime.Hour < 4) // gece
        {

            RenderSettings.fogColor = Color.black;
            if (RenderSettings.fogDensity < 0.07)
                RenderSettings.fogDensity += 0.00001f;
            RenderSettings.skybox = nightSkybox;

        }
        else if ((currentTime.Hour >= 4 && currentTime.Hour < 6)) // güne yakýn
        {

            if (RenderSettings.fogDensity >= 0.001f)
            {
               
                RenderSettings.fogDensity -= 0.00007f;
            }
        }

        else if (currentTime.Hour >= 6 && currentTime.Hour < 7) // gün doðumu
        {
            Debug.Log("sd");
            RenderSettings.skybox = nearDay;
            RenderSettings.fogDensity = 0.001f;
            RenderSettings.fogColor = Color.grey;
        }
        else // gündüz
        {


            RenderSettings.fogColor = Color.grey;
            RenderSettings.fogDensity = 0.008f;
            RenderSettings.skybox = daySkybox;
        }
    }
            

    public void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMutliplier);

        timeText.text = currentTime.ToString("HH:mm");
    }

    public void AddHours(int sleepTime)
    {
        currentTime = currentTime.AddHours(sleepTime);  // sleeping
    }

    public TimeSpan CalculateTimeDifference(TimeSpan formTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - formTime;

        if(difference.TotalSeconds<0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    public void RotateSun()
    {
        float sunLightRotation;

        if(currentTime.TimeOfDay>sunsetTime && currentTime.TimeOfDay<sunsetTime)
        {

            TimeSpan sunsetToSunsireDuration = CalculateTimeDifference(sunsetTime, sunsireTime);

            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunsireDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
           
           // 

            

        }
        else
        {
           

            TimeSpan sunsireToSunsetDuration = CalculateTimeDifference(sunsireTime, sunsetTime);

            TimeSpan timeSinceSunsire = CalculateTimeDifference(sunsireTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunsire.TotalMinutes / sunsireToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
          //  

        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }


    public void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, dotProduct);

        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
      //  RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
      
    }
}
