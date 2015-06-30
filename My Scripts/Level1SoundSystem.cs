using UnityEngine;
using System.Collections;

public class Level1SoundSystem : MonoBehaviour {

	private AudioSource coinJingle;
	private AudioSource enemyDeathJingle;
	private AudioSource ouch;
	private AudioSource level1_1;
	private AudioSource level1_2;
	private AudioSource level1_3;
	private AudioSource level1_4;
	private AudioSource level1_5;
	private AudioSource coin_rush;
	private GameObject AIManager;
	// Use this for initialization
	void Start () {
		AIManager = GameObject.Find ("AIManager");
		AudioSource[] audios = GetComponents<AudioSource> ();
		coinJingle = audios [0];
		enemyDeathJingle = audios [1];
		ouch = audios [2];
		level1_1 = audios [3];
		level1_2 = audios [4];
		level1_3 = audios [5];
		level1_4 = audios [6];
		level1_5 = audios [7];
		coin_rush = audios [8];
	}
	
	void GotCoin(){
		coinJingle.Play ();
	}

	void DeadEnemy(){
		enemyDeathJingle.Play ();
	}

	void Ouch(){
		ouch.Play ();
	}

	void beatPhase1(){
		level1_1.mute = true;
		level1_2.mute = false;
	}

	void beatPhase2(){
		level1_2.mute = true;
		level1_3.mute = false;
	}

	void beatPhase3(){
		level1_3.mute = true;
		level1_4.mute = false;
	}

	void beatPhase4(){
		level1_4.mute = true;
		level1_5.mute = false;
	}

	IEnumerator coinRush(){
		if (!level1_1.mute) {
			level1_1.mute = true;
			coin_rush.Play ();
			AIManager.SendMessage("toggleCoinRush");
			yield return new WaitForSeconds(15);
			level1_1.mute = false;
			AIManager.SendMessage("toggleCoinRush");
		} else if (!level1_2.mute) {
			level1_2.mute = true;
			coin_rush.Play ();
			AIManager.SendMessage("toggleCoinRush");
			yield return new WaitForSeconds(15);
			level1_2.mute = false;
			AIManager.SendMessage("toggleCoinRush");
		} else if (!level1_3.mute) {
			level1_3.mute = true;
			coin_rush.Play ();
			AIManager.SendMessage("toggleCoinRush");
			yield return new WaitForSeconds(15);
			level1_3.mute = false;
			AIManager.SendMessage("toggleCoinRush");
		} else if (!level1_4.mute) {
			level1_4.mute = true;
			coin_rush.Play ();
			AIManager.SendMessage("toggleCoinRush");
			yield return new WaitForSeconds(15);
			level1_4.mute = false;
			AIManager.SendMessage("toggleCoinRush");
		} else if (!level1_5.mute) {
			level1_5.mute = true;
			coin_rush.Play ();
			AIManager.SendMessage("toggleCoinRush");
			yield return new WaitForSeconds(15);
			level1_5.mute = false;
			AIManager.SendMessage("toggleCoinRush");
		}
	}
}
