using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {
	// Use this for initialization
	public float gun1bulletcst;
	public float gun2bulletcst;
	public float gun3bulletcst;
	public bool currGunIsAuto;
	public bool IsCharging;

	//Weapons
	public GameObject weapon1;
	public Transform weapon1_GunEnd;
	public GameObject weapon2;
	public Transform weapon2_GunEnd;
	public GameObject weapon3;
	public Transform weapon3_GunEnd;
	public int weaponswitch;

	//Weapon Switching
	public Texture2D[] Images;

	//Effects
	public GameObject ChargingEffect;
	public float ChargeFire;
	public float ChargingTime;

	[HideInInspector]
	public GameObject ChargeBullet;

	void Start () {
		weaponswitch = 0;
		ChargeFire = 5.0f;
		ChargingTime = 0f;
		gun1bulletcst = 50.0f;
		gun2bulletcst = 20.0f;
		gun3bulletcst = 10.0f;
	}
		

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Shoot");
			IsCharging = true;
			gameObject.GetComponent<Health> ().manabarslider.value -= 5.0f;
			gameObject.GetComponent<Health> ().manabar -= 5.0f;
		}

		//if (currGunIsAuto) {}
			if (Input.GetMouseButton (0)) {
				//Debug.Log ("Charging");
			if (IsCharging && gameObject.GetComponent<Player>().isIronSight < 1) {
				
//					GameObject ChargeBullet = Instantiate (ChargingEffect, gameObject.GetComponent<Player> ().gunEnd.position, gameObject.GetComponent<Player> ().gunEnd.rotation) as GameObject;
//					ChargeBullet.transform.parent = transform;
//					Destroy (ChargeBullet, 1.0f);
				}
				//ChargingTime += Time.deltaTime;
			}

			

		if (Input.GetMouseButtonUp (0)) {
//			ChargingTime = 0f;
//			Destroy (ChargeBullet, 1.0f);
		}

//		if (ChargingTime >= ChargeFire) {
//			IsCharging = false;
//			Destroy (ChargeBullet, 1.0f);
//			gameObject.GetComponent<Health> ().manabarslider.value -= gun1bulletcst;
//			gameObject.GetComponent<Health> ().manabar -= gun1bulletcst;
//		}
		//Switching the guns 
		switch (weaponswitch) {
		case 0:
			gameObject.GetComponent<Player> ().gun = weapon1;
			gameObject.GetComponent<Player> ().gunEnd = weapon1_GunEnd;

			weapon1.gameObject.SetActive (true);
			weapon1_GunEnd.gameObject.SetActive (true);
			weapon2.gameObject.SetActive (false);
			weapon2_GunEnd.gameObject.SetActive (false);
			weapon3.gameObject.SetActive (false);
			weapon3_GunEnd.gameObject.SetActive (false);
			break;
		case 1:
			gameObject.GetComponent<Player> ().gun = weapon2;
			gameObject.GetComponent<Player> ().gunEnd = weapon2_GunEnd;

			weapon1.gameObject.SetActive (false);
			weapon1_GunEnd.gameObject.SetActive (false);
			weapon2.gameObject.SetActive (true);
			weapon2_GunEnd.gameObject.SetActive (true);
			weapon3.gameObject.SetActive (false);
			weapon3_GunEnd.gameObject.SetActive (false);
			break;
		case 2:
			gameObject.GetComponent<Player> ().gun = weapon3;
			gameObject.GetComponent<Player> ().gunEnd = weapon3_GunEnd;

			weapon1.gameObject.SetActive (false);
			weapon1_GunEnd.gameObject.SetActive (false);
			weapon2.gameObject.SetActive (false);
			weapon2_GunEnd.gameObject.SetActive (false);
			weapon3.gameObject.SetActive (true);
			weapon3_GunEnd.gameObject.SetActive (true);
			break;
		}

		//switch to previous gun
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (weaponswitch > 0) {
				
				weaponswitch--;
			} else {
				weaponswitch = 2;
			}
		}
		//switch to next gun
		if (Input.GetKeyDown (KeyCode.E)) {
			if (weaponswitch < 2) {
				weaponswitch++;
			} else {
				weaponswitch = 0;
			}
		}
	}
}