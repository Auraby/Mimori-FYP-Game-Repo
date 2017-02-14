using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	//Gun Mods
	public Image EnmarMod;
	public Image ZoltranMod;
	public Image IshiraMod;
	public bool EnmarModTaken = false;
	public bool ZoltranModTaken = false;
	public bool IshiraModTaken = false;

	public GameObject[] GunModSlots = new GameObject[3];

	public GameObject CurrMod;
	public GameObject PrevMod;
	public GameObject NextMod;
	public int currModAmt;
	public Image[] GunMods = new Image[3];
	public int gunmodcounter;
	public int Eupdatecounter;
	public int Zupdatecounter;
	public int Iupdatecounter;
	[HideInInspector]
	public GameObject ChargeBullet;

	void Start () {
		weaponswitch = 0;
		ChargeFire = 5.0f;
		ChargingTime = 0f;
		gun1bulletcst = 50.0f;
		gun2bulletcst = 20.0f;
		gun3bulletcst = 10.0f;

		gunmodcounter = 0;
        //check weaponswitch
        if (SceneManager.GetActiveScene().name == "Mimori") {
            EnmarModTaken = true;
        }
        if (SceneManager.GetActiveScene().name == "Forest of Misery")
        {
            EnmarModTaken = true;
        }
        if (SceneManager.GetActiveScene().name == "Temple of Aphelion") {
            EnmarModTaken = true;
            ZoltranModTaken = true;
        }
        if (SceneManager.GetActiveScene().name == "Lost Crater")
        {
            EnmarModTaken = true;
            ZoltranModTaken = true;
            IshiraModTaken = true;
        }
        //IshiraModTaken = true;

        Eupdatecounter = 1;
		Zupdatecounter = 1;
		Iupdatecounter = 1;

	}
		

	// Update is called once per frame
	void Update () {
        Debug.Log(gunmodcounter);
        if (GameController.gameController.enmarAbsorbed) {
            EnmarModTaken = true;
        }
        if (GameController.gameController.zoltranAbsorbed)
        {
            ZoltranModTaken = true;
        }
        if (GameController.gameController.ishiraAbsorbed)
        {
            IshiraModTaken = true;
        }
        //Check Counter If Exceeded
        if (gunmodcounter > 2) {
			gunmodcounter = 0;
		}
		if (gunmodcounter < 0) {
			gunmodcounter = 2;
		}

		if (gunmodcounter == 0 && EnmarModTaken == true) {
			gameObject.GetComponent<GunModSkills> ().EyeOfEnmar ();
		} else if (gunmodcounter == 1 && ZoltranModTaken == true) {
			gameObject.GetComponent<GunModSkills> ().SoulOfZoltran ();
		}else if (gunmodcounter == 2 && IshiraModTaken == true)
        {
            gameObject.GetComponent<GunModSkills>().SoulOfZoltran();
        }

        if (EnmarModTaken == true && Eupdatecounter == 1) {
			EnmarMod.gameObject.SetActive (true);
			currModAmt = 1;
			Eupdatecounter = 0;
		}
		if (ZoltranModTaken == true && Zupdatecounter == 1) {
			ZoltranMod.gameObject.SetActive (true);
			currModAmt = 2;
			Zupdatecounter = 0;
		}
		if (IshiraModTaken == true && Iupdatecounter == 1) {
			IshiraMod.gameObject.SetActive (true);
			currModAmt = 3;
			Iupdatecounter = 0;
		}

		//next weapon
		if (Input.GetKeyDown (KeyCode.Q)) {
				if (currModAmt == 1) {
					gunmodcounter = 0;
				} else if (currModAmt == 2) {
					gunmodcounter++;
					if (gunmodcounter > 1) {
						gunmodcounter = 0;
					} 
					if (gunmodcounter < 0) {
						gunmodcounter = 1;
					}
				} else {
					gunmodcounter++;
					if (gunmodcounter > 2) {
						gunmodcounter = 0;
					}
					if (gunmodcounter < 0) {
						gunmodcounter = 2;
					}
				}
						
		}
		//previous weapon
		if (Input.GetKeyDown (KeyCode.E)) {
				if (currModAmt == 1) {
					gunmodcounter = 0;

				} else if (currModAmt == 2) {
					gunmodcounter--;
					if (gunmodcounter > 1) {
						gunmodcounter = 0;
					} 
					if (gunmodcounter < 0) {
						gunmodcounter = 1;
					}
			
				} else {
					gunmodcounter--;
					if (gunmodcounter > 2) {
						gunmodcounter = 0;
					}
					if (gunmodcounter < 0) {
						gunmodcounter = 2;
					}
			
				}


		}
			
			
				
		switch (gunmodcounter) {
		case 0: 
			if (EnmarMod != null && EnmarModTaken == true) {
				EnmarMod.gameObject.transform.SetParent (CurrMod.transform);
				EnmarMod.gameObject.transform.position = CurrMod.gameObject.transform.position;
				EnmarMod.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);

			}
			if (ZoltranMod != null && ZoltranModTaken == true) {
				ZoltranMod.gameObject.transform.SetParent (NextMod.transform);
				ZoltranMod.gameObject.transform.position = NextMod.gameObject.transform.position;
				ZoltranMod.gameObject.transform.localScale = new Vector3 ( 1.0f, 1.0f, 1.0f);
			}
			if (IshiraMod != null && IshiraModTaken == true) {
				IshiraMod.gameObject.transform.SetParent (PrevMod.transform);
				IshiraMod.gameObject.transform.position = PrevMod.gameObject.transform.position;
				IshiraMod.gameObject.transform.localScale = new Vector3 ( 1.0f, 1.0f, 1.0f);
			}
			break;
		case 1:
			if (ZoltranMod != null && ZoltranModTaken == true) {
				ZoltranMod.gameObject.transform.SetParent (CurrMod.transform);
				ZoltranMod.gameObject.transform.position = CurrMod.gameObject.transform.position;
				ZoltranMod.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			if (IshiraMod != null && IshiraModTaken == true) {
				IshiraMod.gameObject.transform.SetParent (NextMod.transform);
				IshiraMod.gameObject.transform.position = NextMod.gameObject.transform.position;
				IshiraMod.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			if(EnmarMod != null && EnmarModTaken == true){
			EnmarMod.gameObject.transform.SetParent (PrevMod.transform);
				EnmarMod.gameObject.transform.position = PrevMod.gameObject.transform.position;
				EnmarMod.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
				break;
		case 2:
			if (IshiraMod != null && IshiraModTaken == true) {
				IshiraMod.gameObject.transform.SetParent (CurrMod.transform);
				IshiraMod.gameObject.transform.position = CurrMod.gameObject.transform.position;
				IshiraMod.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			if (EnmarMod != null && EnmarModTaken == true) {
				
				EnmarMod.gameObject.transform.SetParent (NextMod.transform);
				EnmarMod.gameObject.transform.position = NextMod.gameObject.transform.position;
				EnmarMod.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			if (ZoltranMod != null && ZoltranModTaken == true) {
				ZoltranMod.gameObject.transform.SetParent (PrevMod.transform);
				ZoltranMod.gameObject.transform.position = PrevMod.gameObject.transform.position;
				ZoltranMod.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			break;
		default:
			break;

		}


//		if (ChargingTime >= ChargeFire) {
//			IsCharging = false;
//			Destroy (ChargeBullet, 1.0f);
//			gameObject.GetComponent<Health> ().manabarslider.value -= gun1bulletcst;
//			gameObject.GetComponent<Health> ().manabar -= gun1bulletcst;
//		}
		//Switching the guns 
//		switch (weaponswitch) {
//		case 0:
//			gameObject.GetComponent<Player> ().gun = weapon1;
//			gameObject.GetComponent<Player> ().gunEnd = weapon1_GunEnd;
//
//			weapon1.gameObject.SetActive (true);
//			weapon1_GunEnd.gameObject.SetActive (true);
//			weapon2.gameObject.SetActive (false);
//			weapon2_GunEnd.gameObject.SetActive (false);
//			weapon3.gameObject.SetActive (false);
//			weapon3_GunEnd.gameObject.SetActive (false);
//			break;
//		case 1:
//			gameObject.GetComponent<Player> ().gun = weapon2;
//			gameObject.GetComponent<Player> ().gunEnd = weapon2_GunEnd;
//
//			weapon1.gameObject.SetActive (false);
//			weapon1_GunEnd.gameObject.SetActive (false);
//			weapon2.gameObject.SetActive (true);
//			weapon2_GunEnd.gameObject.SetActive (true);
//			weapon3.gameObject.SetActive (false);
//			weapon3_GunEnd.gameObject.SetActive (false);
//			break;
//		case 2:
//			gameObject.GetComponent<Player> ().gun = weapon3;
//			gameObject.GetComponent<Player> ().gunEnd = weapon3_GunEnd;
//
//			weapon1.gameObject.SetActive (false);
//			weapon1_GunEnd.gameObject.SetActive (false);
//			weapon2.gameObject.SetActive (false);
//			weapon2_GunEnd.gameObject.SetActive (false);
//			weapon3.gameObject.SetActive (true);
//			weapon3_GunEnd.gameObject.SetActive (true);
//			break;
//		}

		//switch to previous gun
		/*if (Input.GetKeyDown (KeyCode.Q)) {
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
		}*/
	}
}