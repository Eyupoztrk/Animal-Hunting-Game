using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanic : MonoBehaviour
{

    RaycastHit hit;
    private Vector3 rayPoint;
    public bool canFire;
    float gunTimer;
    public float gunCoolDown;
    public Animator animator;
    public AudioSource shootSound;

    public Animation cameraShootAnim;

    
    void Start()
    {
        rayPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    }

    
    void Update()
    {
        
    }

    public void Shoot()
    {
        
        if (Physics.Raycast(rayPoint, Camera.main.transform.forward, out hit))
        {
            cameraShootAnim.Play("CameraShootAnim", PlayMode.StopAll);
            shootSound.Play();
            Debug.Log(hit.transform.name);
        }
             
    }
}
