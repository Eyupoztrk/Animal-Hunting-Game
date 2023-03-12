using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OurWolf : MonoBehaviour
{
    NavMeshAgent agent;

    private GameObject target;
    public GameObject player;
     List<GameObject> Animals;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioSource far;
    [SerializeField] AudioSource near;
    [SerializeField] LineManager line;


    bool findingAnimal,findedAnimal,findedPlayer;
    bool findPlayer;

    public bool ANIMALFINDED;
    void Start()
    {
        Animals = GameManager.Instance.Animals;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        agent.destination = player.transform.position;
    }

   
    void Update()
    {

        if(GetDistance(player,this.gameObject)<5)
        {
            ANIMALFINDED = false;
        }

        if(target !=null)
        {
            if (GetDistance(this.gameObject, target) < 5)
            {
                if (findedAnimal)
                {
                    Debug.Log("v");
                    FindedAnimal();
                }
                   
            }
            else
                findedAnimal = true;
        }



        if (GetDistance(this.gameObject, player) < 5)
        {
            if (findedPlayer)
                FindedPlayer();

        }
        else
        {
           
            if(!findingAnimal)
            {
                agent.destination = player.transform.position;
                findedPlayer = true;
                agent.speed = 5;
                animator.SetBool("isHowling", false);
                animator.SetBool("isRunning", true);
                findedAnimal = false;
                
            }
            
        }
            



        if (findingAnimal)
            FindNearstAnimal();
        if(findPlayer)
            agent.destination = player.transform.position;

        if (Input.GetKeyDown(KeyCode.F))
        {
            FindAnimal();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FindPlayer();

        }
    }

    
    public void FindedAnimal()
    {

        ANIMALFINDED = true;
        animator.SetBool("isHowling", true);
        animator.SetBool("isRunning", false);
        audioSource.Play();
        
        findedAnimal = false;
        agent.speed = 0;


    }

    public void FindedPlayer()
    {
        animator.SetBool("isHowling", false);
        animator.SetBool("isRunning", false);
       
        agent.speed = 0;
        findedPlayer = false;
    }

    public void FindNearstAnimal()
    {
        int idx = 0;
        float dist = GetDistance(Animals[0], this.gameObject);

        for (int i = 1; i < Animals.Count; i++)
        {
            float cdist = GetDistance(Animals[i], this.gameObject);

            if (cdist < dist)
            {
                idx = i;
                dist = cdist;
            }
        }


        target= Animals[idx];
      
        

        agent.destination = target.transform.position;
    
}

    public float GetDistance(GameObject g1,GameObject g2)
    {
        return Vector3.Distance(g1.transform.position, g2.transform.position);
    }

    public void FindAnimal()
    {
        near.Play();
        agent.speed = 5;
        findingAnimal = true;
        findPlayer = false;
        animator.SetBool("isHowling", false);
        animator.SetBool("isRunning", true);
    }

    public void FindPlayer()
    {
        ANIMALFINDED = false;
        far.Play();
        agent.speed = 5;
        findingAnimal = false;
        findPlayer = true;
        animator.SetBool("isHowling", false);
        animator.SetBool("isRunning", true);
    }

}
