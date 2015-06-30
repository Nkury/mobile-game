using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

	private GameObject targetObj;
	private ParticleSystem explosion;
	private GameObject soundSystem;
	public GameObject AIManager;
	public bool attacking = true;
	public float rotateSpeed = 60f;
	public Material goodTexture;
	public Material badTexture;

	void Start(){
		soundSystem = GameObject.Find ("Sound System");
	}

	void Awake(){
		targetObj = GameObject.Find ("player");
		AIManager = GameObject.Find ("AIManager");
		explosion = (ParticleSystem)Resources.Load ("prefabs/explosion", typeof(ParticleSystem));
	}

	void Update(){
		bumpers ();

		if (attacking == true) {
			float distance = Vector3.Distance(targetObj.transform.position, transform.position);
			Vector3 direction = Vector3.Normalize(targetObj.transform.position - transform.position);
			transform.position += direction * Time.deltaTime * 1.5f;
			transform.Rotate (0, rotateSpeed * Time.deltaTime, 0, Space.World);
		}
	}

	void DestroySelf(){
		soundSystem.SendMessage ("DeadEnemy");
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
		Destroy (expl, 3); 
	}

	void bumpers(){
		foreach (Transform child in transform) {
			if(child.name != "body"){
				if(child.GetComponent<Renderer>().material.name == badTexture.name)
					child.GetComponent<Renderer>().material = goodTexture;
				else
					child.GetComponent<Renderer>().material = badTexture;
			}

		}
	}
}
