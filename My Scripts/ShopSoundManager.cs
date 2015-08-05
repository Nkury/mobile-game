using UnityEngine;
using System.Collections;

public class ShopSoundManager : MonoBehaviour {

	private AudioSource itemBought;
	// Use this for initialization
	void Start () {
		AudioSource[] audios = GetComponents<AudioSource> ();
		itemBought = audios [1];
	}
	// Update is called once per frame
	void Update () {
	
	}

	void itemPurchased(){
		itemBought.Play ();
	}
}
