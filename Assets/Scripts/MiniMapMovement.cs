using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Player.transform.position.x, 20, Player.transform.position.z);
        transform.rotation = Quaternion.Euler(90, Player.transform.rotation.eulerAngles.y, Player.transform.rotation.eulerAngles.z);
    }
}
