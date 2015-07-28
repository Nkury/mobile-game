using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

	private GameObject targetObj;
	private ParticleSystem explosion;
	private ParticleSystem bigExplosion;
	private GameObject soundSystem;
	public GameObject AIManage;
	public float rotateSpeed = 60f;
	public Material goodTexture;
	public Material badTexture;
	private bool green = false;
	private bool red = true;
	private int lives = 4;
	private bool attacking = true;
	private bool jab = false;
	private bool jab1 = false;
	private bool lunge = false;
	private bool lunge1 = false;
	private Vector3 playerPosition;
	private GameObject missionAcc;

	void Start(){
		soundSystem = GameObject.Find ("Sound System");
		missionAcc = GameObject.Find ("mission_acc");
	}

	void Awake(){
		targetObj = GameObject.Find ("player");
		AIManage = GameObject.Find ("AIManager");
		explosion = (ParticleSystem)Resources.Load ("prefabs/explosion", typeof(ParticleSystem));
		bigExplosion = (ParticleSystem)Resources.Load ("prefabs/boss_explosion", typeof(ParticleSystem));
	}

	void Update(){

		if (red) {
			red = false;
			StartCoroutine(turnGreen());
		}

		if (green) {
			green = false;
			StartCoroutine (turnRed());
		}

		if (jab) {
			attacking = false;
			Vector3 direction = Vector3.Normalize(targetObj.transform.position - transform.position);
			Quaternion rotation = Quaternion.LookRotation(direction);
			transform.rotation = rotation;
			playerPosition = direction;
			if(jab1){
				jab1 = false;
				StartCoroutine (beginToLunge());
			}
		}

		if (lunge) {
			float distance = Vector3.Distance (targetObj.transform.position, transform.position);
			if(lives == 3)
				transform.position += playerPosition * Time.deltaTime * 20;
			if(lives == 2)
				transform.position += playerPosition * Time.deltaTime * 25;
			if(lives == 1)
				transform.position += playerPosition * Time.deltaTime * 30;
			if(lunge1){
				lunge1 = false;
				StartCoroutine (beginToAttack());
			}
		}

		if (attacking) {
			if(lives == 4){
				float distance = Vector3.Distance (targetObj.transform.position, transform.position);
				Vector3 direction = Vector3.Normalize (targetObj.transform.position - transform.position);
				if(transform.position.y > 3.55)
					transform.position -= new Vector3(0, .05f, 0);
				transform.Rotate (0, rotateSpeed * Time.deltaTime, 0, Space.World);
			} else if (lives == 3) {
				float distance = Vector3.Distance (targetObj.transform.position, transform.position);
				Vector3 direction = Vector3.Normalize (targetObj.transform.position - transform.position);
				transform.position += direction * Time.deltaTime * 1.5f;
				transform.Rotate (0, rotateSpeed * Time.deltaTime, 0, Space.World);
			} else if (lives == 2) {
				float distance = Vector3.Distance (targetObj.transform.position, transform.position);
				Vector3 direction = Vector3.Normalize (targetObj.transform.position - transform.position);
				transform.position += direction * Time.deltaTime * 1.8f;
				transform.Rotate (0, rotateSpeed * 1.2f * Time.deltaTime, 0, Space.World);
			} else if (lives == 1) {
				float distance = Vector3.Distance (targetObj.transform.position, transform.position);
				Vector3 direction = Vector3.Normalize (targetObj.transform.position - transform.position);
				transform.position += direction * Time.deltaTime * 2.2f;
				transform.Rotate (0, rotateSpeed * 1.5f * Time.deltaTime, 0, Space.World);
			}
		}
	}

	IEnumerator DestroySelf(){
		targetObj.GetComponent<HealthBar> ().newLimit ();
		if (lives > 1) {
			if(lives == 4)
				soundSystem.SendMessage("startBoss");
			attacking = false;
			transform.position = new Vector3 (2.24f, 11.13f, -5.19f);
			foreach (Transform child in transform) {
				if (child.name != "body") {
					child.GetComponent<Renderer> ().material = badTexture;
					child.tag = "danger";
				}
			}
			soundSystem.SendMessage ("DeadEnemy");
			Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (expl, 3);
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl2 = Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (expl2, 3);
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl3 = Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (expl3, 3);
			lives--;
			attacking = true;
		} else {
			attacking = false;
			soundSystem.SendMessage ("DeadEnemy");
			Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (expl, 3);
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl2 = Instantiate (explosion,(transform.position + new Vector3(.1f, .1f, .1f))
				, Quaternion.identity);
			Destroy (expl2, 3);
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl3 = Instantiate (explosion, (transform.position  - new Vector3(.1f, .1f, .1f))
			    , Quaternion.identity);
			Destroy (expl3, 3);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl4 = Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (expl4, 3);
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl5 = Instantiate (explosion, (transform.position + new Vector3(.1f, .1f, .1f))
			                            , Quaternion.identity);
			Destroy (expl5, 3);
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl6 = Instantiate (explosion, (transform.position - new Vector3(.1f, .1f, .1f))
			                            , Quaternion.identity);
			Destroy (expl6, 3);
			yield return new WaitForSeconds(.25f);
			soundSystem.SendMessage ("DeadEnemy");
			Object expl7 = Instantiate (bigExplosion, transform.position, Quaternion.identity);
			Destroy (expl7, 3);
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			yield return new WaitForSeconds (.25f);
			soundSystem.SendMessage ("DeadEnemy");
			ScoreManager.money += 100;
			ScoreManager.score += 500;
			if(ScoreManager.score >= 2500 && !AIManager.mission1){
				AIManager.mission1 = true;
				missionAcc.GetComponent<TextMesh>().text = "Mission #1 Complete";
				missionAcc.GetComponent<Renderer>().enabled = true;
				StartCoroutine(setDeactive());
				soundSystem.SendMessage("missionComplete");
			}
			Destroy (gameObject);
		}
	}

	IEnumerator turnGreen(){
		int count = 0;
		if (lives == 4) {
			yield return new WaitForSeconds (2);
		} else if (lives == 3) {
			yield return new WaitForSeconds (4);
			jab1 = true;
			jab = true;
			yield return new WaitForSeconds (6);
		} else if (lives == 2) {
			yield return new WaitForSeconds (5);
			jab1 = true;
			jab = true;
			yield return new WaitForSeconds(5);
			jab1 = true;
			jab = true;
			yield return new WaitForSeconds(6);
		} else if (lives == 1) {
			yield return new WaitForSeconds (5);
			jab1 = true;
			jab = true;
			yield return new WaitForSeconds(4);
			jab1 = true;
			jab = true;
			yield return new WaitForSeconds(4);
			jab1 = true;
			jab = true;
			yield return new WaitForSeconds(4);
			jab1 = true;
			jab = true;
			yield return new WaitForSeconds(6);
		}

		int rand = Random.Range (1, 5);
		foreach (Transform child in transform) {
			if(count == rand){
				child.GetComponent<Renderer>().material = goodTexture;
				child.tag = "hit";
			}
			count++;
		}
		green = true;
	}

	IEnumerator turnRed(){
		if (lives == 4)
			yield return new WaitForSeconds (10);
		if(lives == 3)
			yield return new WaitForSeconds (7);
		if (lives == 2)
			yield return new WaitForSeconds (5);
		if (lives == 1)
			yield return new WaitForSeconds (3);
		foreach (Transform child in transform) {
			if(child.name != "body"){
				child.GetComponent<Renderer>().material = badTexture;
				child.tag = "danger";
			}
		}
		red = true;
	}

	IEnumerator beginToLunge(){
		if(lives == 3)
			yield return new WaitForSeconds (2);
		if (lives == 2)
			yield return new WaitForSeconds (1.5f);
		if (lives == 1)
			yield return new WaitForSeconds (1);
		jab = false;

		if (lives == 3)
			yield return new WaitForSeconds (.5f);
		if (lives == 2)
			yield return new WaitForSeconds (.3f);
		if(lives == 1)
			yield return new WaitForSeconds (.15f);
		lunge = true;
		lunge1 = true;
	}
	
	IEnumerator beginToAttack() {
		yield return new WaitForSeconds (.25f);
		lunge = false;
		attacking = true;
	}

	IEnumerator setDeactive(){
		yield return new WaitForSeconds (5);
		missionAcc.GetComponent<Renderer>().enabled = false;
	}
}
