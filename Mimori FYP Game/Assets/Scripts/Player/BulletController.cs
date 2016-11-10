using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private float liveTime = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (liveTime >= 0) {
			liveTime -= Time.deltaTime;
		}

		if (liveTime <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "MeleeMinion") {
			other.GetComponent<MeleeMinionFSM> ().getHit (20);
			Destroy (this.gameObject);
		}
		if (other.gameObject.tag == "RangeMinion") {
			other.GetComponent<RangeMinionFSM> ().getHit (20);
			Destroy (this.gameObject);
		}
	}
}
