using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineManager : MonoBehaviour
{
    LineRenderer line;
    public GameObject Player;
    public GameObject wolf;
    [SerializeField] TextMeshProUGUI DistanceText;
   

    void Start()
    {
        DistanceText.gameObject.SetActive(false);
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {

        SetLine();
        
    }

    public void SetLine()
    {
        if (wolf.GetComponent<OurWolf>().ANIMALFINDED)
        {
            DistanceText.gameObject.SetActive(true);
            DistanceText.text = ((int)Vector3.Distance(Player.transform.position, wolf.transform.position)).ToString() +" m";
            line.positionCount = 2;
            line.SetPosition(0, Player.transform.position);
            line.SetPosition(1, wolf.transform.position);
        }
        else
        {
            DistanceText.gameObject.SetActive(false);
            line.positionCount = 0;
        }
            
    }
}
