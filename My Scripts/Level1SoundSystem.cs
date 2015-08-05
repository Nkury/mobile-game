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
	private AudioSource finalResMusic;
	private AudioSource keepScore;
	private AudioSource boom1;
	private AudioSource boom2;
	private AudioSource boom3;
	private AudioSource boom4;
	private AudioSource boom5;
	private AudioSource boom6;
	private AudioSource boom7;
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
		finalResMusic = audios [8];
		keepScore = audios [9];
		boom1 = audios [10];
		boom2 = audios [11];
		boom3 = audios [12];
		boom4 = audios [13];
		boom5 = audios [14];
		boom6 = audios [15];
		boom7 = audios [16];
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

	void BossDead(){
		boss_music.mute = true;
	}

	void finalResultsMus(){
		finalResMusic.Play ();
	}

	void incScore(){
		keepScore.mute = false;
		keepScore.Play ();
	}

	void stopScore(){
		keepScore.mute = true;
	}
	void boomOne(){
		boom1.Play ();
	}

	void boomTwo(){
		boom2.Play ();
	}

	void boomThree(){
		boom3.Play ();
	}
	void boomFour(){
		boom4.Play ();
	}
	void boomFive(){
		boom5.Play ();
	}
	void boomSix(){
		boom6.Play ();
	}
	void boomSeven(){
		boom7.Play ();
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
