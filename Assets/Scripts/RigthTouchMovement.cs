using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RigthTouchMovement : MonoBehaviour, IDragHandler
{
    

    private float horizontal;
    private float vertical;
    void Start()
    {
         Debug.Log("s�r�kl�yor");
    }

    
    void Update()
    {
       
    }

    public void Drag()
    {
       
    }

    public float GetHorizontalAxis()
    {
        return horizontal;
    }

    public float GetVerticalAxix()
    {
        return vertical;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("s�r�kl�yor");
     
        horizontal = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        vertical = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
       
    }
}
