using UnityEngine;
using System.Collections;

public class EyeOfEnmarSkillSelf : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "MeleeMinion") {
			Debug.Log ("stun");
			other.gameObject.GetComponent<MeleeMinionFSM> ().getStun (1);
		}
	}
}
