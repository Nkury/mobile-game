using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

	private int count = 0;

	// spawnpoints 
	public GameObject sp1;
	public GameObject sp2;
	public GameObject sp3;
	public GameObject sp4;

	// phase 3d texts and their respective booleans
	public GameObject ph1;
	public GameObject ph2;
	public GameObject ph3;
	public GameObject ph4;
	public GameObject ph5;
	public static bool ph1on = true;
	public static bool ph2on = false;
	public static bool ph3on = false;
	public static bool ph4on = false;
	public static bool ph5on = false;

	public GameObject enemyPrefab;
	public GameObject termitePrefab;

	// Safezone variables
	public GameObject safeZonePrefab;
	public GameObject nukePrefab;
	private bool is_safezone = false;

	// keeps track of what phase we are on
	private int phaseCount = 1;
	public static bool start1 = false;
	public static bool start2 = false;
	public static bool start3 = false;
	public static bool start4 = false;
	public static bool start5 = false;

	public int num = 0;
	private int randomChoice;

	private bool wave1start = true;
	private bool makeEnemy = true;

	// User-control of phases variables
	public int Phase1Total; // number of total enemies in phase 1
	public int Phase1Wave1; // number of enemies in wave 1
	public int Phase1Wave2; // number of enemies in wave 2
	public int Phase1Wave3; // number of enemies in wave 3
	public int Phase2Total; // number of total enemies in phase 2
	public int Phase2Wave1; // number of enemies in wave 1
	public int Phase2Wave2; // number of enemies in wave 2
	public int Phase2Wave3; // number of enemies in wave 3
	public int Phase3Total; // number of total enemies in phase 3
	public int Phase3Wave1; // number of enemies in wave 1
	public int Phase3Wave2; // number of enemies in wave 2
	public int Phase3Wave3Ticks; // number of ticks in wave 3
	public int Phase3Wave3Termites; // number of termites in wave 3
	public int Phase4Total; // number of total enemies in phase 4
	public int Phase4Wave1Ticks; // number of ticks in wave 1
	public int Phase4Wave1Termites; // number of termites in wave 1
	public int Phase4Wave2Ticks; // number of ticks in wave 2
	public int Phase4Wave2Termites; // number of termites in wave 2
	public int Phase4Wave3Ticks; // number of ticks in wave 3
	public int Phase4Wave3Termites; // number of termites in wave 3
	public int Phase5Total; // number of total enemies in phase 5
	public int Phase5Wave1Ticks; // number of ticks in wave 1
	public int Phase5Wave1Termites; // number of termites in wave 1
	public int Phase5Wave2Ticks; // number of ticks in wave 2
	public int Phase5Wave2Termites; // number of termites in wave 2
	public int Phase5Wave3Ticks; // number of ticks in wave 3
	public int Phase5Wave3Termites; // number of termites in wave 3

	private bool getCoin = true;
	private bool coin_rush = false;
	public GameObject coinPrefab;
	public GameObject coinRushPrefab;
	private bool makeNewCoinRush = true;

	private bool shop = false;
	private GameObject player;
	private GameObject soundPlayer;
	private GameObject count_tens;
	private GameObject count_ones;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
		soundPlayer = GameObject.Find ("Sound System");
		count_tens = GameObject.Find ("counter_tens");
		count_ones = GameObject.Find ("counter_ones");
	}
	
	// Update is called once per frame
	void Update () {

		if (!is_safezone) {
			is_safezone = true;
			generateSafeZone ();
		}

		generateCoins ();

		if (ph1on && start1) {

			count_tens.GetComponent<TextMesh>().text = (Phase1Total / 10).ToString ();
			count_ones.GetComponent<TextMesh>().text = (Phase1Total % 10).ToString ();

			if(wave1start){
				generateEnemy(Phase1Wave1);
				wave1start = false;
			}

			if(Phase1Total == (Phase1Wave2 + Phase1Wave3) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase1Wave2));
			}

			if(Phase1Total == (Phase1Wave3) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase1Wave3));
			}

			if(Phase1Total == 0){
				ph1on = false;
				ph2on = true;
				shop = true;
			}

		} else if (ph2on && start2) {

			count_tens.GetComponent<TextMesh>().text = (Phase2Total / 10).ToString ();
			count_ones.GetComponent<TextMesh>().text = (Phase2Total % 10).ToString ();

			if(wave1start){
				wave1start = false;
				soundPlayer.SendMessage ("beatPhase1");
				StartCoroutine (setPhase2());
			}

			if(Phase2Total == (Phase2Wave2 + Phase2Wave3) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase2Wave2));
			}
			
			if(Phase2Total == (Phase2Wave3) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase2Wave3));
			}
			
			if(Phase2Total == 0){
				ph2on = false;
				ph3on = true;
				shop = true;
			}

		} else if (ph3on && start3) {

			count_tens.GetComponent<TextMesh>().text = (Phase3Total / 10).ToString ();
			count_ones.GetComponent<TextMesh>().text = (Phase3Total % 10).ToString ();

			if(wave1start){
				wave1start = false;
				soundPlayer.SendMessage ("beatPhase1");
				soundPlayer.SendMessage ("beatPhase2");
				StartCoroutine (setPhase3());
			}
			
			if(Phase3Total == (Phase3Wave2 + Phase3Wave3Ticks + Phase3Wave3Termites) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase3Wave2));
			}
			
			if(Phase3Total == (Phase3Wave3Ticks + Phase3Wave3Termites) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase3Wave3Ticks));
				StartCoroutine (delayNewTermite (Phase3Wave3Termites));
			}
			
			if(Phase3Total == 0){
				ph3on = false;
				ph4on = true;
				shop = true;
			}


		} else if (ph4on && start4) {

			count_tens.GetComponent<TextMesh>().text = (Phase4Total / 10).ToString ();
			count_ones.GetComponent<TextMesh>().text = (Phase4Total % 10).ToString ();

			if(wave1start){
				wave1start = false;
				soundPlayer.SendMessage ("beatPhase1");
				soundPlayer.SendMessage ("beatPhase2");
				soundPlayer.SendMessage ("beatPhase3");
				StartCoroutine (setPhase4());
			}
			
			if(Phase4Total == (Phase4Wave2Ticks + Phase4Wave2Termites
					        + Phase4Wave3Ticks + Phase4Wave3Termites) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase4Wave2Ticks));
				StartCoroutine (delayNewTermite(Phase4Wave2Termites));
			}
			
			if(Phase4Total == (Phase4Wave3Ticks + Phase4Wave3Termites) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase4Wave3Ticks));
				StartCoroutine (delayNewTermite(Phase4Wave3Termites));
			}
			
			if(Phase4Total == 0){
				ph4on = false;
				ph5on = true;
				shop = true;
			}
		} else if (ph5on && start5) {

			count_tens.GetComponent<TextMesh>().text = (Phase5Total / 10).ToString ();
			count_ones.GetComponent<TextMesh>().text = (Phase5Total % 10).ToString ();

			if(wave1start){
				wave1start = false;
				soundPlayer.SendMessage ("beatPhase1");
				soundPlayer.SendMessage ("beatPhase2");
				soundPlayer.SendMessage ("beatPhase3");
				soundPlayer.SendMessage ("beatPhase4");
				StartCoroutine (setPhase5());
			}
			
			if(Phase5Total == (Phase5Wave2Ticks + Phase5Wave2Termites
					            + Phase5Wave3Ticks + Phase5Wave3Termites) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase5Wave2Ticks));
				StartCoroutine(delayNewTermite(Phase5Wave2Termites));
			}
			
			if(Phase5Total == (Phase5Wave3Ticks + Phase5Wave3Termites) && num == 0 && makeEnemy){
				makeEnemy = false;
				StartCoroutine(delayNewEnemy (Phase5Wave3Ticks));
				StartCoroutine(delayNewTermite(Phase5Wave3Termites));
			}
			
			if(Phase5Total == 0){
				ph5on = false;
				shop = true;
			}
		}

	}

	void generateEnemy(int numberOfEnemies){

		for (int i = 0; i < numberOfEnemies; i++) {
			num += 1;
			// spawning enemy at a random spawnpoint
			randomChoice = Random.Range (0, 4);

			if (randomChoice == 0) {
				float xpos = sp1.transform.position.x + Random.Range (-3, 5);
				Instantiate (enemyPrefab, new Vector3 (xpos, 4, sp1.transform.position.z), Quaternion.identity);
			} else if (randomChoice == 1) {
				float xpos = sp2.transform.position.x + Random.Range (-3, 5);
				Instantiate (enemyPrefab, new Vector3 (xpos, 4, sp2.transform.position.z), Quaternion.identity);
			} else if (randomChoice == 2) {
				float zpos = sp3.transform.position.z + Random.Range (-4, 5);
				Instantiate (enemyPrefab, new Vector3 (sp3.transform.position.x, 4, zpos), Quaternion.identity);
			} else {
				float zpos = sp4.transform.position.z + Random.Range (-4, 5);
				Instantiate (enemyPrefab, new Vector3 (sp4.transform.position.x, 4, zpos), Quaternion.identity);
			}

		}
	}

	void generateTermite(int numberOfEnemies){
		
		for (int i = 0; i < numberOfEnemies; i++) {
			num += 1;
			// spawning enemy at a random spawnpoint
			randomChoice = Random.Range (0, 4);
			
			if (randomChoice == 0) {
				float xpos = sp1.transform.position.x + Random.Range (-3, 5);
				Instantiate (termitePrefab, new Vector3 (xpos, 4, sp1.transform.position.z), Quaternion.identity);
			} else if (randomChoice == 1) {
				float xpos = sp2.transform.position.x + Random.Range (-3, 5);
				Instantiate (termitePrefab, new Vector3 (xpos, 4, sp2.transform.position.z), Quaternion.identity);
			} else if (randomChoice == 2) {
				float zpos = sp3.transform.position.z + Random.Range (-4, 5);
				Instantiate (termitePrefab, new Vector3 (sp3.transform.position.x, 4, zpos), Quaternion.identity);
			} else {
				float zpos = sp4.transform.position.z + Random.Range (-4, 5);
				Instantiate (termitePrefab, new Vector3 (sp4.transform.position.x, 4, zpos), Quaternion.identity);
			}
			
		}
	}

	void killed(){
		num -= 1;

		if (ph1on) {
			Phase1Total--;
		} else if (ph2on) {
			Phase2Total--;
		} else if (ph3on) {
			Phase3Total--;
		} else if (ph4on) {
			Phase4Total--;
		} else if (ph5on) {
			Phase5Total--;
		}
	}

	IEnumerator setPhase2(){
		yield return new WaitForSeconds(2);
		ph1.SetActive (false);
		ph2.SetActive (true);
		generateEnemy (Phase2Wave1);
	}

	IEnumerator setPhase3(){
		yield return new WaitForSeconds(2);
		ph1.SetActive (false);
		ph2.SetActive (false);
		ph3.SetActive (true);
		generateEnemy (Phase3Wave1-1);
		yield return new WaitForSeconds (8);
		generateEnemy (1);
	}

	IEnumerator setPhase4(){
		yield return new WaitForSeconds(2);
		ph1.SetActive (false);
		ph2.SetActive (false);
		ph3.SetActive (false);
		ph4.SetActive (true);
		generateEnemy (Phase4Wave1Ticks);
		generateTermite (Phase4Wave1Termites);
	}

	IEnumerator setPhase5(){
		yield return new WaitForSeconds(2);
		ph1.SetActive (false);
		ph2.SetActive (false);
		ph3.SetActive (false);
		ph4.SetActive (false);
		ph5.SetActive (true);
		generateEnemy (Phase5Wave1Ticks);
		generateTermite (Phase5Wave1Termites);
	}

	void StartPhase(){

		if (phaseCount == 1) {
			start1 = true;
			is_safezone = false;
		} else if (phaseCount == 2) {
			start2 = true;
			is_safezone = false;
		} else if (phaseCount == 3) {
			start3 = true;
			is_safezone = false;
		} else if (phaseCount == 4) {
			start4 = true;
			is_safezone = false;
		} else if (phaseCount == 5) {
			start5 = true;
			is_safezone = false;
		}

		phaseCount++;
	}

	void generateSafeZone(){
		count++;
		if (!wave1start && count >= 6 && !shop) {
			float zpos = -5.2f + Random.Range (-2, 2);
			float xpos = 2.4f + Random.Range (-2, 2);
			Instantiate (safeZonePrefab, new Vector3(xpos, 3, zpos), Quaternion.identity);
			StartCoroutine(newSafeZone ());
		}
	}

	void generateCoins(){
		int rand = 1;

		if(makeNewCoinRush)
			rand = Random.Range (0, 100);

		if (!wave1start && getCoin && !shop) {
			getCoin = false;
			float xpos = 2.1f + Random.Range (-3, 3);
			float zpos = -4.3f + Random.Range (-4, 3);
			if(rand % 10 == 0){
				Instantiate (coinRushPrefab, new Vector3(xpos, 4.35f, zpos), coinRushPrefab.transform.rotation);
				makeNewCoinRush = false;
				StartCoroutine(newCoinRush ());
			}
			else 
				Instantiate (coinPrefab, new Vector3 (xpos, 3.65f, zpos), coinPrefab.transform.rotation);

			if(coin_rush)
				StartCoroutine(coinRush ());
			else 
				StartCoroutine (newCoin ());
		}
	}

	IEnumerator newCoin(){
			yield return new WaitForSeconds(1.5f);
			getCoin = true;
	}

	IEnumerator coinRush(){
		yield return new WaitForSeconds (.25f);
		getCoin = true;
	}

	IEnumerator newCoinRush(){
		yield return new WaitForSeconds (60);
		makeNewCoinRush = true;
	}

	IEnumerator newSafeZone(){
		yield return new WaitForSeconds(40);
		is_safezone = false;
	}

	IEnumerator delayNewEnemy(int numberOfEnemies){
		yield return new WaitForSeconds(2);
		generateEnemy (numberOfEnemies);
		makeEnemy = true;
	}

	IEnumerator delayNewTermite(int numberOfEnemies){
		yield return new WaitForSeconds(2);
		generateTermite (numberOfEnemies);
		makeEnemy = true;	
	}

	void DoNuke(){
		GameObject sz = (GameObject)Instantiate (nukePrefab, new Vector3 (2.4f, 2.4f, -5.2f), Quaternion.identity);
		StartCoroutine (destroyNuke (sz));
	}

	IEnumerator destroyNuke(GameObject nk){
		yield return new WaitForSeconds (1);
		Destroy (nk);
	}

	void toggleCoinRush(){
		coin_rush = !coin_rush;
	}

	void OnGUI(){
		if (shop) {
			player.SendMessage ("enableTalking");
			GUI.BeginGroup (new Rect ((Screen.width / 2) - Screen.width / 4,
		                          (Screen.height / 2) - Screen.height / 4, Screen.width / 2,
		                          Screen.height / 2));
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2),
		         "You finished a phase of enemies!\nWould you like to go to the shop?");
		
			if (GUI.Button (new Rect (120, 150, 75, 50), "YES")) {
				player.SendMessage ("disableTalking");
				Application.LoadLevel ("shop");
			}
		
			if (GUI.Button (new Rect (200, 150, 75, 50), "NO")) {
				wave1start = true;
				player.SendMessage ("disableTalking");
				shop = false;
			}
			GUI.EndGroup ();
		}
	}
}
