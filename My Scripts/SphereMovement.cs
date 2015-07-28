using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereMovement : MonoBehaviour {

	public static bool paused = true;
	public float speed;
	public bool can_jump;
	public float gravity;
	public static int lives = 3;
	public static int max_lives = 5;
	private ParticleSystem explosion;
	private GameObject soundSystem;
	private GameObject scoreSystem;
	//private ParticleSystem restartExplosion;
	public GameObject life1;
	public GameObject life2;
	public GameObject life3;
	public GameObject life4;
	public GameObject life5;
	public GameObject lifemultiplier;
	public static bool talking = false;
	public GameObject walls;
	public GameObject scripting;

	private int interval = 1;
	private bool invincible = false;

	public static bool has_nuke = false;
	public static bool has_invincibility = false;
	public GameObject nuke;
	public GameObject invinc;
	public GameObject max_label;
	private GameObject MissionSystem;
	private GameObject AIManage;

	void Start(){
		soundSystem = GameObject.Find ("Sound System");
		scoreSystem = GameObject.Find ("ScoreManager");
		explosion = (ParticleSystem)Resources.Load ("prefabs/player_explosion", typeof(ParticleSystem));
		AIManage = GameObject.Find ("AIManager");
		MissionSystem = GameObject.Find ("MissionSystem");
		//restartExplosion = (ParticleSystem)Resources.Load ("prefabs/restart_explosion", typeof(ParticleSystem));
	}

	void Update(){
		interval += 1;

		if (MissionSystem != null) {
			if (!paused) {
				Time.timeScale = 1;
				MissionSystem.SetActive (false);
			} else {
				Time.timeScale = 0;
				MissionSystem.SetActive (true);
				foreach (Transform child in MissionSystem.transform) {
					if (AIManager.mission1) {
						if (child.name == "mission_obj1") {
							child.GetComponent<Renderer> ().enabled = false;
						}
					}

					if (AIManager.mission2) {
						if (child.name == "mission_obj2") {
							child.GetComponent<Renderer> ().enabled = false;
						}
					}

					if (AIManager.mission3) {
						if (child.name == "mission_obj3") {
							child.GetComponent<Renderer> ().enabled = false;
						}
					}
				}
			}
		}
		if (Input.GetKeyDown("p")){
			paused = !paused;
		}



		if (interval % 3 == 0 && invincible) {
			GetComponent<Renderer> ().enabled = !GetComponent<Renderer> ().enabled;
		} 

		if (has_nuke && nuke != null) {
			nuke.SetActive (true);
			if(Input.GetKeyDown (KeyCode.Q)){
				AIManage.SendMessage("DoNuke");
				has_nuke = false;
			}
		}
		else if(nuke != null)
			nuke.SetActive (false);

		if (has_invincibility && !invincible) {
			invinc.SetActive (true);
			if (Input.GetKeyDown (KeyCode.E)) {
				invincible = true;
				has_invincibility = false;
				StartCoroutine (StopInvincibility ());
			}
		} else if(!has_invincibility)
			invinc.SetActive (false);

		if (walls != null) {
			if (invincible) {
				walls.SetActive (true);
			} else {
				GetComponent<Renderer> ().enabled = true;
				walls.SetActive (false);
			}
		}

		if (lives == 1) {
			life1.SetActive(true);
		} else if (lives == 2) {
			life1.SetActive(true);
			life2.SetActive(true);
		} else if (lives == 3) {
			life1.SetActive(true);
			life2.SetActive(true);
			life3.SetActive(true);
		} else if (lives == 4) {
			life1.SetActive(true);
			life2.SetActive(true);
			life3.SetActive(true);
			life4.SetActive(true);
		} else if (lives == 5) {
			life1.SetActive(true);
			life2.SetActive(true);
			life3.SetActive(true);
			life4.SetActive(true);
			life5.SetActive(true);
		} else if (lives > 5) {
			life1.SetActive(true);
			lifemultiplier.SetActive (true);
			lifemultiplier.GetComponent<Text>().text = "x " + (lives+1);
		}

		
		if(lives == max_lives){
			max_label.SetActive(true);
		}

	}

	void FixedUpdate (){
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float moveUp = Input.GetAxis ("Jump");
		Vector3 movement;

		if (!talking) {
			if (can_jump) 
				movement = new Vector3 (moveHorizontal, moveUp * 26, moveVertical);
			else {
				Vector3 vel = GetComponent<Rigidbody> ().velocity;
				vel.y -= gravity * Time.deltaTime;
				GetComponent<Rigidbody> ().velocity = vel;
				movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
				}

			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);
			}
		}

	void disableJump(){
		can_jump = false;
	}

	void enableJump(){
		can_jump = true;
	}

	void OnCollisionEnter(Collision other){
		if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "danger") && !invincible) {

			if (lives != 1) {
				if (lives == 2) {
					life2.SetActive (false);
				} else if (lives == 3) {
					life3.SetActive (false);
				} else if (lives == 4) {
					life4.SetActive (false);
				} else if (lives == 5) {
					life5.SetActive (false);
				} else if (lives > 5) {
					if (lives == 6) {
						life1.SetActive (true);
						life2.SetActive (true);
						life3.SetActive (true);
						life4.SetActive (true);
						life5.SetActive (true);
						lifemultiplier.SetActive (false);
					} else
						lifemultiplier.GetComponent<Text> ().text = "x " + (lives - 1);
				}

				lives--;
				if (lives < max_lives) {
					max_label.SetActive (false);
				}
				Restart ();
			} else {
				StartCoroutine (GameOver ());
			}
		} else if (other.gameObject.tag == "hit") {
			other.gameObject.transform.parent.SendMessage("DestroySelf");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "abyss trigger") {

			if (lives != 1) {
				if (lives == 2) {
					life2.SetActive (false);
				} else if (lives == 3) {
					life3.SetActive (false);
				} else if (lives == 4) {
					life4.SetActive (false);
				} else if (lives == 5) {
					life5.SetActive (false);
				} else if (lives > 5) {
					if (lives == 6) {
						life1.SetActive (true);
						life2.SetActive (true);
						life3.SetActive (true);
						life4.SetActive (true);
						life5.SetActive (true);
						lifemultiplier.SetActive (false);
					} else
						lifemultiplier.GetComponent<Text> ().text = "x " + (lives - 1);
				}
				
				lives--;
				if (lives < max_lives) {
					max_label.SetActive (false);
				}
				Restart ();
			} else {
				StartCoroutine (GameOver ());
			}
		} else if (other.tag == "coin" || other.tag == "coin_rush") {
			other.SendMessage ("Collect");
		} else if (other.name == "exit_trigger") {
			scripting.SendMessage("ExitShop");
		} else if (scripting != null){
			if(other.tag == "item_heart") {
				if(ScoreManager.money >= 35)
					scripting.SendMessage ("enableBuyExtraLife");
				else
					scripting.SendMessage ("enableInsufficientHearts");
			} else if (other.tag == "item_armor") {
				if(ScoreManager.money >= 25)
					scripting.SendMessage("enableBuyArmor");
				else
					scripting.SendMessage ("enableInsufficientArmor");
			} else if (other.tag == "item_nuke") {
				if(ScoreManager.money >= 30)
					scripting.SendMessage("enableBuyNuke");
				else
					scripting.SendMessage ("enableInsufficientNuke");
			} else if (other.tag == "item_cap") {
				if(ScoreManager.money >= 250)
					scripting.SendMessage("enableBuyCap");
				else
					scripting.SendMessage ("enableInsufficientCap");			
			}
		}
	}

	void Restart(){
		soundSystem.SendMessage ("Ouch");
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		transform.position = new Vector3 (2.36f, 5, -4.69f);
		//Object expl2 = Instantiate (restartExplosion, transform.position, Quaternion.identity);
		Destroy (expl, 3);
	//	Destroy (expl2, 3);
		invincible = true;
		StartCoroutine (StopInvincibility ());
	}

	IEnumerator StopInvincibility(){
				yield return new WaitForSeconds(3);
		invincible = false;
		}

	void GainLife(){
		if(lives == 1){
			life2.SetActive (true);
			lives++;
		} else if(lives == 2){
			life2.SetActive (true);
			life3.SetActive (true);
			lives++;
		} else if (lives == 3){
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			lives++;
		} else if (lives == 4){
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			lives++;
		} else if (lives >= 5 && (lives+1) <= max_lives){
			life2.SetActive (false);
			life3.SetActive (false);
			life4.SetActive (false);
			life5.SetActive (false);
			lifemultiplier.SetActive (true);
			lifemultiplier.GetComponent<Text>().text = "x " + (lives);
			lives++;
		} else if (lives >= 5 && (lives + 1) > max_lives){

		}
	}

	void enableTalking(){
		talking = true;
	}

	void disableTalking(){
		talking = false;
	}

	IEnumerator GameOver(){
		soundSystem.SendMessage ("Ouch");
		scoreSystem.SendMessage ("gameOver");
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		Object expl2 = Instantiate (explosion, transform.position + new Vector3 (.1f, .1f, .1f), Quaternion.identity);
		Object expl3 = Instantiate (explosion, transform.position - new Vector3 (.1f, .1f, .1f), Quaternion.identity);
		Destroy (expl, 3);
		Destroy (expl2, 3);
		Destroy (expl3, 3);
		lives--;
		life1.SetActive (false);
		transform.position = new Vector3 (2.36f, 1000, -4.69f);
		yield return new WaitForSeconds (4);
		Application.LoadLevel ("game over screen");
	}
	
}
