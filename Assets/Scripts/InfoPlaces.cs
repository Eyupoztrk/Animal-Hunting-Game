using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPlaces : MonoBehaviour
{
    [SerializeField] string InfoText;
    [SerializeField] int infoTime;

    [SerializeField] InfoManager InfoManager;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public string GetInfoText()
    {
        return InfoText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        InfoManager.SetInfo(InfoText,infoTime);
        InfoManager.DestroyIt(this.gameObject);
    }

    
}
