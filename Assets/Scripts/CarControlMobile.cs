using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlMobile : MonoBehaviour
{

    public Joystick joystick;

    public bool isBreake;
    public bool isDownCam;

    [SerializeField] car car;

    public float Vertical;
    public float Horizontal;


    void Start()
    {
        Horizontal = 0;
        Vertical = 0;
    }
    
    void Update()
    {

       
    }


    public void BrakeDown()
    {
        isBreake = true;
    }


    public void BrakeUp()
    {
        isBreake = false;
    }



    public void GoButtonDown()
    {

        Vertical = 1f;
    } 
    
    public void GoButtonUp()
    {
        Vertical = 0f;

    }
    
    public void BackButtonDown()
    {
        Vertical = -1f;

    }
    
    public void BackButtonUp()
    {
        Vertical = 0f;

    }
    
    public void RightButtonDown()
    {
        Horizontal = 0.5f;

    }
    
    public void RightButtonUp()
    {
        Horizontal = 0f;

    }

    public void Mathhh()
    {
        Debug.Log("merhaa");
    }
    
    public void LeftButtonDown()
    {
        Horizontal = -0.5f;

    }
    
    public void LeftButtonUp()
    {
        Horizontal = 0f;

    }

    public void ChanceCamDown()
    {
       
        isDownCam = true;
    }
    
    public void ChanceCamUp()
    {
       
        isDownCam = false;
    
     }
}
