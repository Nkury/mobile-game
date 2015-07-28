using UnityEngine;
using System.Collections;

public class SafeZone : MonoBehaviour {
		
	public GameObject scoreManager;
	public GameObject sp1;
	public GameObject sp2;
	public GameObject sp3;
	public GameObject sp4;

	private ParticleSystem explosion;
	private int i;
	private float rotateSpeed = 60f;
	private bool startingUp = true;
	private bool startShrink = false;
	private int interval = 0;
	private int randomChoice;

	void Start(){
		sp1 = GameObject.FindGameObjectWithTag ("Spawn1");
		sp2 = GameObject.FindGameObjectWithTag ("Spawn2");
		sp3 = GameObject.FindGameObjectWithTag ("Spawn3");
		sp4 = GameObject.FindGameObjectWithTag ("Spawn4");
		explosion = (ParticleSystem)Resources.Load ("prefabs/safezone_explosion", typeof(ParticleSystem));
		scoreManager = GameObject.FindGameObjectWithTag ("Score");
		StartCoroutine (BeginTime ());
		StartCoroutine (StartShrink ());
	}

	void Update(){
		interval += 1;

		if (interval % 3 == 0 && startingUp) {
			GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
		} else if (!startingUp) {
			GetComponent<Renderer>().enabled = true;
		}

		transform.Rotate (0, rotateSpeed * Time.deltaTime, 0, Space.World);

		if (transform.localScale.x != 0 && transform.localScale.y != 0 && transform.localScale.z != 0 && 
		    !startingUp && startShrink && !SphereMovement.paused)
			transform.localScale = new Vector3 (transform.localScale.x - .019f, transform.localScale.y, 
		                                   transform.localScale.z - .019f);
		else
			StartCoroutine(Eliminate ());
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "player" && !startingUp) {
			scoreManager.SendMessage ("Use1");
			GameObject[] eCollection = GameObject.FindGameObjectsWithTag("Enemy");
			for(i = 0; i < eCollection.Length; i++){
				if(eCollection[i].name == "tick(Clone)"){
					TickAttack AIScript = eCollection [i].GetComponent<TickAttack> ();
					if (AIScript.attacking == true)
						AIScript.attacking = false;
				}
				else{
					TermiteAttack AIScript = eCollection[i].GetComponent<TermiteAttack>();
					if (AIScript.attacking == true)
						AIScript.attacking = false;
				}
			}
		}
		else if (other.gameObject.tag == "Enemy" && !startingUp) {
			//other.SendMessage("DestroySelf");
		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.name == "player" && !startingUp) {
			scoreManager.SendMessage ("Use1");
			GameObject[] eCollection = GameObject.FindGameObjectsWithTag ("Enemy");
			for (i = 0; i < eCollection.Length; i++) {
				if(eCollection[i].name == "tick(Clone)"){
					TickAttack AIScript = eCollection [i].GetComponent<TickAttack> ();
					if (AIScript.attacking == true)
						AIScript.attacking = false;
				}
				else{
					TermiteAttack AIScript = eCollection[i].GetComponent<TermiteAttack>();
					if (AIScript.attacking == true)
						AIScript.attacking = false;
				}
			}
		} else if (other.gameObject.tag == "Enemy" && !startingUp) {
			other.SendMessage("DestroySelf");
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name == "player" && !startingUp) {
			scoreManager.SendMessage ("Use2");
			GameObject[] eCollection = GameObject.FindGameObjectsWithTag("Enemy");
			for(i = 0; i < eCollection.Length; i++){
				if(eCollection[i].name == "tick(Clone)"){
					TickAttack AIScript = eCollection [i].GetComponent<TickAttack> ();
					AIScript.attacking = true;
				}
				else{
					TermiteAttack AIScript = eCollection[i].GetComponent<TermiteAttack>();
					AIScript.attacking = true;
				}
			}
		}
	}

	IEnumerator BeginTime(){
		yield return new WaitForSeconds (2);
		startingUp = false;
	}

	IEnumerator StartShrink(){
		yield return new WaitForSeconds (5);
		startShrink = true;
	}

	/*
	IEnumerator TurnYellow(){
		yield return new WaitForSeconds (4);
		GetComponent<Renderer> ().material.color = new Color (1, .5f, 0);
		GetComponent<ParticleSystem> ().startColor = new Color (1, .5f, 0);
	}

	IEnumerator TurnRed(){
		yield return new WaitForSeconds (6);
		GetComponent<Renderer> ().material.color = new Color (1, 0, 0);
		GetComponent<ParticleSystem> ().startColor = new Color (1, 0, 0);
	} */

	IEnumerator Eliminate(){
		yield return new WaitForSeconds(10);
		GameObject[] eCollection = GameObject.FindGameObjectsWithTag("Enemy");
		for (i = 0; i < eCollection.Length; i++) {
			if(eCollection[i].name == "tick(Clone)"){
				TickAttack AIScript = eCollection [i].GetComponent<TickAttack> ();
				AIScript.attacking = true;
			}
			else{
				TermiteAttack AIScript = eCollection[i].GetComponent<TermiteAttack>();
				AIScript.attacking = true;
			}
		}

		scoreManager.SendMessage ("Use2");
		Object expl = Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
		Destroy (expl, 3);
	}

}
