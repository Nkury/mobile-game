using UnityEngine;
using System.Collections;

public class ShopFloorTrigger : MonoBehaviour {

	private GameObject shopkeeper;

	void Start()
	{
		shopkeeper = GameObject.Find ("shopkeeper");
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space) && !SphereMovement.talking)
		   shopkeeper.SendMessage("warnJump");
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.name == "player") {
			other.SendMessage ("disableJump");
		}
	}
}
