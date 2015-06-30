using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour {

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
		Destroy(gameObject); 
	}
}
