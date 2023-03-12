using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoManager : MonoBehaviour
{
    private string InfoText;
    private int InfoTime;

    private GameObject DestroyedObject;
    [SerializeField] GameObject[] InfoPlaces;
    [SerializeField] Text infoTextCanvas;
    [SerializeField] GameObject infoPanel;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void SetInfoPanelFalse()
    {
        infoPanel.SetActive(false);
    } 


    /// <summary>
    /// Gece dýþarda olmak tehlidelidir. Arabaya bin ve Eve doðru sür. Ev'ini bulmak için patika yolunu takip et.
    /// </summary>
    

    public void SetInfo(string infoText, int infoTime)
    {
        this.InfoTime = infoTime;
        this.infoTextCanvas.text = infoText;
        infoPanel.SetActive(true);
        Invoke("SetInfoPanelFalse", infoTime);
      
    }

    public void DestroyIt(GameObject gameObject)
    {
        DestroyedObject = gameObject;
        Invoke("DestroyAfterWaiting", InfoTime);
    }

    private void DestroyAfterWaiting()
    {
        
        Destroy(DestroyedObject);
    }
}
