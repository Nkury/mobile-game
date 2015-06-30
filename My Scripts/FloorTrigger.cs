using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour {

	void OnTriggerExit(Collider other){
		if (other.gameObject.name == "player") {
			other.SendMessage ("disableJump");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "player") {
			other.SendMessage ("enableJump");
		}
	}
}
