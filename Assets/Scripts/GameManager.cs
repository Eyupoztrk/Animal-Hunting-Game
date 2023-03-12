using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> Animals;
    public List<GameObject> Targets;
    public List<GameObject> FarTargets;

 
    [Header("Butons in UI")]
    public GameObject ZoomButton1, ZoomButton2;


    [Header("Bullets UI")]
    public TextMeshProUGUI bulletsInGunText;
    public TextMeshProUGUI remainingBulletsText;

    public bool canFire;
    public int OtherBullets;
    public int RemainingBullets;
    public int BulletsinGun;

   public bool isStart;

    [Header("GetDistanceInfo")]
    public TextMeshProUGUI DistanceInfoText;
    public GameObject InfoImage;
    private RaycastHit DistanceHit;
    public AudioSource raySound;
    public GameObject InfoEffect;


    public AudioSource footSound;



    private void Awake()
    {


        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
      
        OtherBullets = 100;
        BulletsinGun = 10;
        RemainingBullets = OtherBullets + BulletsinGun;
        bulletsInGunText.text = BulletsinGun.ToString();
        remainingBulletsText.text = OtherBullets.ToString();

    }

   
    void Update()
    {
        if(BulletsinGun <=0 && isStart)
        {
            SetBullets(0, true);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GetDistanceFromAnyWhere();
        }
    }


    public void UpdadeAnimals(GameObject animal)
    {
        Animals.Remove(animal);
    }

    #region Bullet system in UI

    public void SetBullets(int Bullet, bool isReleoading)
    {
        if(!isReleoading)
        {

            BulletsinGun += Bullet;
           // AllBullets += Bullet;
            RemainingBullets = OtherBullets + BulletsinGun;
            
        }
        else
        {
            isStart = false;
            // AllBullets -= 10;
            if(BulletsinGun !=0)
            {
                OtherBullets -= 10 -BulletsinGun;
                BulletsinGun = 10;

                RemainingBullets = OtherBullets + BulletsinGun;
            }
            else
            {
                OtherBullets -= 10;
                BulletsinGun = 10;

                RemainingBullets = OtherBullets + BulletsinGun;
            }
            
           

        }

        bulletsInGunText.text = BulletsinGun.ToString();
        remainingBulletsText.text = OtherBullets.ToString();
        
    }

    public void SetCanFire()
    {
        if (OtherBullets <= 0)
            canFire = false;

    }



    #endregion

    #region GetDistanceFromAnyWhere

    public void GetDistanceFromAnyWhere()
    {
       Vector3 rayPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(0,0,3));
        if (Physics.Raycast(rayPoint, Camera.main.transform.forward, out DistanceHit))
        {
            Debug.Log(DistanceHit.transform.name);
            Instantiate(InfoEffect, DistanceHit.point,Quaternion.Euler(90,0,0));
            raySound.Play();
            DistanceInfoText.text = DistanceHit.distance.ToString(".#") +" m";
        }
    }
    #endregion
}
