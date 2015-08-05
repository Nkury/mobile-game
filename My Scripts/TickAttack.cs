using UnityEngine;
using System.Collections;

public class TickAttack : MonoBehaviour {

	private GameObject targetObj;
	private ParticleSystem explosion;
	private GameObject soundSystem;
	public GameObject AIManage;
	public bool visible;
	public bool attacking;
	private GameObject MissionAcc;
	private bool flicker = false;
	private int interval = 1;
	private Color color;
	private Vector3 playerPrevPos;

	private bool dash = true;
	private bool dashTime = true;
	private bool goToCenter = false;

	void Start(){
		soundSystem = GameObject.Find ("Sound System");
		attacking = false;
		color = GetComponent<Renderer> ().material.color;
		StartCoroutine (NoAttack ());
		Invoke ("Flicker", 30);
		Invoke("DestroySelf", 33);	
	}

	void Awake(){
		targetObj = GameObject.Find ("player");
		AIManage = GameObject.Find ("AIManager");
		explosion = (ParticleSystem)Resources.Load ("prefabs/explosion", typeof(ParticleSystem));
		MissionAcc = GameObject.Find ("mission_acc");
	}

	void Update(){
		interval += 1;
		
		if (interval % 7 == 0 && flicker) {
			GetComponent<Renderer> ().material.color = new Color (0, 1, 1);
		} else if (flicker) {
			GetComponent<Renderer> ().material.color = color;
		}
	
		if (visible == false)
			GetComponent<Renderer>().enabled = false;
		else
			GetComponent<Renderer>().enabled = true;
	
		if (attacking == true && visible == true) {
			if (goToCenter) {

				if(dashTime){
					dashTime = false;
					GetComponent<Renderer>().material.color = new Color(1, 1, 0);
				}

				if(!((transform.position.x < (playerPrevPos.x + .05f) && transform.position.x > (playerPrevPos.x - .05f))
				   || (transform.position.z < (playerPrevPos.z + .1f) && transform.position.z > (playerPrevPos.z - .1f)))){
					Vector3 direction = Vector3.Normalize (new Vector3(playerPrevPos.x, transform.position.y,
					                                                   playerPrevPos.z) - transform.position);
					transform.position += direction * Time.deltaTime * 5;
				}
				else{
					goToCenter = false;
					dashTime = true;
				}
			} else {
				float distance = Vector3.Distance (targetObj.transform.position, transform.position);
				Vector3 direction = Vector3.Normalize (targetObj.transform.position - transform.position);
				Quaternion rotation = Quaternion.LookRotation (direction);
				transform.rotation = rotation;
				if (dash) {
					transform.position += direction * Time.deltaTime * 2.7f;
					if (dashTime) {
						dashTime = false;
						GetComponent<Renderer> ().material.color = color;
						StartCoroutine (Dash ());
					}			
				} else {
					transform.position += direction * Time.deltaTime * 2;
					if (!dashTime) {
						dashTime = true;
						GetComponent<Renderer> ().material.color = new Color (1, .5f, 0);
						StartCoroutine (Dash ());
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "abyss trigger") {
			DestroySelf();
			if(!AIManager.mission2){
				AIManager.mission2 = true;
				MissionAcc.GetComponent<TextMesh>().text = "Mission #2 Complete";
				MissionAcc.GetComponent<Renderer>().enabled = true;
				StartCoroutine(setDeactive());
				soundSystem.SendMessage ("missionComplete");
			}
		}
	}

	IEnumerator Dash(){
		int rand = Random.Range (3, 10);
		if (!dashTime)
			yield return new WaitForSeconds (rand);
		else {
			yield return new WaitForSeconds (rand);
			playerPrevPos = targetObj.transform.position;
			goToCenter = true;
		}
		dash = !dash;
	}

	void DestroySelf(){
		soundSystem.SendMessage ("DeadEnemy");
		targetObj.GetComponent<HealthBar> ().newLimit ();
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
		AIManage.SendMessage ("killed");
		Destroy (expl, 3); 
	}

	void Flicker(){
		GetComponent<AudioSource> ().Play ();
		flicker = true;
	}
	
	IEnumerator NoAttack(){
		yield return new WaitForSeconds (2);
		attacking = true;
	}

	IEnumerator setDeactive(){
		yield return new WaitForSeconds (5);
		MissionAcc.GetComponent<Renderer>().enabled = false;
	}
}
