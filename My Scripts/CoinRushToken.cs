using UnityEngine;
using System.Collections;

public class CoinRushToken : MonoBehaviour {

	private float rotateSpeed = 60f;
	private int interval = 1;
	private bool startingUp = false;
	private GameObject scoreManage; 
	private GameObject soundSystem;	
	// Use this for initialization
	void Start () {
		
		StartCoroutine (Expire ());
		scoreManage = GameObject.Find ("ScoreManager");
		soundSystem = GameObject.Find ("Sound System");
	}
	
	// Update is called once per frame
	void Update () {
		interval += 1;
		
		if (interval % 3 == 0 && startingUp) {
			GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
		} else if (!startingUp) {
			GetComponent<Renderer>().enabled = true;
		}

		transform.Rotate (0, rotateSpeed * Time.deltaTime, 0, Space.World);
	}

	IEnumerator Expire(){
		yield return new WaitForSeconds(1);
		startingUp = true;
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}

	void Collect(){
		soundSystem.SendMessage ("coinRush");
		Destroy (gameObject);
	}

}
