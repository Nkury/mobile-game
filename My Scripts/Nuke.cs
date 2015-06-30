using UnityEngine;
using System.Collections;

public class Nuke : MonoBehaviour {

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Enemy") 
			other.SendMessage("DestroySelf");
	}
}
