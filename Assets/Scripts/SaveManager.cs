using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

   
 
    public GameObject player;
    public GameObject car;
    public GameObject wolf;

    [SerializeField] Vector3 playerFirstPos;
    [SerializeField] RectTransform carFirstPos;
    [SerializeField] Vector3 wolfFirstPos;

    [SerializeField] GameObject[] InfoPanels;
    [SerializeField] List<GameObject> animals;

    public DayLoop dayLoop;
    

    void Start()
    {
       
        if(!PlayerPrefs.HasKey("PlayerPosX")) // player open the game fist time
        {

            SetFirstSaving();

        }
        else 
        {
          
            foreach (var item in InfoPanels)
            {
                
                Destroy(item);
            }
            GameManager.Instance.Animals = animals;

            SetPlayer();
            SetCar();
            SetWolf();
            SaveAnimals();

            // when player open the game first time then Info panel can be actives after that they have to be deleted

           


        }
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        PlayerPrefs.DeleteAll();
    }

    private void SetFirstSaving()
    {
        player.transform.position = playerFirstPos;
        player.transform.GetChild(0).transform.rotation = Quaternion.identity;

        car.transform.position = carFirstPos.position;
        car.transform.rotation = Quaternion.identity;

        wolf.transform.position = wolfFirstPos;
        wolf.transform.rotation = Quaternion.identity;

       

    }

    public void Save()
    {
        SavePlayer();
        SaveCar();
        SaveWolf();
        SaveAnimals();
        SaveTime(); 
    }


    #region Saving Gameobjects
    public void SavePlayer()
    {

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
       

        PlayerPrefs.SetFloat("PlayerRotX", player.transform.rotation.x);
        PlayerPrefs.SetFloat("PlayerRotY", player.transform.rotation.y);
        PlayerPrefs.SetFloat("PlayerRotZ", player.transform.rotation.z);


    /*    PlayerPosX = PlayerPrefs.GetFloat("PlayerPosX");
        PlayerPosY = PlayerPrefs.GetFloat("PlayerPosY");
        PlayerPosZ = PlayerPrefs.GetFloat("PlayerPosZ");

        PlayerRotX = PlayerPrefs.GetFloat("PlayerRotX");
        PlayerRotY = PlayerPrefs.GetFloat("PlayerRotY");
        PlayerRotZ = PlayerPrefs.GetFloat("PlayerRotZ");*/


       // SetPlayer();
    }
    public void SetPlayer()
    {
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ"));
        player.transform.GetChild(0).transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("PlayerRotX"), PlayerPrefs.GetFloat("PlayerRotY"), PlayerPrefs.GetFloat("PlayerRotZ"));
    }


    public void SaveCar()
    {
        PlayerPrefs.SetFloat("CarPosX", car.GetComponent<RectTransform>().position.x);
        PlayerPrefs.SetFloat("CarPosY", car.GetComponent<RectTransform>().position.y);
        PlayerPrefs.SetFloat("CarPosZ", car.GetComponent<RectTransform>().position.z);


        PlayerPrefs.SetFloat("CarRotX", car.GetComponent<RectTransform>().rotation.x);
        PlayerPrefs.SetFloat("CarRotY", car.GetComponent<RectTransform>().rotation.y);
        PlayerPrefs.SetFloat("CarRotZ", car.GetComponent<RectTransform>().rotation.z);
    }

    public void SetCar()
    {
        car.transform.position = new Vector3(PlayerPrefs.GetFloat("CarPosX"), PlayerPrefs.GetFloat("CarPosY"), PlayerPrefs.GetFloat("CarPosZ"));
        car.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("CarRotX"), PlayerPrefs.GetFloat("CarRotY"), PlayerPrefs.GetFloat("CarRotZ"));
    }
    
    
    public void SaveWolf()
    {
        PlayerPrefs.SetFloat("WolfPosX", wolf.transform.position.x);
        PlayerPrefs.SetFloat("WolfPosY", wolf.transform.position.y);
        PlayerPrefs.SetFloat("WolfPosZ", wolf.transform.position.z);


        PlayerPrefs.SetFloat("WolfRotX", wolf.transform.rotation.x);
        PlayerPrefs.SetFloat("WolfRotY", wolf.transform.rotation.y);
        PlayerPrefs.SetFloat("WolfRotZ", wolf.transform.rotation.z);
    }

    public void SetWolf()
    {
        wolf.transform.position = new Vector3(PlayerPrefs.GetFloat("WolfPosX"), PlayerPrefs.GetFloat("WolfPosY"), PlayerPrefs.GetFloat("WolfPosZ"));
        wolf.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("WolfRotX"), PlayerPrefs.GetFloat("WolfRotY"), PlayerPrefs.GetFloat("WolfRotZ"));
    }

    #endregion

    #region Saving Status

    public void SaveAnimals()
    {
        foreach (var item in animals)
        {
            if(PlayerPrefs.GetInt(item.name)==1) // if dead
            {
              //  animals.Remove(item);
                item.SetActive(false);
              
            }
        }
      //  GameManager.Instance.Animals = animals;

       
    }

    public void SaveTime()
    {
        PlayerPrefs.SetInt("Time", dayLoop.currentTime.Hour);
    }


    #endregion

    

}
