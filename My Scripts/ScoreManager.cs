using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public float duration = 2;

	public static int score = 0;
	public static int money = 0;
	public static int lifeScore;
	private bool check = true;
	private bool start = false;
	private bool inc1 = false;
	private bool dead = false;

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

		if (!dead) {
			if (check && start && inc1) {
				check = false;
				StartCoroutine (Increment1 (AIManage.GetComponent<AIManager> ().num));
			} else if (check && start && !inc1) {
				check = false;
				StartCoroutine (Increment2 (AIManage.GetComponent<AIManager> ().num));
			}
		} else if(dead && scoreLabel.transform.position.x < 1.7) {
			Vector3 direction = Vector3.Normalize(new Vector3(1.9f,4,-3) - scoreLabel.transform.position);
			scoreLabel.GetComponent<TextMesh>().fontSize += 2;
			scoreLabel.transform.position += direction * Time.deltaTime * 10;
		}

		if (dead && moneyLabel.transform.position.x < 1.2) {
			Vector3 direction = Vector3.Normalize(new Vector3(1.4f,4,-5.5f) - moneyLabel.transform.position);
			moneyLabel.GetComponent<TextMesh>().fontSize += 2;
			moneyLabel.transform.position += direction * Time.deltaTime * 10;
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

	void gameOver(){
		int descent = score - ((score % 200) + 200);
		if (descent < 0)
			descent = 0;
		dead = true;
		StartCoroutine (CountTo(descent));
		StartCoroutine (CountToMoney (money / 2));
	}

	IEnumerator CountTo (int target) {
		yield return new WaitForSeconds (2);
		int start = score;
		for (float timer = 0; timer < duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			score = (int)Mathf.Lerp (start, target, progress);
			yield return null;
		}
		score = target;
	}

	IEnumerator CountToMoney (int target) {
		yield return new WaitForSeconds (2);
		int start = money;
		for (float timer = 0; timer < duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			money = (int)Mathf.Lerp (start, target, progress);
			yield return null;
		}
		money = target;
	}
}
