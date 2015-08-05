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
	private bool beatLevel;
	private bool beatScore = false; // for moving score during final results
	private bool beatMoney = false; // for moving money during final results
	private bool toContinue = false; // for flickering the "Click to Continue" label
	private int interval = 1;
	private int prevScore = 0;
	private int prevMoney = 0;

	public GameObject AIManage;
	private GameObject soundplayer;
	private GameObject player;
	private GameObject scoreLabel;
	private GameObject moneyLabel;
	public GameObject finalResults;
	private GameObject FinalRes;
	private GameObject accomplishments;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		scoreLabel = GameObject.Find ("score");
		moneyLabel = GameObject.Find ("money");
		FinalRes = GameObject.Find ("Final Results");
		accomplishments = GameObject.Find ("accomplishments");
		soundplayer = GameObject.Find ("Sound System");
	}

	void Update() {
		interval++;

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

		if (beatScore && scoreLabel.transform.position.x < 3) {
			Vector3 direction = Vector3.Normalize(new Vector3(3,4,-2.7f) - scoreLabel.transform.position);
			scoreLabel.transform.position += direction * Time.deltaTime * 10;
		}

		if (beatLevel) {
			Vector3 direction = Vector3.Normalize(new Vector3(player.transform.position.x
			                                                  ,2,player.transform.position.z) - player.transform.position);
			player.transform.position += direction * Time.deltaTime * 7;
		}

		if (beatMoney && moneyLabel.transform.position.x < 3) {
			Vector3 direction = Vector3.Normalize(new Vector3(3,4,-5.2f) - moneyLabel.transform.position);
			moneyLabel.transform.position += direction * Time.deltaTime * 10;
		}

		if (toContinue && interval %15 == 0) {
			foreach(Transform child in FinalRes.transform){
				if(child.name == "clickToCont")
					child.GetComponent<Renderer> ().enabled = !child.GetComponent<Renderer> ().enabled;
			}
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

	void BeatLevel(){
		soundplayer.SendMessage ("finalResultsMus");
		StartCoroutine(beatenLevel ());
	}

	IEnumerator beatenLevel(){
		finalResults.GetComponent<TextMesh>().text = "Final Results";
		finalResults.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds (2);
		soundplayer.SendMessage ("boomOne");
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "scoreLabel"){
				child.GetComponent<Renderer>().enabled = true;
			}
		}
		beatScore = true;
		yield return new WaitForSeconds (3);
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "+Boss"){
				soundplayer.SendMessage ("boomTwo");
				child.GetComponent<Renderer>().enabled = true;
				StartCoroutine(CountTo(score + 500));
			}
		}
		yield return new WaitForSeconds (4);
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "+Lives"){
				soundplayer.SendMessage ("boomThree");
				child.GetComponent<TextMesh>().text = "+ " + SphereMovement.lives + " Lives " +
					(SphereMovement.lives * 100);
				child.GetComponent<Renderer>().enabled = true;
				StartCoroutine(CountTo(score + (SphereMovement.lives * 100)));
			}
		}
		yield return new WaitForSeconds (4);
		soundplayer.SendMessage ("boomFour");
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "moneyLabel"){
				child.GetComponent<Renderer>().enabled = true;
			}
		}
		beatMoney = true;
		yield return new WaitForSeconds (3);
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "+Boss Money"){
				soundplayer.SendMessage("boomFive");
				child.GetComponent<Renderer>().enabled = true;
				StartCoroutine(CountToMoney(money + 100));
			}
		}
		yield return new WaitForSeconds (4);
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "+Score"){
				soundplayer.SendMessage ("boomSix");
				child.GetComponent<TextMesh>().text = "+ " + score + "/ 100 = " + (score / 100);
				child.GetComponent<Renderer>().enabled = true;
				StartCoroutine(CountToMoney(money + (score / 100)));
			}
		}
		yield return new WaitForSeconds (4);
		soundplayer.SendMessage ("boomSeven");
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "missionLabel"){
				child.GetComponent<Renderer>().enabled = true;
			}
		}
		yield return new WaitForSeconds (1);
		foreach (Transform child in accomplishments.transform) {

			if(child.name == "accomplishment 1"){
				if(AIManager.mission1)
					child.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
				else
					child.GetComponent <Renderer>().material.color = new Color(1,0,0);
				child.GetComponent<Renderer>().enabled = true;
			}

			if(child.name == "accomplishment 2"){
				if(AIManager.mission2)
					child.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
				else
					child.GetComponent <Renderer>().material.color = new Color(1,0,0);
				child.GetComponent<Renderer>().enabled = true;
			}

			if(child.name == "accomplishment 3"){
				if(AIManager.mission3)
					child.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
				else
					child.GetComponent <Renderer>().material.color = new Color(1,0,0);
				child.GetComponent<Renderer>().enabled = true;
			}
		}
		yield return new WaitForSeconds (2);
		toContinue = true;
		foreach (Transform child in FinalRes.transform) {
			if(child.name == "clickToCont"){
				child.GetComponent<Renderer>().enabled = true;
			}
		}
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
		int descent = prevScore - (score % 200);
		if (descent < 0)
			descent = 0;
		dead = true;
		StartCoroutine (CountTo(descent));
		StartCoroutine (CountToMoney (prevMoney / 2));
	}

	public void storeScore(){
		prevScore = score;
		prevMoney = money;
	}

	IEnumerator CountTo (int target) {
		yield return new WaitForSeconds (2);
		soundplayer.SendMessage ("incScore");
		int start = score;
		for (float timer = 0; timer < duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			score = (int)Mathf.Lerp (start, target, progress);
			yield return null;
		}
		soundplayer.SendMessage ("stopScore");
		score = target;
	}

	IEnumerator CountToMoney (int target) {
		yield return new WaitForSeconds (2);
		soundplayer.SendMessage ("incScore");
		int start = money;
		for (float timer = 0; timer < duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			money = (int)Mathf.Lerp (start, target, progress);
			yield return null;
		}
		soundplayer.SendMessage ("stopScore");
		money = target;
	}
}
