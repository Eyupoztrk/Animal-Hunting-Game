using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Giraffe : MonoBehaviour
{
    [SerializeField] private GameObject[] Targets;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    NavMeshAgent agent;
    int index;
    bool isComplete, isContinue;
    void Start()
    {
        isComplete = true;
        index = Random.Range(0, Targets.Length);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        agent.destination = Targets[index].transform.position;


        if (Vector3.Distance(gameObject.transform.position, Targets[index].transform.position) < 2f && isComplete)
        {
            isComplete = false;
            SetAction(false);

        }

        if(Vector3.Distance(gameObject.transform.position, Targets[index].transform.position) < 5f)
        {
            SetAction(true);
        }




    }

    public void SetTarget()
    {

        int randomIndex = Random.Range(0, 3);
        while (index == randomIndex)
            randomIndex = Random.Range(0, 3);

        index = randomIndex;
        isComplete = true;


    }

    public void SetAction(bool isPlayer)
    {
        if(!isPlayer)
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {

                animator.SetBool("isWalking", true);
                animator.SetBool("isEating", false);
                SetTarget();


            }

            else if (random == 1)
            {
                animator.SetBool("isEating", true);
                animator.SetBool("isWalking", false);
                StartCoroutine(Wait());
            }
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isEating", false);
            SetTarget();
        }
        
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(35);
        animator.SetBool("isWalking", true);
        animator.SetBool("isEating", false);

        SetTarget();

    }
}
