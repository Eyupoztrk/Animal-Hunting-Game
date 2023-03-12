using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammoAmount;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            GetComponent<Animator>().enabled = true;

            GameManager.Instance.OtherBullets += ammoAmount;
            GameManager.Instance.remainingBulletsText.text = GameManager.Instance.OtherBullets.ToString();
            Invoke("SetActiveFalse", 2);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().enabled = true;

        GameManager.Instance.OtherBullets += ammoAmount;
        GameManager.Instance.remainingBulletsText.text = GameManager.Instance.OtherBullets.ToString();
        Invoke("SetActiveFalse", 2);

    }

    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}
