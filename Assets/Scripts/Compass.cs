using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject mainObject;
    Vector3 direction;
    void Start()
    {
        
    }

    void Update()
    {
        FindObject();
    }

    public void FindObject()
    {
        direction.z = mainObject.transform.eulerAngles.y;
        transform.localEulerAngles = direction;

    }
}
