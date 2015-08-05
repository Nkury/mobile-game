using UnityEngine;
using System.Collections;

public class scriptingForItems : MonoBehaviour {

	private GameObject player;
	private bool insufficientFundsHearts = false;
	private bool insufficientFundsNuke = false;
	private bool insufficientFundsArmor = false;
	private bool insufficientFundsCap = false;
	private bool buyExtraLife = false;
	private bool buyNuke = false;
	private bool buyArmor = false;
	private bool incLifeCapacity = false;
	private bool exit_shop = false;
	private GameObject soundSystem;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
		soundSystem = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (insufficientFundsHearts) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
		                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
		                          Screen.height / 2));
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
			         "EXTRA LIFE\nCost: $35.\n\n Grants player another heart for survival in\n the arena.\n\n\nInsufficient funds.");
			if (GUI.Button (new Rect (120, 150, 150, 100), "OK")) {
				insufficientFundsHearts = false;
				player.SendMessage ("disableTalking");
			}
			GUI.EndGroup ();
		} else if (insufficientFundsNuke) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
			         "NUKE\nCost: $30.\n\n Gives player a nuclear weapon.\nPress \"Q\" to activate and destroy all enemies" +
				" on screen\n Can only have one at a time.\n\n\nInsufficient funds.");
			if (GUI.Button (new Rect (120, 150, 150, 100), "OK")) {
				insufficientFundsNuke = false;
				player.SendMessage ("disableTalking");
			}
			GUI.EndGroup ();
		} else if (insufficientFundsArmor) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
			         "INVINCIBILITY\nCost: $25.\n\nBy pressing \"E\", the player can get out" +
				" of any sticky situation\nthrough a 3-second invincibility potion.\nOnly" +
				"one at a time.\n\n\nInsufficient funds.");
			if (GUI.Button (new Rect (120, 150, 150, 100), "OK")) {
				insufficientFundsArmor = false;
				player.SendMessage ("disableTalking");
			}
			GUI.EndGroup ();
		} else if (insufficientFundsCap) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
			         "HEART CONTAINER\nCost: $250.\n\n Increases the maximum amount of lives" +
				" the player can have.\nCurrent maximum: " + SphereMovement.max_lives + 
				"\n\n\nInsufficient funds.");
			if (GUI.Button (new Rect (120, 150, 150, 100), "OK")) {
				insufficientFundsCap = false;
				player.SendMessage ("disableTalking");
			}
			GUI.EndGroup ();
		} else if (buyExtraLife) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));

			if (SphereMovement.max_lives == SphereMovement.lives) {
				GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
				         "EXTRA LIFE\nCost: $35.\n\n Grants player another heart for survival in\n the arena.\n\n\n" +
				         "You have exceeded maximum lives and\ncannot purchase.");
				;
				
				if (GUI.Button (new Rect (120, 150, 150, 100), "OK")) {
					buyExtraLife = false;
					player.SendMessage ("disableTalking");
				}
			}
			else{
				GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
				         "EXTRA LIFE\nCost: $35.\n\n Grants player another heart for survival in\n the arena.\n\n\n" +
					"Would you like to purchase?");

				if (GUI.Button (new Rect (120, 150, 75, 50), "YES")) {
					buyExtraLife = false;
					player.SendMessage ("GainLife");
					ScoreManager.money -= 35;
					player.SendMessage ("disableTalking");
					soundSystem.SendMessage ("itemPurchased");
				}

				if (GUI.Button (new Rect (200, 150, 75, 50), "NO")) {
					buyExtraLife = false;
					player.SendMessage ("disableTalking");
				}
			}
			GUI.EndGroup ();
		} else if (buyNuke) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));
			if (SphereMovement.has_nuke) {
				GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
				         "NUKE\nCost: $30.\n\n Gives player a nuclear weapon.\nPress \"Q\" to activate and destroy all enemies" +
					" on screen\n Can only have one at a time.\n\n\nCannot purchase.");

				if (GUI.Button (new Rect (120, 150, 150, 100), "OK")) {
					buyNuke = false;
					player.SendMessage ("disableTalking");
				}
			} else {
				GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
			         "NUKE\nCost: $30.\n\n Gives player a nuclear weapon.\nPress \"Q\" to activate and destroy all enemies" +
					" on screen\n Can only have one at a time.\n\n\nWould you like to purchase?");
				
				if (GUI.Button (new Rect (120, 150, 75, 50), "YES")) {
					buyNuke = false;
					SphereMovement.has_nuke = true;
					ScoreManager.money -= 30;
					player.SendMessage ("disableTalking");
					soundSystem.SendMessage ("itemPurchased");
				}
				
				if (GUI.Button (new Rect (200, 150, 75, 50), "NO")) {
					buyNuke = false;
					player.SendMessage ("disableTalking");
				}
			}
			GUI.EndGroup ();
		} else if (buyArmor) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));
			if (SphereMovement.has_invincibility) {
				GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
				         "INVINCIBILITY\nCost: $25.\n\nBy pressing \"E\", the player can get out" +
					" of any sticky situation\nthrough a 3-second invincibility potion.\nOnly" +
					"one at a time.\n\n\nCannot purchase.");
				
				if (GUI.Button (new Rect (120, 150, 150, 100), "OK")) {
					buyArmor = false;
					player.SendMessage ("disableTalking");
				}
			} else {
				GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
				         "INVINCIBILITY\nCost: $25.\n\nBy pressing \"E\", the player can get out" +
					" of any sticky situation\nthrough a 3-second invincibility potion.\nOnly" +
					"one at a time.\n\n\nWould you like to purchase?");
				if (GUI.Button (new Rect (120, 150, 75, 50), "YES")) {
					buyArmor = false;
					SphereMovement.has_invincibility = true;
					ScoreManager.money -= 25;
					player.SendMessage ("disableTalking");
					soundSystem.SendMessage ("itemPurchased");
				}
				
				if (GUI.Button (new Rect (200, 150, 75, 50), "NO")) {
					buyArmor = false;
					player.SendMessage ("disableTalking");
				}
			}
			GUI.EndGroup ();
		} else if (incLifeCapacity) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
			         "HEART CONTAINER\nCost: $250.\n\n Increases the maximum amount of lives" +
				" the player can have.\nCurrent maximum: " + SphereMovement.max_lives + 
				"\n\n\nWould you like to purchase?");
			
			if (GUI.Button (new Rect (120, 150, 75, 50), "YES")) {
				incLifeCapacity = false;
				SphereMovement.max_lives++;
				for (int i = SphereMovement.lives; i < SphereMovement.max_lives; i++)
					player.SendMessage ("GainLife");
				ScoreManager.money -= 250;
				player.SendMessage ("disableTalking");
				soundSystem.SendMessage ("itemPurchased");
			}
			
			if (GUI.Button (new Rect (200, 150, 75, 50), "NO")) {
				buyExtraLife = false;
				player.SendMessage ("disableTalking");
			}
			GUI.EndGroup ();
		} else if (exit_shop) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
			                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
			                          Screen.height / 2));
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
			        "Would you like to exit the shop and\nenter the arena again?");
			
			if (GUI.Button (new Rect (120, 150, 75, 50), "YES")) {
				exit_shop = false;
				Application.LoadLevel ("level 1");
				player.SendMessage ("disableTalking");
			}
			
			if (GUI.Button (new Rect (200, 150, 75, 50), "NO")) {
				exit_shop = false;
				player.transform.position = new Vector3(2.36f, 3.96f, -9.12f);
				player.SendMessage ("disableTalking");
			}
			GUI.EndGroup ();
		}
	}

	void enableInsufficientHearts(){
		insufficientFundsHearts = true;
	}

	void enableInsufficientArmor(){
		insufficientFundsArmor = true;
	}

	void enableInsufficientNuke(){
		insufficientFundsNuke = true;
	}

	void enableInsufficientCap(){
		insufficientFundsCap = true;
	}

	void enableBuyExtraLife(){
		buyExtraLife = true;
	}

	void enableBuyArmor(){
		buyArmor = true;
	}

	void enableBuyNuke(){
		buyNuke = true;
	}

	void enableBuyCap(){
		incLifeCapacity = true;
	}

	void ExitShop(){
		exit_shop = true;
	}
}
