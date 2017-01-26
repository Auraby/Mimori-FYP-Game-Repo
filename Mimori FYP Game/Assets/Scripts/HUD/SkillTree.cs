using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class SkillTree : MonoBehaviour {

	//EventSystem
	public EventSystem eventsys;

	//General Tree
	float incHealth;
	float incMana;

	//Player Tree
	//SkillImages
	public GameObject HealthActive1;
	public GameObject HealthPassive2;
	public GameObject HealthPassive1;

	private int HealthUpAmt;
	private int ManaUpAmt;
	public GameObject HealthUp;
	public GameObject ManaUp;
	public Button HealthUp_Btn;
	public Button ManaUp_Btn;

	public GameObject ManaActive1;
	public GameObject ManaPassive2;
	public GameObject ManaPassive1;
	//Buttons
	public Button HealthActive1_Btn;
	public Button HealthPassive2_Btn;
	public Button HealthPassive1_Btn;

	public Button ManaActive1_Btn;
	public Button ManaPassive2_Btn;
	public Button ManaPassive1_Btn;

	//Weapon Tree
	//SkillImages
	public GameObject PowerActive1;
	public GameObject PowerActive2;
	public GameObject PowerPassive1;

	public GameObject SpeedActive1;
	public GameObject SpeedActive2;
	public GameObject SpeedPassive1;
	//Buttons
	public Button PowerActive1_Btn;
	public Button PowerActive2_Btn;
	public Button PowerPassive1_Btn;

	public Button SpeedActive1_Btn;
	public Button SpeedActive2_Btn;
	public Button SpeedPassive1_Btn;

	//Execute Spells/Cooldowns
	public Image spell1;
	public Image spell2;
	public Image spell3;
	public Image spell4;
	//Spell Cooldown
	public float s1cooldown;
	public float s2cooldown;
	public float s3cooldown;
	public float s4cooldown;
	public bool spell1activate;
	public bool spell2activate;
	public bool spell3activate;
	public bool spell4activate;

	//Spell Effects prefab 
	public GameObject skilleffect1;
	public GameObject skilleffect2;
	public GameObject skilleffect3;
	public GameObject skilleffect4;

	//Temp Spell Storing
	private GameObject Skillspelleffect1;
	private GameObject Skillspelleffect2;
	private GameObject Skillspelleffect3;
	private GameObject Skillspelleffect4;


	//Stats
	public float incSpeed;
	//public Toggle ChargeShot;
	public int checkSkillPoint;
	public Text SkillPointsTXT;
	//public GameObject ErrorMessageTXT;

	//unlocking
	public bool unlockHealthUp;
	public bool unlockManaUp;

	public bool unlockLifeActive1;
	public bool unlockLifePassive2;
	public bool unlockLifePassive1;

	public bool unlockManaActive1;
	public bool unlockManaPassive2;
	public bool unlockManaPassive1;

	public bool unlockPowerActive1;
	public bool unlockPowerActive2;
	public bool unlockPowerPassive1;

	public bool unlockSpeedActive1;
	public bool unlockSpeedActive2;
	public bool unlockSpeedPassive1;

	public bool disableContSkillP;

	public GameObject playerobj;

	Slot slot;

	// Use this for initialization
	void Start () {
		checkSkillPoint = 10;

	}



	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			//Display SkillTree
		}
		SkillPointsTXT.text = checkSkillPoint.ToString ();

		//SPELL ABILITIES
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			string slot1Skill = slot.getgameobject (1);
			if(slot.getgameobject(1) != null){
			switch (slot1Skill) {
				case "Health Skill3":
					Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
					//spell4.gameObject.transform.SetParent (playerobj.transform);
					//Skillspelleffect4.transform.SetAsFirstSibling ();
					spell4.gameObject.SetActive (true);
					spell4activate = true;
					break;
				case "Mana Skill3":
					Skillspelleffect1 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
					spell1.gameObject.SetActive (true);
					spell1activate = true;
					break;
				case "Power Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Speed Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Power Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;

				case "Speed Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;
			}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			string slot2Skill = slot.getgameobject (2);
			if(slot.getgameobject(2) != null){
				switch (slot2Skill) {
				case "Health Skill3":
					Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
					//spell4.gameObject.transform.SetParent (playerobj.transform);
					//Skillspelleffect4.transform.SetAsFirstSibling ();
					spell4.gameObject.SetActive (true);
					spell4activate = true;
					break;
				case "Mana Skill3":
					Skillspelleffect1 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
					spell1.gameObject.SetActive (true);
					spell1activate = true;
					break;
				case "Power Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Speed Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Power Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;

				case "Speed Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			string slot3Skill = slot.getgameobject (3);
			if(slot.getgameobject(3) != null){
				switch (slot3Skill) {
				case "Health Skill3":
					Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
					//spell4.gameObject.transform.SetParent (playerobj.transform);
					//Skillspelleffect4.transform.SetAsFirstSibling ();
					spell4.gameObject.SetActive (true);
					spell4activate = true;
					break;
				case "Mana Skill3":
					Skillspelleffect1 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
					spell1.gameObject.SetActive (true);
					spell1activate = true;
					break;
				case "Power Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Speed Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Power Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;

				case "Speed Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			string slot4Skill = slot.getgameobject (4);
			if(slot.getgameobject(4) != null){
				switch (slot4Skill) {
				case "Health Skill3":
					Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
					//spell4.gameObject.transform.SetParent (playerobj.transform);
					//Skillspelleffect4.transform.SetAsFirstSibling ();
					spell4.gameObject.SetActive (true);
					spell4activate = true;
					break;
				case "Mana Skill3":
					Skillspelleffect1 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
					spell1.gameObject.SetActive (true);
					spell1activate = true;
					break;
				case "Power Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Speed Skill1":
					Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					spell2.gameObject.SetActive (true);
					spell2activate = true;
					break;
				case "Power Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;

				case "Speed Skill3":
					Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
					spell3.gameObject.SetActive (true);
					spell3activate = true;
					break;
				}
			}
		
		}
		if (spell1activate == true && spell1.fillAmount <= 0) {
			Destroy (Skillspelleffect1);
			spell1activate = false;
			spell1.gameObject.SetActive (false);
			spell1.fillAmount = 1.0f;
		}
		if(spell1activate == true){
			spell1.fillAmount -= 0.5f * Time.deltaTime;
		}

		if (spell2activate == true && spell2.fillAmount <= 0) {
			Destroy (Skillspelleffect2);
			spell2activate = false;
			spell2.gameObject.SetActive (false);
			spell2.fillAmount = 1.0f;
		}
		if(spell2activate == true){
			spell2.fillAmount -= 0.5f * Time.deltaTime;
		}

		if (spell3activate == true && spell3.fillAmount <= 0) {
			Destroy (Skillspelleffect3);
			spell3activate = false;
			spell3.gameObject.SetActive (false);
			spell3.fillAmount = 1.0f;
		}
		if(spell3activate == true){
			spell3.fillAmount -= 0.5f * Time.deltaTime;
		}

		if (spell4activate == true && spell4.fillAmount <= 0) {
			Destroy (Skillspelleffect4);
			spell4activate = false;
			spell4.gameObject.SetActive (false);
			spell4.fillAmount = 1.0f;
		}
		if(spell4activate == true){
			spell4.fillAmount -= 0.5f * Time.deltaTime;
		}

	}



	public void SelectSkillOnClick(){
		
		if (checkSkillPoint > 0) {
			if (eventsys.currentSelectedGameObject.GetComponent<Button> ().interactable == true) {

				if (eventsys.currentSelectedGameObject == HealthActive1 && unlockLifeActive1 == false) {
					checkSkillPoint -= 1;
				} else if (eventsys.currentSelectedGameObject == ManaActive1 && unlockManaActive1 == false) {
					checkSkillPoint -= 1;
				} else if (eventsys.currentSelectedGameObject == PowerActive1 && unlockPowerActive1 == false) {
					checkSkillPoint -= 1;
				} else if (eventsys.currentSelectedGameObject == PowerActive2 && unlockPowerActive2 == false) {
					checkSkillPoint -= 1;
				} else if (eventsys.currentSelectedGameObject == SpeedActive1 && unlockSpeedActive1 == false) {
					checkSkillPoint -= 1;
				} else if (eventsys.currentSelectedGameObject == SpeedActive2 && unlockSpeedActive1 == false) {
					checkSkillPoint -= 1;
				} else {
					if (eventsys.currentSelectedGameObject.tag == "PassiveSkill") {
						checkSkillPoint -= 1;
					}
				}
			}
		}

	
			if (eventsys.currentSelectedGameObject == HealthUp) {
				unlockHealthUp = true;
				if (HealthUpAmt <= 3) {
					HealthUpAmt++;
				gameObject.GetComponent<Health> ().maxhealth += 5;
				gameObject.GetComponent<Health> ().healthbarslider.maxValue += 5;
				} else {
					HealthUp_Btn.interactable = false;
				}

			}
			else if(eventsys.currentSelectedGameObject == ManaUp){
				unlockManaUp = true;
				if (ManaUpAmt <= 3) {
					ManaUpAmt++;
				gameObject.GetComponent<Health> ().maxmana += 5;
				gameObject.GetComponent<Health> ().manabarslider.maxValue += 5;
				} else {
					ManaUp_Btn.interactable = false;
				}

			}

		else if (eventsys.currentSelectedGameObject == HealthPassive1) {
				unlockLifePassive1 = true;
		
			}
		else if (eventsys.currentSelectedGameObject == HealthPassive2) {
				unlockLifePassive2 = true;

			}
		else if (eventsys.currentSelectedGameObject == HealthActive1) {
				unlockLifeActive1 = true;
			
			}
		else if (eventsys.currentSelectedGameObject == ManaPassive1) {
				unlockManaPassive1 = true;

			}
		else if (eventsys.currentSelectedGameObject == ManaPassive2) {
				unlockManaPassive2 = true;

			}
		else if (eventsys.currentSelectedGameObject == ManaActive1) {
				unlockManaActive1 = true;
			
			}

		else if (eventsys.currentSelectedGameObject == PowerActive1) {
				unlockPowerActive1 = true;
			
			}
		else if (eventsys.currentSelectedGameObject == PowerPassive1) {
			
		
			}
		else if (eventsys.currentSelectedGameObject == PowerActive2) {
				unlockPowerActive2 = true;
			
			}

		else if (eventsys.currentSelectedGameObject == SpeedActive1) {
				unlockSpeedActive1 = true;
			
			}
		else if (eventsys.currentSelectedGameObject == SpeedPassive1) {
				unlockSpeedPassive1 = true;
			}
		else if (eventsys.currentSelectedGameObject == SpeedActive2) {
				unlockSpeedActive2 = true;
			
			}


		else {
			//ErrorMessageTXT.SetActive (true);
		}
		SkillPointsTXT.text = checkSkillPoint.ToString ();
	}
		

	void updateMagicPoints(){
		//SkillPointsTXT.text = "" + GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints;
		
	}
}
