using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {
	// Use this for initialization
	public float gun1bulletcst;
	public float gun2bulletcst;
	public float gun3bulletcst;
	public bool currGunIsAuto;

	[HideInInspector]
	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;

	void Start () {
		gun1bulletcst = 5.0f;
		gun2bulletcst = 20.0f;
		gun3bulletcst = 10.0f;
	}
		

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Shoot");
			gameObject.GetComponent<Health> ().manabarslider.value -= 2.0f;
			gameObject.GetComponent<Health> ().manabar -= 2.0f;

		}

		if (currGunIsAuto) {
			if (Input.GetMouseButton (0)) {
				Debug.Log ("SprayAndPray");
				gameObject.GetComponent<Health> ().manabarslider.value -= gun1bulletcst;
				gameObject.GetComponent<Health> ().manabar -= gun1bulletcst;
			}
		}


		if (Input.GetKeyDown (KeyCode.Q)) {

		}

		if (Input.GetKeyDown (KeyCode.E)) {

		}
	}
}
