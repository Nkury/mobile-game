using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public float duration = 0.5f;

	public static int score = 0;
	public static int money = 0;
	private static int lifeScore;
	private bool check = true;
	private bool start = false;
	private bool inc1 = false;

	public GameObject AIManage;
	private GameObject player;
	private GameObject scoreLabel;
	private GameObject moneyLabel;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		scoreLabel = GameObject.Find ("score");
		moneyLabel = GameObject.Find ("money");
	}

	void Update() {

		if (lifeScore > 200) {
			lifeScore = lifeScore - 200;
			player.SendMessage("GainLife");
		}

		if (check && start && inc1) {
			check = false;
			StartCoroutine (Increment1 (AIManage.GetComponent<AIManager>().num));
		} else if (check && start && !inc1) {
			check = false;
			StartCoroutine (Increment2 (AIManage.GetComponent<AIManager>().num));
		}

		if(scoreLabel != null)
			scoreLabel.GetComponent<TextMesh> ().text = score.ToString ();

		if (moneyLabel != null)
			moneyLabel.GetComponent<TextMesh> ().text = "$" + money.ToString ();

	}
	
	/*
	void GUIButtonCountTo (int target) {
		if (GUILayout.Button ("Count to " + target)) {
			StopCoroutine ("CountTo");
			StartCoroutine ("CountTo", target);
		}
	}
	*/

	IEnumerator Increment2 (int multiplier ) {
		yield return new WaitForSeconds(1);
		if (multiplier > 0) {
			score += (2 * multiplier);
			lifeScore += (2 * multiplier);
		}
		check = true;
	} 

	IEnumerator Increment1 (int multiplier) {
		yield return new WaitForSeconds(1);
		if (multiplier > 0) {
			score += (1 * multiplier);
			lifeScore += (1 * multiplier);
		}
		check = true;
	} 
	
	void StartScore(){
		start = true;
	}

	void Use1(){
		inc1 = true;
	}

	void Use2(){
		inc1 = false;
	}

	void gainMoney(){
		money++;
	}

}
