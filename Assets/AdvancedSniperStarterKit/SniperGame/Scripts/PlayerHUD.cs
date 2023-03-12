using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{

	public GameObject Player;
	public GameObject GameOver;
	private DamageManager damageManager;
	private GunHanddle gunHanddle;

	public Text HPtext;
	public Text AmmoText;

	void Start ()
	{
		if (Player) {
			damageManager = Player.GetComponent<DamageManager> ();
			gunHanddle = Player.GetComponent<GunHanddle> ();
		}
	}


	void Update ()
	{
		if (Player) {
			if (HPtext)
				HPtext.text = damageManager.hp.ToString();
			if (AmmoText)
				AmmoText.text = gunHanddle.CurrentGun.Clip + " / " + gunHanddle.CurrentGun.ClipSize;

			if(GameOver){
				GameOver.SetActive(false);
			}
		}else{
			Player = GameObject.FindGameObjectWithTag("Player");
			MouseLock.MouseLocked = false;
			if(GameOver){
				GameOver.SetActive(true);
			}
		}
	}

	public void Tryagain(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Quit(){
		Application.Quit();
	}
}
