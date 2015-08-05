using UnityEngine;
using System.Collections;

public class FadingOut : MonoBehaviour {

	public GameObject ScoreManager;
	public GameObject AIManager;
		bool fadeIn = false;
		bool fadeOut = false;
		bool check;
		float fadeSpeed = 0.01f;
		float minAlpha = 0.0f;
		float maxAlpha = 1.0f;
		Color color;
		
		
		void Awake()
		{
			color = GetComponent<Renderer>().material.color;  
			color.a = -.5f;
			check = true;
		}
		
		void Update()
		{    
		GetComponent<Renderer> ().material.color = color;
			
		if (!SphereMovement.paused) {
			if (fadeIn && !fadeOut)
				FadeIn ();
			
			if (fadeOut && !fadeIn)
				FadeOut ();

			if (color.a <= minAlpha && !check) {
				ScoreManager.SendMessage ("StartScore");
				AIManager.SendMessage ("StartPhase");
			}
			
			if (color.a <= minAlpha && check) {
				fadeOut = false;
				if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.D)
					|| Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown
			   (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow)) {
					fadeIn = true;    
					check = false;
				}

			}
			
			if (color.a >= maxAlpha) {
				fadeIn = false;
				if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.D)
					|| Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown
			   (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow)) {
					fadeOut = true;    
				}
			}
		}
	}
		void FadeIn()
		{
			color.a += fadeSpeed;
		}
		
		void FadeOut()
		{
			color.a -= fadeSpeed;
		}
	
}
