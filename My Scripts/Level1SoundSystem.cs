using UnityEngine;
using System.Collections;

public class Level1SoundSystem : MonoBehaviour {

	private AudioSource coinJingle;
	private AudioSource enemyDeathJingle;
	private AudioSource ouch;
	private AudioSource level1_1;
	private AudioSource coin_rush;
	private AudioSource boss_entrance;
	private AudioSource boss_music;
	private AudioSource mission_complete;
	private GameObject AIManager;
	// Use this for initialization
	void Start () {
		AIManager = GameObject.Find ("AIManager");
		AudioSource[] audios = GetComponents<AudioSource> ();
		coinJingle = audios [0];
		enemyDeathJingle = audios [1];
		ouch = audios [2];
		level1_1 = audios [3];
		coin_rush = audios [4];
		boss_entrance = audios [5];
		boss_music = audios [6];
		mission_complete = audios [7];
	}

	void Update(){
	
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

	void startBoss(){
		level1_1.mute = true;
		boss_music.Play ();
	}

	void BossEntrance(){
		level1_1.mute = true;
		boss_entrance.Play ();
	}
	IEnumerator coinRush(){
			level1_1.mute = true;
			coin_rush.Play ();
			AIManager.SendMessage("toggleCoinRush");
			yield return new WaitForSeconds(15);
			level1_1.mute = false;
			AIManager.SendMessage("toggleCoinRush");
	}

	void missionComplete(){
		mission_complete.Play ();
	}
}
