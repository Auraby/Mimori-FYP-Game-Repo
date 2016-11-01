using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public float healthbar;
	public Slider healthbarslider;
	public Slider manabarslider;
	public float maxmana;
	public float manabar;
	public float maxhealth;
	public float manaregen;
	public int regentime;

	public Image spell1;
	public Image spell2;
	public Image spell3;
	public Image spell4;
	public float s1cooldown;
	public float s2cooldown;
	public float s3cooldown;
	public float s4cooldown;
	public bool spell1activate;
	public bool spell2activate;
	public bool spell3activate;
	public bool spell4activate;

	public GameObject skilleffect1;
	public GameObject skilleffect2;
	public GameObject skilleffect3;
	public GameObject skilleffect4;

	public Transform player;
	public GameObject player1;

	// Use this for initialization
	void Start () {
		regentime = 2;
		manaregen = 2.0f;
		maxhealth = 100.0f;
		maxmana = 100.0f;
		healthbar = maxhealth;
		manabar = maxmana;
		healthbarslider.value = healthbar;
		manabarslider.value = manabar;
		//InvokeRepeating ("MPRegen", 0.0f, 1.0f / manaregen); 
	
	}

	void MPRegen(){
		if (manabar < maxmana) {
			manabar += manaregen * Time.deltaTime;
			manabarslider.value += manaregen * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy") {
			Debug.Log ("HIT, HIT");
			healthbar -= 20.0f;
			healthbarslider.value -= 20.0f;
		}
	}
		

//	void OnTriggerEnter(Collider other){
//		if (other.gameObject.tag == "Enemy") {
//			Debug.Log ("HIT,HIT");
//			healthbarslider.value = healthbar;
//		}
//	}

	// Update is called once per frame
	void Update () {
		if (manabar < maxmana) {
			manabar += manaregen * Time.deltaTime;
			manabarslider.value += manaregen * Time.deltaTime;
		}

		//SPELL ABILITIES
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			spell1.gameObject.SetActive (true);
			spell1activate = true;
			Instantiate (skilleffect1, new Vector3 (1, 1, 1), Quaternion.identity);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			spell2.gameObject.SetActive (true);
			spell2activate = true;
			Instantiate (skilleffect2, new Vector3 (1, 1, 1), Quaternion.identity);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			spell3.gameObject.SetActive (true);
			spell3activate = true;
			Instantiate (skilleffect3, new Vector3 (1, 1, 1), Quaternion.identity);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			spell4.gameObject.SetActive (true);
			spell4activate = true;
			Instantiate (skilleffect4, new Vector3 (1, 1, 1), Quaternion.identity);
		}
		if (spell1activate == true && spell1.fillAmount <= 0) {
			spell1activate = false;
			spell1.gameObject.SetActive (false);
			spell1.fillAmount = 1.0f;
		}
		if(spell1activate == true){
			spell1.fillAmount -= 0.5f * Time.deltaTime;
		}

		if (spell2activate == true && spell2.fillAmount <= 0) {
			spell2activate = false;
			spell2.gameObject.SetActive (false);
			spell2.fillAmount = 1.0f;
		}
		if(spell2activate == true){
			spell2.fillAmount -= 0.5f * Time.deltaTime;
		}

		if (spell3activate == true && spell3.fillAmount <= 0) {
			spell3activate = false;
			spell3.gameObject.SetActive (false);
			spell3.fillAmount = 1.0f;
		}
		if(spell3activate == true){
			spell3.fillAmount -= 0.5f * Time.deltaTime;
		}

		if (spell4activate == true && spell4.fillAmount <= 0) {
			spell4activate = false;
			spell4.gameObject.SetActive (false);
			spell4.fillAmount = 1.0f;
		}
		if(spell4activate == true){
			spell4.fillAmount -= 0.5f * Time.deltaTime;
		}


		/////
	}




}
