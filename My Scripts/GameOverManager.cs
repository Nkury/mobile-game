using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
 	
	public GUIStyle textstyle;
	public GUIStyle buttonstyle;
	public GameObject sound;
	public Font theFont;

	void Start(){
		/*
		if (AIManager.ph1on)
			AIManager.ph1on = true;
		else if (AIManager.ph2on) {
			AIManager.ph1on = false;
			AIManager.ph2on = true;
		} else if (AIManager.ph3on) {
			AIManager.ph1on = false;
			AIManager.ph2on = false;
			AIManager.ph3on = true;
		}
		AIManager.ph2on = false;
		AIManager.ph3on = false;
		AIManager.ph4on = false;
		AIManager.ph5on = false;

		AIManager.start1 = false;
		AIManager.start2 = false;
		AIManager.start3 = false;
		AIManager.start4 = false;
		AIManager.start5 = false;
		AIManager.start6 = false;
		*/
		SphereMovement.lives = 3;
		ScoreManager.lifeScore = 0;
	}

	void OnGUI(){
		textstyle.font = theFont;
		GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 2.8f,	
		                          (Screen.height / 2) - Screen.height / 2.8f, Screen.width,
		                          Screen.height));
		GUI.Label(new Rect (0, 0, Screen.width / 2, Screen.height / 2), "Game Over", textstyle);
		if (GUI.Button (new Rect (150, 300, 150, 50), "Try Again", buttonstyle)) {
			sound.GetComponents<AudioSource> () [1].Play ();
			Application.LoadLevel ("level 1");
		}
		if (GUI.Button (new Rect (150, 400, 150, 50), "Title Screen", buttonstyle)) {
			sound.GetComponents<AudioSource> () [1].Play ();
			Application.LoadLevel ("title screen");
		}
		GUI.EndGroup ();
	}
}
