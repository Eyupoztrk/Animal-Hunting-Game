using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPos : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] GameObject carCam;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject PlayerPos;
    [SerializeField] MiniMapMovement miniMapMovement;
    [SerializeField] GameObject carAudio;

    bool isRiding;
    bool inCar;


    [SerializeField] GameObject RideCarPanel;
    [SerializeField] GameObject CarControlPanel;
    [SerializeField] GameObject PlayerControlPanel;
    [SerializeField] GameObject PlayerSpawnPoint;
    [SerializeField] GameObject PlayerInCarPoint;
  
    void Start()
    {
        car.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        carAudio.SetActive(false);

    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("nn");
            GetInTheCar();
        }
        SleepUpdate();
    }

    public void SleepUpdate()
    {

     /*   if (GetDistance(Player, car) < 5)
        {
            if (!isRiding)
                RideCarPanel.SetActive(true);
        }
        else
        {
            RideCarPanel.SetActive(false);
            isRiding = false;
        }*/

    }


    public float GetDistance(GameObject g1, GameObject g2)
    {
        return Vector3.Distance(g1.transform.position, g2.transform.position);
        
    }


    public void GetInTheCar()
    {
        if (GetDistance(Player, car) < 5)
        {
            inCar = !inCar;
        if(!inCar)  // arabadan inme
        {
            Player.transform.position = PlayerSpawnPoint.transform.position;
                Player.transform.rotation = Quaternion.identity;
            Player.transform.parent = null;


            isRiding = false;
            Player.SetActive(true);
            carCam.SetActive(false);

            CarControlPanel.SetActive(false);
            PlayerControlPanel.SetActive(true);

            miniMapMovement.Player = PlayerPos;

            car.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            carAudio.SetActive(false);
            
        }
        else // arabaya binme
        {


          
                Player.transform.position = PlayerInCarPoint.transform.position;
                Player.transform.parent = car.transform;

                isRiding = true;
                Player.SetActive(false);
                carCam.SetActive(true);

                CarControlPanel.SetActive(true);
                PlayerControlPanel.SetActive(false);

                miniMapMovement.Player = car.gameObject;

                car.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None ;
                carAudio.SetActive(true);
               


           


        }


    }


    }
}
