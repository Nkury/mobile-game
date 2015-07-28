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
			float distance = Vector3.Distance(targetObj.transform.position, transform.position);
			Vector3 direction = Vector3.Normalize(targetObj.transform.position - transform.position);
			transform.position += direction * Time.deltaTime * 2.4f;
			Quaternion rotation = Quaternion.LookRotation(direction);
			transform.rotation = rotation;
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

	void DestroySelf(){
		soundSystem.SendMessage ("DeadEnemy");
		targetObj.GetComponent<HealthBar> ().newLimit ();
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
		AIManage.SendMessage ("killed");
		Destroy (expl, 3); 
	}

	void Flicker(){
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
