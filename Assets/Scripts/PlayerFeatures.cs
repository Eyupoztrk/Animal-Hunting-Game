using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFeatures : MonoBehaviour
{
    [SerializeField] DayLoop day;
    [SerializeField] GameObject Player;


    [Header("***** SLEEP PART *****")]
    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject sleepPanel;
    [SerializeField] private GameObject sleepButtonPanel;
    [SerializeField] private GameObject InfoPanel;
    [SerializeField] private TextMeshProUGUI sleepText;  
    
   



    bool sleeping;

    void Start()
    {
        
    }

    void Update()
    {
        SleepUpdate();   

        if (Input.GetKeyDown(KeyCode.L))
            Sleep();
    }



    #region SPEELING

    public void SleepUpdate()
    {

        if (GetDistance(Player, bed) < 2)
        {
            if (!sleeping)
                sleepButtonPanel.SetActive(true);
        }
        else
        {
            sleepButtonPanel.SetActive(false);
            sleeping = false;
        }

    }

    public void Sleep()
    {
       
        if(day.currentTime.Hour >= 20 || day.currentTime.Hour <=6)
        {
            sleeping = true;
            sleepPanel.SetActive(true);
            sleepButtonPanel.SetActive(false);

            Invoke("SetPanelInActive", 3f);
        }
       
       
    }

    private void SetPanelInActive()
    {
        sleepPanel.SetActive(false);
        day.AddHours(7);
        
    }

    #endregion

    #region GET AMMO

    




    #endregion


    public float GetDistance(GameObject g1, GameObject g2)
    {
        return Vector3.Distance(g1.transform.position, g2.transform.position);
    }
}
