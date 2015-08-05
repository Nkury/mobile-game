using UnityEngine;
using System.Collections;

public class TermiteAttack : MonoBehaviour {

	
	private GameObject targetObj;
	private ParticleSystem explosion;
	private GameObject soundSystem;
	private int interval = 1;
	public GameObject AIManage;
	public bool visible;
	public bool attacking;
	private bool disappear = false;
	private bool entered = true;
	private GameObject MissionAcc;
	private bool flicker = false;
	private Color color;

	void Start(){
		soundSystem = GameObject.Find ("Sound System");
		attacking = false;
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
		interval++;

		if (interval % 7 == 0 && flicker) {
			GetComponent<Renderer> ().material.color = new Color (0, 1, 1);
		} else if (flicker) {
			GetComponent<Renderer> ().material.color = color;
		}

		if (interval % 4 == 0 && disappear) {
			GetComponent<Renderer> ().enabled = true;
		} else if (disappear) {
			GetComponent<Renderer> ().enabled = false;
		}

		if (visible == false && !entered) {
			entered = true;
			GetComponent<Renderer> ().enabled = false;
			StartCoroutine (BeVisible ());
		} else if(visible == true && entered) {
			entered = false;
			GetComponent<Renderer> ().enabled = true;
			StartCoroutine (BeVisible ());
		}
		
		if (attacking == true && visible == true) {
			float distance = Vector3.Distance(targetObj.transform.position, transform.position);
			Vector3 direction = Vector3.Normalize(targetObj.transform.position - transform.position );
			if(distance < 3)
				transform.position += direction * Time.deltaTime * 3.3f;
			else
				transform.position += direction * Time.deltaTime * 2.7f;
			Quaternion rotation = Quaternion.LookRotation(direction);
			transform.rotation = rotation;
		}
	}

	IEnumerator BeVisible(){
		int rand = Random.Range (4, 7);
		yield return new WaitForSeconds (rand);
		if (!entered) {
			disappear = true;
			attacking = false;
			yield return new WaitForSeconds (1.5f);
			disappear = false;
			attacking = true;
		}
		visible = !visible;
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
	
	void DestroySelf(){
		soundSystem.SendMessage ("DeadEnemy");
		targetObj.GetComponent<HealthBar> ().newLimit ();
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
		AIManage.SendMessage ("killed");
		Destroy (expl, 3); 
	}

	IEnumerator NoAttack(){
		yield return new WaitForSeconds (2);
		attacking = true;
	}

	IEnumerator setDeactive(){
		yield return new WaitForSeconds (5);
		MissionAcc.GetComponent<Renderer>().enabled = false;
	}

	
	void Flicker(){
		GetComponent<AudioSource> ().Play ();
		flicker = true;
	}
	
}
