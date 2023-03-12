using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [SerializeField] AudioSource clickSound;

    public void OpenGame()
    {
      //  SceneManager.LoadScene("GameScene");
    }
    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI LoadingValueText;

    public void LoadingScene()
    {
        clickSound.Play();
        progressBar.gameObject.SetActive(true);
        StartCoroutine(startLoading(1));
    }

   
    IEnumerator startLoading(int level)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while(!async.isDone)
        {
            Debug.Log(async.progress);
            progressBar.value = async.progress;
            LoadingValueText.text = "%"+(async.progress * 100).ToString("0");
            yield return null;
        }
    }
}
