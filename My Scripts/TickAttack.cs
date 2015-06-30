using UnityEngine;
using System.Collections;

public class TickAttack : MonoBehaviour {

	private GameObject targetObj;
	private ParticleSystem explosion;
	private GameObject soundSystem;
	public GameObject AIManager;
	public bool visible;
	public bool attacking;
	
	void Start(){
		soundSystem = GameObject.Find ("Sound System");
		attacking = false;
		StartCoroutine (NoAttack ());
		Invoke("DestroySelf", 33);	
	}

	void Awake(){
		targetObj = GameObject.Find ("player");
		AIManager = GameObject.Find ("AIManager");
		explosion = (ParticleSystem)Resources.Load ("prefabs/explosion", typeof(ParticleSystem));
	}

	void Update(){
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
		}
	}

	void DestroySelf(){
		soundSystem.SendMessage ("DeadEnemy");
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
		AIManager.SendMessage ("killed");
		Destroy (expl, 3); 
	}

	IEnumerator NoAttack(){
		yield return new WaitForSeconds (2);
		attacking = true;
	}

}
