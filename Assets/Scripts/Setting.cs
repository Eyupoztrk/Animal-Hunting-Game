using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public static Setting Instance { get; private set; }
    [SerializeField] GameObject mainsettingPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject animalMarketPanel;

    [SerializeField] GameObject[] qualityButtons;
    [SerializeField] GameObject[] resulationButtons;

    [SerializeField] Slider backgraundSoundSlider;
    [SerializeField] Slider otherSoundSlider;

    [SerializeField] AudioSource backgraundSound;
    [SerializeField] AudioSource[] othersounds;


    [SerializeField] TextMeshProUGUI killedDeerAmountText;
    [SerializeField] TextMeshProUGUI killedBuffaloAmountText;
    [SerializeField] TextMeshProUGUI killedBoarAmountText;
    [SerializeField] AudioSource clickSound;

    public int killedDeerAmount;
    public int killedBuffaloAmount;
    public int killedBoarAmount;


    private void Awake()
    {


        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {

        FirstSetups();
        killedDeerAmountText.text = "KILLED: " + PlayerPrefs.GetInt("Deer").ToString();
        killedBuffaloAmountText.text = "KILLED: " + PlayerPrefs.GetInt("Buffalo").ToString();
        killedBoarAmountText.text = "KILLED: " + PlayerPrefs.GetInt("Boar").ToString();

    }

    void Update()
    {
        AudioListener.volume = backgraundSoundSlider.value;
        
    }

    #region settings
    public void FirstSetups()
    {

        if (!PlayerPrefs.HasKey("Quality"))
        {
            SetQualityNormal();
        }
        else
        {
            if (PlayerPrefs.GetInt("Quality") == 1)
                SetQualityLow();
            else if (PlayerPrefs.GetInt("Quality") == 2)
                SetQualityNormal();
            else if (PlayerPrefs.GetInt("Quality") == 3)
                SetQualityHigh();
        }

        if (!PlayerPrefs.HasKey("Res"))
        {
            SetResulationHigh();
        }
        else
        {
            if (PlayerPrefs.GetInt("Res") == 1)
                SetResulationLow();
            else if (PlayerPrefs.GetInt("Res") == 2)
                SetResulationNormal();
            else if (PlayerPrefs.GetInt("Res") == 3)
                SetResulationHigh();
        }

        if(!PlayerPrefs.HasKey("BackValue"))
        {
            backgraundSoundSlider.value = 1;
        }
        else
        {

            backgraundSoundSlider.value = PlayerPrefs.GetFloat("BackValue");
        }

        if(!PlayerPrefs.HasKey("Deer") && !PlayerPrefs.HasKey("Buffalo") && !PlayerPrefs.HasKey("Boar"))
        {
            PlayerPrefs.SetInt("Deer", 0);
            PlayerPrefs.SetInt("Buffalo", 0);
            PlayerPrefs.SetInt("Boar", 0);
        }
    }
    // quality settings
    public void SetQualityLow()
    {
        QualitySettings.SetQualityLevel(0, true);
        SetButtonColor(qualityButtons, "Low");
        PlayerPrefs.SetInt("Quality", 1);
    }
    
    public void SetQualityNormal()
    {
        QualitySettings.SetQualityLevel(1, true);
        SetButtonColor(qualityButtons, "Normal");
        PlayerPrefs.SetInt("Quality", 2);
    }
    
    public void SetQualityHigh()
    {
        QualitySettings.SetQualityLevel(2, true);
        SetButtonColor(qualityButtons, "High");
        PlayerPrefs.SetInt("Quality", 3);
    }

    // resulation settings



    public void SetResulationLow()
    {
        
        Screen.SetResolution(960,540 ,true);
       
        SetButtonColor(resulationButtons, "Low");
        PlayerPrefs.SetInt("Res", 1);
    }

    public void SetResulationNormal()
    {
        Screen.SetResolution(1920, 1080, true);
        SetButtonColor(resulationButtons, "Normal");
        PlayerPrefs.SetInt("Res", 2);
    }

    public void SetResulationHigh()
    {
        Screen.SetResolution(2560, 1440, true);
        SetButtonColor(resulationButtons, "High");
        PlayerPrefs.SetInt("Res", 3);
    }

    public void SetButtonColor(GameObject[] buttons, string name)
    {
        foreach (var item in buttons)
        {
            if(item.transform.name.Equals(name))
            {
                item.GetComponent<Image>().color = Color.green;
            }
            else
            {
                item.GetComponent<Image>().color = Color.white;
            }
        }

        
    }


    // sound settings

    public void sliderUp()
    {
        PlayerPrefs.SetFloat("BackValue", backgraundSoundSlider.value);
    }

    #endregion

    #region panel settings

    public void ClosePanel()
    {
        mainsettingPanel.SetActive(false);
        clickSound.Play();
    }

    public void OpenPanel()
    {
        mainsettingPanel.SetActive(true);
        CloseMarketPanel();
        OpenSettingPanel();
        clickSound.Play();
    } 
    
    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        clickSound.Play();

    }
    
    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
        clickSound.Play();
    }

    public void OpenMarketPanel()
    {
        animalMarketPanel.SetActive(true);
        CloseSettingPanel();
        clickSound.Play();
    }

    public void CloseMarketPanel()
    {
        animalMarketPanel.SetActive(false);
        clickSound.Play();

    }
    #endregion

    #region Market Settings

    public void SetAnimalTexts(string animalName)
    {
        if(animalName.Equals("Deer"))
        {                       
            PlayerPrefs.SetInt("Deer", PlayerPrefs.GetInt("Deer") +1);
            killedDeerAmountText.text = "KILLED: "+PlayerPrefs.GetInt("Deer").ToString();
        }
        
        else if(animalName.Equals("Buffalo"))
        {
          
            PlayerPrefs.SetInt("Buffalo", PlayerPrefs.GetInt("Buffalo") + 1);
            killedBuffaloAmountText.text = "KILLED: " + PlayerPrefs.GetInt("Buffalo").ToString();
        }


        else if(animalName.Equals("Boar"))
        {
           
            PlayerPrefs.SetInt("Boar", PlayerPrefs.GetInt("Boar") + 1);
            killedBoarAmountText.text = "KILLED: " + PlayerPrefs.GetInt("Boar").ToString();
        }


        else
        {
            Debug.LogError("There is no animal called" + animalName + "."); ;
        }
    }


    #endregion
}
