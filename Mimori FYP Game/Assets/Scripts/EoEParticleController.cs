using UnityEngine;
using System.Collections;

public class EoEParticleController : MonoBehaviour {
	private float countdown = 4f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (countdown >= 0) {
			countdown -= Time.deltaTime;
		}

		if (countdown <= 0) {
			Destroy (this.gameObject);
		}
	}
}
