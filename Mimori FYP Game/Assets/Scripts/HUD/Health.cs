using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public float currentHealth;
	public Slider healthbarslider;
	public Slider manabarslider;
	public float maxmana;
	public float manabar;
	public float maxhealth;
	public float manaregen;
	public int regentime;
	public GameObject questObjective;


	public Transform player;
	public GameObject player1;

    public static Health instance { get; set; }
	// Use this for initialization
	void Start () {
		regentime = 2;
		manaregen = 2.0f;
		maxhealth = 100.0f;
		maxmana = 100.0f;
		currentHealth = maxhealth;
		manabar = maxmana;
		healthbarslider.value = currentHealth;
		manabarslider.value = manabar;
        instance = this;
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
			currentHealth -= 20.0f;
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
		if(Input.GetKeyDown(KeyCode.J)){
			if (!questObjective.activeSelf) {
				questObjective.SetActive (true);
			} else {
				questObjective.SetActive (false);
			}
		}
        healthbarslider.value = currentHealth;

	}




}
