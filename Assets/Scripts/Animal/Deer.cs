using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : AS_BulletHiter
{
    private List<GameObject> Targets;
    private List<GameObject> FarTargets;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deadPoint;
     private DummyTarget dummyTarget;

    public Gun gun;
    NavMeshAgent agent;
    int index,indexFar;
    bool isComplete, isComplete0,isComplete2,  isPlayerNear;
    public bool isComplete3;
    bool isDead;

    void Start()
    {

        Targets = GameManager.Instance.Targets;
        FarTargets = GameManager.Instance.FarTargets;
        isComplete = true;
        isComplete0 = true;
        isComplete3 = true;
        isPlayerNear = false;
        index = Random.Range(0, Targets.Count);
       
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dummyTarget = GetComponent<DummyTarget>();
        agent.destination = Targets[index].transform.position;
    }
    

    void Update()
    {


        if(isDead)
        {
         
            GameManager.Instance.UpdadeAnimals(this.gameObject);
            animator.SetBool("isDead", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isEating", false);
            isDead = false;
            agent.Stop();
            agent.speed = 0;
            deadPoint.SetActive(true);



        }

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 25f && isComplete0)
        {
            isComplete0 = false;
            isPlayerNear = true;
            isComplete2 = true;
            SetAction(true);
        }

        if (!isPlayerNear)
        {
            if (Vector3.Distance(gameObject.transform.position, Targets[index].transform.position) < 2f && isComplete)
            {
                isComplete = false;
                
                SetAction(false);

            }
        }

        if(isPlayerNear)
        {
            if (Vector3.Distance(gameObject.transform.position, FarTargets[indexFar].transform.position) < 2 && isComplete2)
            {
                Debug.Log("ss");
                isPlayerNear = false;
                isComplete2 = false;
                isComplete0 = true;
                isComplete3 = true;

                SetAction(false);
            }
        }

        if(gun.muzzleCopy != null)
        {
            
            if (Vector3.Distance(gameObject.transform.position, gun.muzzleCopy.transform.position) < 100 && isComplete3)
            {
                Debug.Log("chech");
                isComplete3 = false;
                isPlayerNear = true;
                isComplete2 = true;
                SetAction(true);
            }
        }
        
        
       




    }

    public override void OnHit(RaycastHit hit, AS_Bullet bullet)
    {
        Setting.Instance.SetAnimalTexts("Deer");

        isDead = true;
        gameObject.GetComponent<AnimalStateForSaving>().isDead = true;
        PlayerPrefs.SetInt(gameObject.transform.name, 1); // 0 is alive 1 is dead;
        AddAudio(hit.point);
        base.OnHit(hit, bullet);

        
    }

    public void SetTarget()
    {
        
        int randomIndex = Random.Range(0, 3);
        while(index ==randomIndex)
             randomIndex = Random.Range(0, 3);

        index = randomIndex;
        isComplete = true;
        agent.destination = Targets[index].transform.position;

    }

    public void SetAction(bool isPlayer)
    {
       

        if (!isPlayer)
        {
            int random = Random.Range(0, 10);
           

            if (random %2 == 0)
            {
                agent.speed = 1.3f;
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isEating", false);
                SetTarget();


            }

            else
            {
                agent.speed = 1.3f;
                animator.SetBool("isEating", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                StartCoroutine(Wait());
            }
        }
        else
        {
           
            SetFarTargets();
            animator.SetBool("isEating", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            
            agent.speed = 10;
            

        }

    }

    public IEnumerator Wait()
    {
       
        yield return new WaitForSeconds(35);
        animator.SetBool("isWalking", true);
        animator.SetBool("isEating", false);

        SetTarget();

    }

    public void SetFarTargets()
    {

        indexFar = Random.Range(0, FarTargets.Count);

        agent.destination = FarTargets[indexFar].transform.position;
    }
}
