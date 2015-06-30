using UnityEngine;
using System.Collections;

public class ShopKeeper : MonoBehaviour {

	public float rotateSpeed = 60;
	public bool spin = false;
	private int interval = 1;
	private bool warn = false;
	private static bool welcome_back = false;
	private GameObject player;

	public string[] dialogue = {"Hi there!",
		"Yeah, over here!",
		"Hi! Welcome to the shop. My name's Mars.\n Press SPACE", 
		"We have items from extra lives to armor to nukes! \n Press SPACE", 
		"Step on a pedestal to purchase an item!\n Press SPACE",
		"You better bring some money, though.",
		"Oh, and don't mind the mess. Happy shopping!\n Press SPACE"
	};

	public int index = 0;
	private Rect dialogueRect = new Rect(435,25,350,25);

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		interval++;

		if (index < 2 && interval % 50 == 0) {
			index++;
		}

		if (interval % 170 == 0) {
			index++;
		}

		if (welcome_back) {
			if(index == 0)
				player.SendMessage("enableTalking");
			else
				player.SendMessage("disableTalking");
		} else {
			if (warn || index < dialogue.Length) {
				player.SendMessage ("enableTalking");
			} else
				player.SendMessage ("disableTalking");
		}
		if (index < dialogue.Length || warn) {
			if (interval % 20 == 0 || spin) {
				transform.Rotate (0, rotateSpeed * Time.deltaTime, -2, Space.World);
				spin = true;
			}
		
			if (interval % 30 == 0 || !spin) {
				transform.Rotate (0, -rotateSpeed * Time.deltaTime, 2, Space.World);
				spin = false;
			}
		}
	}

	void OnGUI(){
		if (!welcome_back) {
			if (index == 0) {
				GUI.Box (new Rect (Camera.main.WorldToScreenPoint (transform.position).x - 
					Camera.main.WorldToScreenPoint (player.GetComponent<Transform> ().position).x / 2,
			                  Camera.main.WorldToScreenPoint (transform.position).y -
					Camera.main.WorldToScreenPoint (player.GetComponent<Transform> ().position).y / 2,
			                  75, 25), dialogue [index]);
			} else if (index == 1) {
				GUI.Box (new Rect (Camera.main.WorldToScreenPoint (transform.position).x - 
					Camera.main.WorldToScreenPoint (player.GetComponent<Transform> ().position).x / 2.3f,
			                  Camera.main.WorldToScreenPoint (transform.position).y -
					Camera.main.WorldToScreenPoint (player.GetComponent<Transform> ().position).y / .6f,
			                  175, 25), dialogue [index]);
			} else if (index < dialogue.Length) {
				GUI.Box (new Rect (Screen.width - Camera.main.WorldToScreenPoint (transform.position).x,
			                  Screen.height - Camera.main.WorldToScreenPoint (transform.position).y,
			                  350, 25), dialogue [index]);
			} else
				welcome_back = true;
		}

		if (welcome_back && index == 0) {
			GUI.Box (new Rect (Screen.width - Camera.main.WorldToScreenPoint (transform.position).x,
			                   Screen.height - Camera.main.WorldToScreenPoint (transform.position).y,
			                   350, 25), "Welcome back!");
		}
		if (warn) {
			GUI.Box (dialogueRect, "Hey! Don't you dare try to jump. Fragile stuff here!");
		}
	}

	void warnJump(){
		warn = true;
		StartCoroutine (disableWarn ());
	}

	IEnumerator disableWarn(){
		yield return new WaitForSeconds (2);
		warn = false;
	}
}
