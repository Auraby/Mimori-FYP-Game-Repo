using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;


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

	private int PassiveUpAmt;
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

	public Text healthuplevel;
	public Text manauplevel;
	public int healthupint;
	public int manaupint;
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

	//Class-Specific Passive
	public bool Warborn;
	public bool Gunslinger;
	public bool Spellslinger;
	public bool Spellshield;
	public Text warborntxt;
	public Text gunslingertxt;
	public Text spellslingertxt;
	public Text spellshieldtxt;

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

	public bool hunterbreathactivated;
	public bool leechingbulletactivated;
	public bool thunderrushactivated;
	public bool berserkeractivated;
	public bool sentrymodeactivated;
	public bool toofastforyouactivated;
	public bool deadoraliveactivated;
	public float deadoralivecd = 120.0f;
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
	public float incSpeed; //firerate
	public int deflectchance; //chance to deflect
	public bool unlockdeflect;
	public float incMSpeed; //movementrate
	public Text WarningTxt;
	public Text HealthStats;
	public Text ManaStats;

	public Image leechingbulletDurationImage;
	public Image ThunderRushDurationImage;
	public Image BerserkerDurationImage;
	//public Toggle ChargeShot;
	public Text SkillPointsTXT;
	//public GameObject ErrorMessageTXT;

	public bool classChosen = false;
	public bool updateloopcounter = false;
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
	public Transform playertransform;
	FirstPersonController playergameobj;
	RaycastHit hit;
	float playercolliderradius;
	public string slot1Skill = "";
	public string slot2Skill = "";
	public string slot3Skill = "";
	public string slot4Skill = "";

	public GameObject slot1obj;
	public GameObject slot2obj;
	public GameObject slot3obj;
	public GameObject slot4obj;

	//skill effect particles prefab
	public GameObject hunterbreatheffect_prefab;

	public bool counterskillloop = false;
	public int skillpointspent;


	public bool manashieldup = false;
	public float manashieldcd = 0;
	Slot slot;

	// Use this for initialization
	void Start () {
		//GameController.gameController.checkSkillPoint = 10;
		GameController.gameController.checkSkillPoint = 0;
		playercolliderradius = playerobj.GetComponent<CapsuleCollider> ().radius;
	}



	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			//Display SkillTree
		}
		SkillPointsTXT.text = GameController.gameController.checkSkillPoint.ToString ();
		//Class-SPecific abilities Check
		if (unlockLifePassive1 == true && unlockPowerActive1 == true && classChosen == false) {
			Warborn = true;
			classChosen = true;
			playerobj.GetComponent<Player> ().fireDelay += 1.5f;
			gameObject.GetComponent<Health> ().maxhealth += 20;
			gameObject.GetComponent<Health> ().healthbarslider.maxValue += 20;
			playerobj.GetComponent<BulletController> ().playerbasedamage += 5;
			unlockdeflect = true;
			playerobj.GetComponent<FirstPersonController>().m_WalkSpeed -= 3.0f;
			playerobj.GetComponent<FirstPersonController>().m_RunSpeed -= 3.0f;
			playerobj.GetComponent<FirstPersonController>().m_JumpSpeed -= 3.0f;

		} 
		if (unlockLifePassive1 == true && unlockSpeedActive1 == true && classChosen == false) {
			Gunslinger = true;
			classChosen = true;
			//Debug.Log ("Class Is Chosen");
			playerobj.GetComponent<Player> ().fireDelay -= 1.5f;
			playerobj.GetComponent<FirstPersonController>().m_WalkSpeed += 3.5f;
			playerobj.GetComponent<FirstPersonController>().m_RunSpeed += 3.5f;
			playerobj.GetComponent<FirstPersonController>().m_JumpSpeed += 3.5f;
		}  
		if (unlockManaPassive1 == true && unlockPowerActive1 == true && classChosen == false ) {
			classChosen = true;
			Spellshield = true;
			gameObject.GetComponent<Health> ().maxhealth += 5;
			gameObject.GetComponent<Health> ().healthbarslider.maxValue += 5;
			gameObject.GetComponent<Health> ().maxmana += 5;
			gameObject.GetComponent<Health> ().manabarslider.maxValue += 5;
			playerobj.GetComponent<FirstPersonController>().m_WalkSpeed -= 1.0f;
			playerobj.GetComponent<FirstPersonController>().m_RunSpeed -= 1.0f;
			playerobj.GetComponent<FirstPersonController>().m_JumpSpeed -= 1.0f;
		}
		if (unlockManaPassive1 == true && unlockSpeedActive1 == true && classChosen == false ) {
			classChosen = true;
			Spellslinger = true;
			//Debug.Log ("Class is Chosen");
			gameObject.GetComponent<Health> ().maxmana += 20;
			gameObject.GetComponent<Health> ().manabarslider.maxValue += 20;
			playerobj.GetComponent<FirstPersonController>().m_WalkSpeed += 1.0f;
			playerobj.GetComponent<FirstPersonController>().m_RunSpeed += 1.0f;
			playerobj.GetComponent<FirstPersonController>().m_JumpSpeed += 1.0f;


		}

		if (unlockManaPassive2) {
			if (playerobj.GetComponent<Health> ().manabar <= 0 && manashieldup == false && manashieldcd <= 0) {
				manashieldup = true;
				manashieldcd = 50.0f;
				StartCoroutine (PassiveDurations (manashieldup, 5));
			}
		}

		if (manashieldcd > 0) {
			manashieldcd -= 1.0f * Time.deltaTime;
		} else if (manashieldcd <= 0) {
			manashieldcd = 0;
		}

		if (toofastforyouactivated) {
			playerobj.GetComponent<Health> ().manabar -= 8.0f;
			playerobj.GetComponent<Health> ().manabarslider.value -= 8.0f;
			if (playerobj.GetComponent<Health> ().manabar <= 0) {
				toofastforyouactivated = false;
			}
		}

		//SPELL ABILITIES
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if(slot1Skill != null){
			switch (slot1Skill) {
				case "Health Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 100) {
						//Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
						//spell4.gameObject.transform.SetParent (playerobj.transform);
						//Skillspelleffect4.transform.SetAsFirstSibling ();
						spell1.gameObject.SetActive (true);
						spell1activate = true;
						hunterbreathactivated = true;
						GameObject hunterbreatheffect = (GameObject)Instantiate (hunterbreatheffect_prefab, playerobj.transform.position, transform.rotation, playerobj.transform);
						//hunterbreatheffect.transform.SetParent (playerobj.transform);
						StartCoroutine (DestroyParticlesEffect (hunterbreatheffect, 3));
						gameObject.GetComponent<Health> ().currentHealth += 30;
						gameObject.GetComponent<Health> ().healthbarslider.value += 30;
						gameObject.GetComponent<Health> ().manabar -= 100;
						gameObject.GetComponent<Health> ().manabarslider.value -= 100;
					} else if (spell1activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Mana Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 35 && !spell1activate) {
						Skillspelleffect1 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
						spell1.gameObject.SetActive (true);
						spell1activate = true;
						leechingbulletactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 35;
						gameObject.GetComponent<Health> ().manabarslider.value -= 35;
						leechingbulletDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (leechingbulletDurationImage,leechingbulletactivated, 10));
						//StartCoroutine (SpellCoolDown (spell1,30, spell1activate));
					} else if (spell1activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}

					break;
				case "Power Skill1":

					if (sentrymodeactivated == false) {
						gameObject.GetComponent<Player> ().isIronSight = 1;
						//gameObject.GetComponent<Player> ().ToggleSentryMode ();
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (true);
						//gameObject.GetComponent<Player> ().currfireDelay += gameObject.GetComponent<Player> ().fireDelay += 2.0f;
						sentrymodeactivated = true;
					} else {
						gameObject.GetComponent<Player> ().isIronSight = 0;
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (false);
						//gameObject.GetComponent<Player> ().currfireDelay = gameObject.GetComponent<Player> ().fireDelay;
						sentrymodeactivated = false;
					}
						//Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
						//spell1.gameObject.SetActive (true);
						//spell1activate = true;
						//counterskillloop = true;
						//sentrymodeactivated = true;
						//sentrymodeactivated = false;
					break;
				case "Speed Skill1": //Thunder Rush
					if (gameObject.GetComponent<Health> ().manabar >= 50 && !spell1activate) {
						Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
						spell1.gameObject.SetActive (true);
						spell1activate = true;
						thunderrushactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 50;
						gameObject.GetComponent<Health> ().manabarslider.value -= 50;
						Vector3 dashdirection = transform.forward;
						playerobj.GetComponent<Player> ().currfireDelay = playerobj.GetComponent<Player> ().fireDelay / 100 * 120;
						float dashlength = 50.0f;
						if (Physics.Raycast (transform.position, dashdirection, out hit, 50.0f + playercolliderradius)) {
							dashlength = hit.distance - playercolliderradius;
						}
						playerobj.transform.position = transform.position + (dashdirection * dashlength);
					
						ThunderRushDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (ThunderRushDurationImage, thunderrushactivated, 5));
					}else if (spell1activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Power Skill3":

					if (gameObject.GetComponent<Health> ().manabar >= 70 && !spell1activate) {
						Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
						spell1.gameObject.SetActive (true);
						spell1activate = true;
						berserkeractivated = true;
						gameObject.GetComponent<Health> ().manabar -= 70;
						gameObject.GetComponent<Health> ().manabarslider.value -= 70;
						BerserkerDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (BerserkerDurationImage, berserkeractivated, 20));
					} else if (spell1activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;

				case "Speed Skill3":
					if (toofastforyouactivated = false) {
						if (playergameobj.GetComponent<Health> ().manabar >= 8) {
							toofastforyouactivated = true;
						}
					} else {
						
						toofastforyouactivated = false;
					}
					break;
			}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			if(slot2Skill != null){
				switch (slot2Skill) {
				case "Health Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 100) {
						//Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
						//spell4.gameObject.transform.SetParent (playerobj.transform);
						//Skillspelleffect4.transform.SetAsFirstSibling ();
						spell2.gameObject.SetActive (true);
						spell2activate = true;
						hunterbreathactivated = true;
						GameObject hunterbreatheffect = (GameObject)Instantiate (hunterbreatheffect_prefab, playerobj.transform.position, transform.rotation, playerobj.transform);
						//hunterbreatheffect.transform.SetParent (playerobj.transform);
						StartCoroutine (DestroyParticlesEffect (hunterbreatheffect, 3));
						gameObject.GetComponent<Health> ().currentHealth += 30;
						gameObject.GetComponent<Health> ().healthbarslider.value += 30;
						gameObject.GetComponent<Health> ().manabar -= 100;
						gameObject.GetComponent<Health> ().manabarslider.value -= 100;
					} else if (spell2activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Mana Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 35 && !spell2activate) {
						Skillspelleffect2 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
						spell2.gameObject.SetActive (true);
						spell2activate = true;
						leechingbulletactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 35;
						gameObject.GetComponent<Health> ().manabarslider.value -= 35;
						leechingbulletDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (leechingbulletDurationImage,leechingbulletactivated, 10));
						//StartCoroutine (SpellCoolDown (spell1,30, spell1activate));
					} else if (spell2activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}

					break;
				case "Power Skill1":

					if (sentrymodeactivated == false) {
						gameObject.GetComponent<Player> ().isIronSight = 1;
						//gameObject.GetComponent<Player> ().ToggleSentryMode ();
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().currfireDelay += gameObject.GetComponent<Player> ().fireDelay += 2.0f;
						sentrymodeactivated = true;
					} else {
						gameObject.GetComponent<Player> ().isIronSight = 0;
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().currfireDelay = gameObject.GetComponent<Player> ().fireDelay;
						sentrymodeactivated = false;
					}
					//Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					//spell1.gameObject.SetActive (true);
					//spell1activate = true;
					//counterskillloop = true;
					//sentrymodeactivated = true;
					//sentrymodeactivated = false;
					break;
				case "Speed Skill1": //Thunder Rush
					if (gameObject.GetComponent<Health> ().manabar >= 50 && !spell2activate) {
						Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
						spell2.gameObject.SetActive (true);
						spell2activate = true;
						thunderrushactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 50;
						gameObject.GetComponent<Health> ().manabarslider.value -= 50;
						Vector3 dashdirection = transform.forward;
						playerobj.GetComponent<Player> ().currfireDelay = playerobj.GetComponent<Player> ().fireDelay / 100 * 120;
						float dashlength = 50.0f;
						if (Physics.Raycast (transform.position, dashdirection, out hit, 50.0f + playercolliderradius)) {
							dashlength = hit.distance - playercolliderradius;
						}
						playerobj.transform.position = transform.position + (dashdirection * dashlength);

						ThunderRushDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (ThunderRushDurationImage, thunderrushactivated, 5));
					}else if (spell2activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Power Skill3":

					if (gameObject.GetComponent<Health> ().manabar >= 70 && !spell2activate) {
						Skillspelleffect2 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
						spell2.gameObject.SetActive (true);
						spell2activate = true;
						berserkeractivated = true;
						gameObject.GetComponent<Health> ().manabar -= 70;
						gameObject.GetComponent<Health> ().manabarslider.value -= 70;
						BerserkerDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (BerserkerDurationImage, berserkeractivated, 20));
					} else if (spell2activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;

				case "Speed Skill3":
					if (toofastforyouactivated = false) {
						if (playergameobj.GetComponent<Health> ().manabar >= 8) {
							toofastforyouactivated = true;
						}
					} else {

						toofastforyouactivated = false;
					}
					break;
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){

			if(slot3Skill != null){
				switch (slot3Skill) {
				case "Health Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 100) {
						//Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
						//spell4.gameObject.transform.SetParent (playerobj.transform);
						//Skillspelleffect4.transform.SetAsFirstSibling ();
						spell3.gameObject.SetActive (true);
						spell3activate = true;
						hunterbreathactivated = true;
						GameObject hunterbreatheffect = (GameObject)Instantiate (hunterbreatheffect_prefab, playerobj.transform.position, transform.rotation, playerobj.transform);
						//hunterbreatheffect.transform.SetParent (playerobj.transform);
						StartCoroutine (DestroyParticlesEffect (hunterbreatheffect, 3));
						gameObject.GetComponent<Health> ().currentHealth += 30;
						gameObject.GetComponent<Health> ().healthbarslider.value += 30;
						gameObject.GetComponent<Health> ().manabar -= 100;
						gameObject.GetComponent<Health> ().manabarslider.value -= 100;
					} else if (spell3activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Mana Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 35 && !spell3activate) {
						Skillspelleffect3 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
						spell3.gameObject.SetActive (true);
						spell3activate = true;
						leechingbulletactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 35;
						gameObject.GetComponent<Health> ().manabarslider.value -= 35;
						leechingbulletDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (leechingbulletDurationImage,leechingbulletactivated, 10));
						//StartCoroutine (SpellCoolDown (spell1,30, spell1activate));
					} else if (spell3activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}

					break;
				case "Power Skill1":

					if (sentrymodeactivated == false) {
						gameObject.GetComponent<Player> ().isIronSight = 1;
						//gameObject.GetComponent<Player> ().ToggleSentryMode ();
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().currfireDelay += gameObject.GetComponent<Player> ().fireDelay += 2.0f;
						sentrymodeactivated = true;
					} else {
						gameObject.GetComponent<Player> ().isIronSight = 0;
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().currfireDelay = gameObject.GetComponent<Player> ().fireDelay;
						sentrymodeactivated = false;
					}
					//Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					//spell1.gameObject.SetActive (true);
					//spell1activate = true;
					//counterskillloop = true;
					//sentrymodeactivated = true;
					//sentrymodeactivated = false;
					break;
				case "Speed Skill1": //Thunder Rush
					if (gameObject.GetComponent<Health> ().manabar >= 50 && !spell3activate) {
						Skillspelleffect3 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
						spell3.gameObject.SetActive (true);
						spell3activate = true;
						thunderrushactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 50;
						gameObject.GetComponent<Health> ().manabarslider.value -= 50;
						Vector3 dashdirection = transform.forward;
						playerobj.GetComponent<Player> ().currfireDelay = playerobj.GetComponent<Player> ().fireDelay / 100 * 120;
						float dashlength = 50.0f;
						if (Physics.Raycast (transform.position, dashdirection, out hit, 50.0f + playercolliderradius)) {
							dashlength = hit.distance - playercolliderradius;
						}
						playerobj.transform.position = transform.position + (dashdirection * dashlength);

						ThunderRushDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (ThunderRushDurationImage, thunderrushactivated, 5));
					}else if (spell3activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Power Skill3":

					if (gameObject.GetComponent<Health> ().manabar >= 70 && !spell3activate) {
						Skillspelleffect3 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
						spell3.gameObject.SetActive (true);
						spell3activate = true;
						berserkeractivated = true;
						gameObject.GetComponent<Health> ().manabar -= 70;
						gameObject.GetComponent<Health> ().manabarslider.value -= 70;
						BerserkerDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (BerserkerDurationImage, berserkeractivated, 20));
					} else if (spell3activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;

				case "Speed Skill3":
					if (toofastforyouactivated = false) {
						if (playergameobj.GetComponent<Health> ().manabar >= 8) {
							toofastforyouactivated = true;
						}
					} else {

						toofastforyouactivated = false;
					}
					break;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {

			if(slot4Skill != null){
				switch (slot4Skill) {
				case "Health Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 100) {
						//Skillspelleffect4 = (GameObject)Instantiate (skilleffect4, playerobj.transform.position, Quaternion.identity);
						//spell4.gameObject.transform.SetParent (playerobj.transform);
						//Skillspelleffect4.transform.SetAsFirstSibling ();
						spell4.gameObject.SetActive (true);
						spell4activate = true;
						hunterbreathactivated = true;
						GameObject hunterbreatheffect = (GameObject)Instantiate (hunterbreatheffect_prefab, playerobj.transform.position, transform.rotation, playerobj.transform);
						//hunterbreatheffect.transform.SetParent (playerobj.transform);
						StartCoroutine (DestroyParticlesEffect (hunterbreatheffect, 3));
						gameObject.GetComponent<Health> ().currentHealth += 30;
						gameObject.GetComponent<Health> ().healthbarslider.value += 30;
						gameObject.GetComponent<Health> ().manabar -= 100;
						gameObject.GetComponent<Health> ().manabarslider.value -= 100;
					} else if (spell4activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Mana Skill3":
					if (gameObject.GetComponent<Health> ().manabar >= 35 && !spell4activate) {
						Skillspelleffect4 = (GameObject)Instantiate (skilleffect1, playerobj.transform.position, Quaternion.identity);
						spell4.gameObject.SetActive (true);
						spell4activate = true;
						leechingbulletactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 35;
						gameObject.GetComponent<Health> ().manabarslider.value -= 35;
						leechingbulletDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (leechingbulletDurationImage,leechingbulletactivated, 10));
						//StartCoroutine (SpellCoolDown (spell1,30, spell1activate));
					} else if (spell4activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}

					break;
				case "Power Skill1":

					if (sentrymodeactivated == false) {
						gameObject.GetComponent<Player> ().isIronSight = 1;
						//gameObject.GetComponent<Player> ().ToggleSentryMode ();
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().currfireDelay += gameObject.GetComponent<Player> ().fireDelay += 2.0f;
						sentrymodeactivated = true;
					} else {
						gameObject.GetComponent<Player> ().isIronSight = 0;
						gameObject.GetComponent<Player> ().gun.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gunEnd.gameObject.SetActive (true);
						gameObject.GetComponent<Player> ().gun_IronSight.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().gunEnd_IronSight.gameObject.SetActive (false);
						gameObject.GetComponent<Player> ().currfireDelay = gameObject.GetComponent<Player> ().fireDelay;
						sentrymodeactivated = false;
					}
					//Skillspelleffect2 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
					//spell1.gameObject.SetActive (true);
					//spell1activate = true;
					//counterskillloop = true;
					//sentrymodeactivated = true;
					//sentrymodeactivated = false;
					break;
				case "Speed Skill1": //Thunder Rush
					if (gameObject.GetComponent<Health> ().manabar >= 50 && !spell4activate) {
						Skillspelleffect4 = (GameObject)Instantiate (skilleffect2, playerobj.transform.position, Quaternion.identity);
						spell4.gameObject.SetActive (true);
						spell4activate = true;
						thunderrushactivated = true;
						gameObject.GetComponent<Health> ().manabar -= 50;
						gameObject.GetComponent<Health> ().manabarslider.value -= 50;
						Vector3 dashdirection = transform.forward;
						playerobj.GetComponent<Player> ().currfireDelay = playerobj.GetComponent<Player> ().fireDelay / 100 * 120;
						float dashlength = 50.0f;
						if (Physics.Raycast (transform.position, dashdirection, out hit, 50.0f + playercolliderradius)) {
							dashlength = hit.distance - playercolliderradius;
						}
						playerobj.transform.position = transform.position + (dashdirection * dashlength);

						ThunderRushDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (ThunderRushDurationImage, thunderrushactivated, 5));
					}else if (spell4activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;
				case "Power Skill3":

					if (gameObject.GetComponent<Health> ().manabar >= 70 && !spell4activate) {
						Skillspelleffect4 = (GameObject)Instantiate (skilleffect3, playerobj.transform.position, Quaternion.identity);
						spell4.gameObject.SetActive (true);
						spell4activate = true;
						berserkeractivated = true;
						gameObject.GetComponent<Health> ().manabar -= 70;
						gameObject.GetComponent<Health> ().manabarslider.value -= 70;
						BerserkerDurationImage.gameObject.SetActive (true);
						StartCoroutine (SpellDurations (BerserkerDurationImage, berserkeractivated, 20));
					} else if (spell4activate) {
						StartCoroutine (DisplayWarning ("Ability On Cooldown", 2));
					}
					else {
						StartCoroutine (DisplayWarning ("Insufficient Mana", 2));
					}
					break;

				case "Speed Skill3":
					if (toofastforyouactivated = false) {
						if (playergameobj.GetComponent<Health> ().manabar >= 8) {
							toofastforyouactivated = true;
						}
					} else {

						toofastforyouactivated = false;
					}
					break;
				}
			}
		
		}

		//Spells Cooldown/Time
		if (leechingbulletactivated) {
		}

		if (spell1activate == true && spell1.fillAmount <= 0) {
			Destroy (Skillspelleffect1);
			spell1activate = false;
			spell1.gameObject.SetActive (false);
			spell1.fillAmount = 1.0f;
		}
		if(spell1activate == true){
			if (berserkeractivated) {
				spell1.fillAmount -= 1.0f / 120.0f * Time.deltaTime;
			} else {
				spell1.fillAmount -= 1.0f / 30.0f * Time.deltaTime;
			}
		}

		if (spell2activate == true && spell2.fillAmount <= 0) {
			Destroy (Skillspelleffect2);
			spell2activate = false;
			spell2.gameObject.SetActive (false);
			spell2.fillAmount = 1.0f;
		}
		if(spell2activate == true){
			if (berserkeractivated) {
				spell2.fillAmount -= 1.0f / 120.0f * Time.deltaTime;
			} else {
				spell2.fillAmount -= 1.0f / 30.0f * Time.deltaTime;
			}
		}

		if (spell3activate == true && spell3.fillAmount <= 0) {
			Destroy (Skillspelleffect3);
			spell3activate = false;
			spell3.gameObject.SetActive (false);
			spell3.fillAmount = 1.0f;
		}
		if(spell3activate == true){
			if (berserkeractivated) {
				spell3.fillAmount -= 1.0f / 120.0f * Time.deltaTime;
			} else {
				spell3.fillAmount -= 1.0f / 30.0f * Time.deltaTime;
			}
		}

		if (spell4activate == true && spell4.fillAmount <= 0) {
			Destroy (Skillspelleffect4);
			spell4activate = false;
			spell4.gameObject.SetActive (false);
			spell4.fillAmount = 1.0f;
		}
		if(spell4activate == true){
			if (berserkeractivated) {
				spell4.fillAmount -= 1.0f / 120.0f * Time.deltaTime;
			} else {
				spell4.fillAmount -= 1.0f / 30.0f * Time.deltaTime;
			}
		}

	}

	IEnumerator DisplayWarning (string warning, float delay){
		WarningTxt.text = warning;
		WarningTxt.gameObject.SetActive (true);
		yield return new WaitForSeconds (delay);
		WarningTxt.gameObject.SetActive (false);
	}

	IEnumerator SpellDurations (Image spelldurationimage, bool spellactivated ,float duration){
		yield return new WaitForSeconds (duration);

		spellactivated = false;
		spelldurationimage.gameObject.SetActive (false);
	}

	public IEnumerator PassiveDurations (bool spellactivated ,float duration){
		yield return new WaitForSeconds (duration);
		spellactivated = false;
		duration = 0;
	}
	IEnumerator DestroyParticlesEffect (GameObject particle, float duration){
		yield return new WaitForSeconds (duration);
		Destroy (particle);
	}

	/*IEnumerator SpellCoolDown (Image spellobj, float cd, bool spellactivated){
		spellobj.gameObject.SetActive (true);
		spellobj.fillAmount = cd;
		spellobj.fillAmount -= 1.0f * Time.deltaTime;
		if (spellobj.fillAmount <= 0) {
			yield return new WaitForSeconds (1);
			spellobj.gameObject.SetActive (false);
			spellobj.fillAmount = cd;
			spellactivated = false;

		}
	}*/
		
	//Reset Lock all Abilities
	public void LockAllSpell(){
		///False To All unlocked abilities
		unlockLifeActive1 = false;
		unlockLifePassive1 = false;
		unlockLifePassive2 = false;

		unlockManaActive1 = false;
		unlockManaPassive1 = false;
		unlockManaPassive2 = false;

		unlockPowerActive1 = false;
		unlockPowerActive2 = false;
		unlockPowerPassive1 = false;

		unlockSpeedActive1 = false;
		unlockSpeedActive2 = false;
		unlockSpeedPassive1 = false;
		/// Clear all spells in the slot
		slot.ClearSlots();
		//all button interactable
		HealthPassive1_Btn.interactable = true;
		ManaPassive1_Btn.interactable = true;

		PowerActive1_Btn.interactable = true;
		SpeedActive1_Btn.interactable = true;

		HealthUp_Btn.interactable = true;
		ManaUp_Btn.interactable = true;
		//deactivate all skills
		leechingbulletactivated = false;
		berserkeractivated = false;
		hunterbreathactivated = false;
		thunderrushactivated = false;
		 sentrymodeactivated = false;
		 toofastforyouactivated = false;
		 deadoraliveactivated = false;
		//deactivate all classes
		Gunslinger = false;
		Warborn = false;
		Spellslinger = false;
		Spellshield = false;

		gunslingertxt.gameObject.SetActive (false);
		warborntxt.gameObject.SetActive (false);
		spellslingertxt.gameObject.SetActive (false);
		spellshieldtxt.gameObject.SetActive (false);
		//deactivate all image
		leechingbulletDurationImage.gameObject.SetActive(false);
		BerserkerDurationImage.gameObject.SetActive (false);
		ThunderRushDurationImage.gameObject.SetActive (false);
		//
		if (gameObject.GetComponent<Player> ().isIronSight >= 1) {
			gameObject.GetComponent<Player> ().isIronSight--;
		}

	}

	public void SelectSkillOnClick(){
		
		if (GameController.gameController.checkSkillPoint > 0) {
			if (eventsys.currentSelectedGameObject.GetComponent<Button> ().interactable == true) {
				if (eventsys.currentSelectedGameObject == HealthActive1 && unlockLifeActive1 == false) {
					GameController.gameController.checkSkillPoint -= 1;
					skillpointspent += 1;
				
				}  if (eventsys.currentSelectedGameObject == ManaActive1 && unlockManaActive1 == false) {
					GameController.gameController.checkSkillPoint -= 1;
					skillpointspent += 1;
				}  if (eventsys.currentSelectedGameObject == PowerActive1 && unlockPowerActive1 == false) {
					GameController.gameController.checkSkillPoint -= 1;
					skillpointspent += 1;
				}  if (eventsys.currentSelectedGameObject == PowerActive2 && unlockPowerActive2 == false) {
					GameController.gameController.checkSkillPoint -= 1;
					skillpointspent += 1;
				}  if (eventsys.currentSelectedGameObject == SpeedActive1 && unlockSpeedActive1 == false) {
					GameController.gameController.checkSkillPoint -= 1;
					skillpointspent += 1;
					SpeedPassive1_Btn.interactable = true;
					PowerActive1_Btn.interactable = false;
				}  if (eventsys.currentSelectedGameObject == SpeedActive2 && unlockSpeedActive1 == false) {
					GameController.gameController.checkSkillPoint -= 1;
					skillpointspent += 1;
				} else {
					if (eventsys.currentSelectedGameObject.tag == "PassiveSkill") {
						GameController.gameController.checkSkillPoint -= 1;
						skillpointspent += 1;

					}
				}

			}
		}

	
			if (eventsys.currentSelectedGameObject == HealthUp && GameController.gameController.checkSkillPoint > 0) {
				unlockHealthUp = true;
				if (PassiveUpAmt <= 6) {
				PassiveUpAmt++;
				healthupint++;
				healthuplevel.text = "Level " + healthupint.ToString();
				gameObject.GetComponent<Health> ().maxhealth += 5;
				gameObject.GetComponent<Health> ().healthbarslider.maxValue += 5;
				} else {

					HealthUp_Btn.interactable = false;
					ManaUp_Btn.interactable = false;
					

				}

			}
		else if(eventsys.currentSelectedGameObject == ManaUp && GameController.gameController.checkSkillPoint > 0){
				unlockManaUp = true;
			if (PassiveUpAmt <= 6) {
				PassiveUpAmt++;
				manaupint++;
				manauplevel.text = "Level " + manaupint.ToString();
				gameObject.GetComponent<Health> ().maxmana += 5;
				gameObject.GetComponent<Health> ().manabarslider.maxValue += 5;
				} else {
				HealthUp_Btn.interactable = false;
				ManaUp_Btn.interactable = false;
				}

			}

		else if (eventsys.currentSelectedGameObject == HealthPassive1 && GameController.gameController.checkSkillPoint > 0) {
				unlockLifePassive1 = true;
			HealthPassive2_Btn.interactable = true;
			HealthPassive1_Btn.interactable = false;
			ManaPassive1_Btn.interactable = false;
		
			}
		else if (eventsys.currentSelectedGameObject == HealthPassive2 && GameController.gameController.checkSkillPoint > 0) {
				unlockLifePassive2 = true;
			HealthActive1_Btn.interactable = true;
			HealthPassive2_Btn.interactable = false;

			}
		else if (eventsys.currentSelectedGameObject == HealthActive1 && GameController.gameController.checkSkillPoint > 0) {
				unlockLifeActive1 = true;
			
			}
		else if (eventsys.currentSelectedGameObject == ManaPassive1 && GameController.gameController.checkSkillPoint > 0) {
				unlockManaPassive1 = true;
			ManaPassive2_Btn.interactable = true;
			ManaPassive1_Btn.interactable = false;
			HealthPassive1_Btn.interactable = false;
			}
		else if (eventsys.currentSelectedGameObject == ManaPassive2 && GameController.gameController.checkSkillPoint > 0) {
				unlockManaPassive2 = true;
				ManaActive1_Btn.interactable = true;
				ManaPassive2_Btn.interactable = false;
			}
		else if (eventsys.currentSelectedGameObject == ManaActive1 && GameController.gameController.checkSkillPoint > 0) {
				unlockManaActive1 = true;
			
			}

		else if (eventsys.currentSelectedGameObject == PowerActive1 && GameController.gameController.checkSkillPoint > 0) {
				unlockPowerActive1 = true;
				PowerPassive1_Btn.interactable = true;
				SpeedActive1_Btn.interactable = false;
			}
		else if (eventsys.currentSelectedGameObject == PowerPassive1 && GameController.gameController.checkSkillPoint > 0) {
			unlockPowerPassive1 = true;
			PowerActive2_Btn.interactable = true;
			PowerPassive1_Btn.interactable = false;
		
			}
		else if (eventsys.currentSelectedGameObject == PowerActive2 && GameController.gameController.checkSkillPoint > 0) {
				unlockPowerActive2 = true;
			
			}

		else if (eventsys.currentSelectedGameObject == SpeedActive1 && GameController.gameController.checkSkillPoint > 0) {
				unlockSpeedActive1 = true;
			SpeedActive1_Btn.interactable = true;
			PowerPassive1_Btn.interactable = false;
			}
		else if (eventsys.currentSelectedGameObject == SpeedPassive1 && GameController.gameController.checkSkillPoint > 0) {
				unlockSpeedPassive1 = true;
				playerobj.GetComponent<Player> ().fireDelay -= 0.3f;
				SpeedActive2_Btn.interactable = true;
				SpeedPassive1_Btn.interactable = false;
			}
		else if (eventsys.currentSelectedGameObject == SpeedActive2 && GameController.gameController.checkSkillPoint > 0) {
				unlockSpeedActive2 = true;
			
			}


		else {
			//ErrorMessageTXT.SetActive (true);
		}
		SkillPointsTXT.text = GameController.gameController.checkSkillPoint.ToString ();
	}

	public bool checkifunlocked(GameObject spell){
		string spellname = spell.name;
		switch (spellname) {
		case "Health Skill3":
			if (unlockLifeActive1) {
				
				return true;
			} else {

				return false;
			}
			break;
		case "Mana Skill3":
			if (unlockManaActive1) {

				return true;
			} else {

				return false;
			}
			break;
		case "Power Skill1":
			
			if (unlockPowerActive1) {
				Debug.Log ("Power Return true");
				return true;
			} else {
				Debug.Log ("Power Return false");
				return false;
			}
			break;
		case "Power Skill3":
			if (unlockPowerActive2) {

				return true;
			} else {
				
				return false;
			}
			break;
		case "Speed Skill1":
			if (unlockSpeedActive1) {
				
				return true;
			} else {
		
				return false;
			}
			break;
		case "Speed Skill3":
			if (unlockSpeedActive2) {
				
				return true;
			} else {
	
				return false;
			}
			break;
		default:
			
			return false;
			break;
		}

	}


	public void playerstatstab(){
		HealthStats.text = gameObject.GetComponent<Health> ().maxhealth.ToString () + " %";
		ManaStats.text = gameObject.GetComponent<Health> ().maxmana.ToString () + " %";
		if (Warborn) {
			warborntxt.gameObject.SetActive (true);
		}
		if (Gunslinger) {
			gunslingertxt.gameObject.SetActive (true);
		}
		if (Spellshield) {
			spellshieldtxt.gameObject.SetActive (true);
		}
		if (Spellslinger) {
			spellslingertxt.gameObject.SetActive (true);
		}
	}


	void updateMagicPoints(){
		//SkillPointsTXT.text = "" + GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints;
		
	}
}
