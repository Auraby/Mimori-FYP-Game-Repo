using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {
    //demo variables
    public GameObject canvas;
    public Image gameoverBlackPanel;
    public Text gameoverText;
    public Text gameoverTextSubtitle;
    //-------------
    public static float inCombatCD;
    public static bool inCombat = false;

    public Camera camera;
	public GameObject interactText;
	public GameObject crosshair;
	public GameObject gun;
	public GameObject bulletPrefab;
	public GameObject leechingbulletPrefab;
	public Transform gunEnd;

    public GameObject dialogueBox;
	//Gun Animator Component
	Animator GunAnimator;
	Animation GunAnimation;
	public bool isShooting;

    //Journal Entries
    public Button JournalEntryBtn1;
    public Button JournalEntryBtn2;
    public Button JournalEntryBtn3;
    public Button JournalEntryBtn4;

    public Text JournalEntryTXT1;
    public Text JournalEntryTXT2;
    public Text JournalEntryTXT3;
    public Text JournalEntryTXT4;

    //Cannon Mechanics
    public float ChargingTime;
	public float ChargeFire = 3.0f;
	public GameObject cannonChargingEffect;
	public GameObject bullet_cannonPrefab;
	public bool isCharging;
	public GameObject ChargeBullet;
	public bool bulletinstantiate;
	//IronSights
	//[Header("Iron Sights")]
	public GameObject gun_IronSight;
	public Transform  gunEnd_IronSight;
	public int isIronSight;

	//objectives
	public Text Objective;
	//SkillTree /Pause Game
	public Image SkillTreePanel;
	[HideInInspector]
	public static CursorLockMode curseMode;
	[HideInInspector]
	public MouseLook mouselook;

	//ironsight
	public float lerpDelay;
	[HideInInspector]
	public float time;

	FirstPersonController fpc;
	RaycastHit hit;
	Ray ray;
	GameObject interactingObj;
	Shader outline;
	Shader normal;
	public Shader unoOutline;
	RaycastHit tempHit;
	public float shootrate = 0;

	//Justin's shootrate
	public float fireDelay = 0.3f;
	public float currfireDelay;
    //Combat sounds
    public AudioClip combat;
    AudioClip originalClip;
    AudioSource bgm;
    bool cSoundPlayed = false;
    bool bgmPlayed = false;
    public static bool wSoundPlayed = false;

    // Use this for initialization
    void Start () {
		//GunAnimator = gun.gameObject.GetComponent<Animator> ();
		GunAnimation = gun.gameObject.GetComponent<Animation> ();
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        originalClip = bgm.clip;
        //Continue Player
        if (GameController.loadingGame){
			transform.position = new Vector3 (
				GameController.gameController.playerPositionX,
				GameController.gameController.playerPositionY,
				GameController.gameController.playerPositionZ
			);
		}
		currfireDelay = fireDelay;
		shootrate = currfireDelay;
		isIronSight = 0;
        FirstPersonController.isPaused = false;
		Cursor.visible = false;
		curseMode = CursorLockMode.Locked;
		//interactDistance = Vector3.Distance (interactObj.transform.position, this.gameObject.transform.position);
		normal = Shader.Find ("Standard");
		outline = Shader.Find ("Outlined/Silhouetted Diffuse");
		fpc = GameObject.FindObjectOfType<FirstPersonController> ();
	}

	public void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Objective2") {
            Objective.text = "Kill 10 Mobs and obtain skillpoint".ToString ();
		}
		if (other.gameObject.tag == "Objective1") {
			Objective.text = "Complete puzzle to unlock hidden treasure".ToString ();
		}

        if (other.gameObject.tag == "DialogueTrigger") {
            if (!dialogueBox.activeSelf) {
                //Gate dialogue triggering
                if (SceneManager.GetActiveScene().name == "Gate of Telluris") {
                    if (other.gameObject.name == "dTrigger1") {
                        if (DialogueManager.enmarDialogueCount == 0 && Level1Controller.instance.startTime > 10)
                        {
                            DialogueTriggered(other);
                        }
                        if (DialogueManager.enmarDialogueCount == 1 && EnmarController.instance.reached)
                        {
                            DialogueTriggered(other);
                        }
                        if (DialogueManager.enmarDialogueCount == 2 && GameController.gameController.enmarAbsorbed)
                        {
                            DialogueTriggered(other);
                        }
                    }
                }
                //Mimori dialogue triggering
                if (SceneManager.GetActiveScene().name == "Mimori") {
                    if (DialogueManager.mimoriDialogueCount == 0 && other.gameObject.name == "dTrigger1") {
                        DialogueTriggered(other);
                    }

                    if (DialogueManager.mimoriDialogueCount == 1 && other.gameObject.name == "dTrigger2")
                    {
                        DialogueTriggered(other);
                    }

                    if (DialogueManager.mimoriDialogueCount == 2 && other.gameObject.name == "dTrigger3")
                    {
                        DialogueTriggered(other);
                    }
                }

                //Forest dialogue triggering
                if (SceneManager.GetActiveScene().name == "Forest of Misery")
                {
                    if (DialogueManager.forestDialogueCount == 0 && other.gameObject.name == "dTrigger1")
                    {
                        DialogueTriggered(other);
                    }
                    if (DialogueManager.forestDialogueCount == 1 && other.gameObject.name == "dTrigger2")
                    {
                        DialogueTriggered(other);
                    }
                    if (DialogueManager.forestDialogueCount == 2 && other.gameObject.name == "ZoltranStart")
                    {
                        StartZoltran.zoltranStart = true;
                        DialogueTriggered(other);
                    }
                    if (DialogueManager.forestDialogueCount == 3 && other.gameObject.name == "dTrigger4" && StartZoltran.zoltranDied)
                    {
                        DialogueTriggered(other);
                    }
                    if (DialogueManager.forestDialogueCount == 4 && other.gameObject.name == "dTrigger5")
                    {
                        DialogueTriggered(other);
                    }
                }
                //Temple dialogue triggering
                if (SceneManager.GetActiveScene().name == "Temple of Aphelion")
                {
                    if (DialogueManager.templeIDialogueCount == 0 && other.gameObject.name == "dTrigger1")
                    {
                        DialogueTriggered(other);
                    }
                    if (DialogueManager.templeIDialogueCount == 1 && other.gameObject.name == "dTrigger2") {
                        DialogueTriggered(other);
                    }
                }
            }
        }
	}

    void DialogueTriggered(Collider other) {
        dialogueBox.SetActive(true);
        Time.timeScale = 0;
        FirstPersonController.isPaused = true;
        canvas.SetActive(false);
        //Destroy(other.gameObject);
    }

	// Update is called once per frame
	void Update () {
        TelePlayerToGate();
        CheckJournalStatus();
        //check if in combat
        if (SceneManager.GetActiveScene().name == "Mimori") {
            if (inCombatCD > 0)
            {
                inCombat = true;
            }
            currfireDelay = fireDelay;
            if (inCombat)
            {
                if (!cSoundPlayed)
                {
                    bgm.clip = combat;
                    bgm.Play();
                    cSoundPlayed = true;
                    bgmPlayed = false;
                }

                inCombatCD -= Time.deltaTime;
            }
            else
            {
                if (!bgmPlayed)
                {
                    cSoundPlayed = false;
                    wSoundPlayed = false;
                    bgmPlayed = true;
                    bgm.clip = originalClip;
                    bgm.Play();
                }
            }

            if (inCombatCD < 0)
            {
                inCombatCD = 0;
                inCombat = false;
            }
        }
        
		if (shootrate > 0) {
			shootrate -= Time.deltaTime;
		}

        //demo code
        if (Input.GetKeyDown(KeyCode.L)) {
            if (canvas.activeSelf) {
                gun.SetActive(false);
                canvas.SetActive(false);
            }

            else
            {
                gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
                gameoverText.canvasRenderer.SetAlpha(0.0f);
                gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
                gun.SetActive(true);
                canvas.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.K)) {
            this.gameObject.GetComponent<Health>().currentHealth = 100;
        }
		if (Input.GetKeyDown (KeyCode.Z)) {
			GameController.gameController.checkSkillPoint = 5;
		}
		if (Input.GetKeyDown (KeyCode.X)) {
            GameController.gameController.checkSkillPoint = this.gameObject.GetComponent<SkillTree> ().skillpointspent;
			this.gameObject.GetComponent<SkillTree> ().LockAllSpell ();
		}

        //--------

		if (isIronSight < 1) {
			//gun.transform.localPosition = Vector3.Lerp (gun.transform.localPosition, new Vector3(0.357f,gun.transform.localPosition.y,gun.transform.localPosition.z), Time.deltaTime * 8);
			//fpc.IronSight = false;
			if (gameObject.GetComponent<SkillTree> ().Gunslinger) {
				fpc.m_WalkSpeed = 13.0f;
				fpc.m_RunSpeed = 18.0f;
			} else if (gameObject.GetComponent<SkillTree> ().Spellslinger) {
				fpc.m_WalkSpeed = 11.0f;
				fpc.m_RunSpeed = 16.0f;
			} else {
				fpc.m_WalkSpeed = 10.0f;
			}
			fpc.IronSight = false;
		} else {
			//gun.transform.localPosition = Vector3.Lerp (gun.transform.localPosition, new Vector3 (0, gun.transform.localPosition.y, gun.transform.localPosition.z), Time.deltaTime * 8);
			//when ironsight , isWalking is enabled
			fpc.m_IsWalking = true;
			fpc.IronSight = true;
			//fpc.m_RunSpeed = 2.0f;
			//Disables The use of Sprinting Calls DisableSprint Function
			//When IronSight Activated , Decrease WalkSpeed
			fpc.m_WalkSpeed = 0.0f;
		}



		///Check If Tab is pressed
		if (Input.GetKeyUp (KeyCode.Tab)) {
			//Debug.Log (isPaused);	
			//Paused Game If it Isn't Paused Yet
			//isPaused = true;
			if (!FirstPersonController.isPaused) {
				Cursor.visible = true;
				curseMode = CursorLockMode.None;
				SkillTreePanel.gameObject.SetActive (true);
				//Freeze Time
				Time.timeScale = 0;
                FirstPersonController.isPaused = true;
			} else {
                FirstPersonController.isPaused = false;
				SkillTreePanel.gameObject.SetActive (false);
				Cursor.visible = false;
				curseMode = CursorLockMode.Locked;
				//unFreeze Time
				Time.timeScale = 1;
			}

		}

		Cursor.lockState = curseMode;

		if (gameObject.GetComponent<FirstPersonController> ().m_IsWalking && gameObject.GetComponent<Player>().isIronSight <= 0) {
			gun.gameObject.GetComponent<Animator> ().SetBool ("isWalking", true);	
			gun.gameObject.GetComponent<Animator> ().SetBool ("isRunning", true);
		}

		if (Input.GetButton ("Fire1") && !gameObject.GetComponent<SkillTree>().sentrymodeactivated) {
			if (shootrate <= 0 && !FirstPersonController.isPaused) {
					if (gameObject.GetComponent<SkillTree> ().leechingbulletactivated) {
						GameObject bullet_leeching = (GameObject)Instantiate (leechingbulletPrefab, gunEnd.position, gunEnd.rotation);
						bullet_leeching.GetComponent<Rigidbody> ().velocity = gunEnd.right * 150;
					} else {
						// Create the Bullet from the Bullet Prefab
						GameObject bullet = (GameObject)Instantiate (bulletPrefab, gunEnd.position, gunEnd.rotation);
						transform.GetChild (0).GetComponent<AudioSource> ().Play ();
				
						// Add velocity to the bullet

						bullet.GetComponent<Rigidbody> ().velocity = gunEnd.right * 150;
					}

			
				// Destroy the bullet after 2 seconds
				//Destroy(bulshoolet, 2.0f);
					shootrate = currfireDelay;
	
			}
					} 

		//if (Input.GetMouseButtonUp (0)) {
		//	gun.gameObject.GetComponent<Animator> ().SetBool ("Firing", false);
		//}

		if (isIronSight <= 0) {
			isShooting = Input.GetButton ("Fire1");
			gun.gameObject.GetComponent<Animator> ().SetBool ("Firing", isShooting);

			if (gameObject.GetComponent<FirstPersonController> ().m_IsStanding) {
				gun.gameObject.GetComponent<Animator> ().SetBool ("isWalking", false);
				gun.gameObject.GetComponent<Animator> ().SetBool ("isRunning", false);
			}

		} 

		if (isIronSight >= 1 && gameObject.GetComponent<SkillTree>().sentrymodeactivated) {
			//Debug.Log (FirstPersonController.isPaused);
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("MOUSE IS HELD DOWN SENTRYMODE");
				if (!FirstPersonController.isPaused) {
					isCharging = true;
				}
				isCharging = true;
			}
			if (Input.GetMouseButton (0)) {
				if (gameObject.GetComponent<SkillTree> ().leechingbulletactivated) {
					GameObject bullet_leeching = (GameObject)Instantiate (leechingbulletPrefab, gunEnd_IronSight.position, gunEnd_IronSight.rotation);
					bullet_leeching.GetComponent<Rigidbody> ().velocity = gunEnd.right * 150;
				}
				if (isCharging) {
					if (!FirstPersonController.isPaused) {
						if (!bulletinstantiate) {
							GameObject ChargeBullet = Instantiate (cannonChargingEffect, gunEnd_IronSight.position, gunEnd.rotation) as GameObject;
							ChargeBullet.name = ChargeBullet.name.Replace ("(Clone)", "");
							ChargeBullet.transform.parent = transform;
							bulletinstantiate = true;	
							//shootrate = 20.0f;
						} 

						//Destroy (ChargeBullet, 1.0f);
					}
					ChargingTime += 1.0f * Time.deltaTime;
				//	shootrate = 6.0f;
					if (ChargingTime >= ChargeFire) {
						foreach (GameObject ChargeBullet in GameObject.FindGameObjectsWithTag("Cannon")) {
							if (ChargeBullet.name == "Charging of the bullet") {
								Destroy (ChargeBullet);
							}
						}
					}
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				ChargingTime = 0f;
				//shootrate = 0f;
				//GameObject bullet_IronSight = (GameObject)Instantiate (bullet_cannonPrefab, gunEnd_IronSight.position, gunEnd_IronSight.rotation);
				//transform.GetChild (0).GetComponent<AudioSource> ().Play ();
				//bullet_IronSight.GetComponent<Rigidbody> ().velocity = gunEnd_IronSight.right * 50;
				//isCharging = false;
				isCharging = false;
				bulletinstantiate = false;
				foreach (GameObject ChargeBullet in GameObject.FindGameObjectsWithTag("Cannon")) {
					if (ChargeBullet.name == "Charging of the bullet") {
						Destroy (ChargeBullet);
					}
				}
			}

			if (ChargingTime >= ChargeFire && isCharging) {
				Destroy (ChargeBullet, 1.0f);
				isCharging = false;
				bulletinstantiate = false;
				GameObject bullet_IronSight = (GameObject)Instantiate (bullet_cannonPrefab, gunEnd_IronSight.position, gunEnd_IronSight.rotation);
				transform.GetChild (0).GetComponent<AudioSource> ().Play ();
				bullet_IronSight.GetComponent<Rigidbody> ().velocity = gunEnd_IronSight.right * 150;

				//Destroy (ChargeBullet);
			}
		}

		//Debug.DrawRay(camera.transform.position, camera.transform.forward * 30, Color.yellow);
		//Debug.DrawRay(gun.transform.position, gun.transform.forward * 30, Color.yellow);
		//ray = camera.ScreenPointToRay(crosshair.transform.position);
		ray = new Ray(camera.transform.position, camera.transform.forward);
		if (Physics.Raycast(ray, out hit)) {
			// Do something with the object that was hit by the raycast.

			if (hit.collider.tag == "InteractObject" && Vector3.Distance (hit.transform.position, this.gameObject.transform.position) < 7) {
				//Debug.Log (" Looking At " + hit.collider.name.ToString());
				tempHit = hit;
				if (tempHit.collider != null && tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>() != null) {
					//tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.shader = outline;
					tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.green);
				}

				if (!interactText.activeSelf) {
					interactText.gameObject.SetActive(true);
				}
				if (Input.GetKeyDown (KeyCode.F)) {
                    if (hit.collider.name == "JournalEntry1")
                    {
                        GameController.gameController.houseTrapActivated = true;
                        GameController.gameController.checkSkillPoint ++;
                        hit.collider.gameObject.SetActive(false);
                    }
                    if (hit.collider.name == "JournalEntry2")
                    {
                        GameController.gameController.journal2Unlocked = true;
                        GameController.gameController.checkSkillPoint++;
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.name == "JournalEntry3")
                    {
                        GameController.gameController.journal3Unlocked = true;
                        GameController.gameController.checkSkillPoint++;
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.name == "JournalEntry4")
                    {
                        GameController.gameController.journal4Unlocked = true;
                        GameController.gameController.checkSkillPoint++;
                        Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.name == "Door_a") {
						hit.collider.GetComponent<Animation> ().Play ();
                        hit.collider.GetComponent<AudioSource>().Play ();
					}
                    if (hit.collider.name == "chest_close") {
                        hit.collider.gameObject.SetActive(false);
                    }
					if (hit.collider.name == "Generator") {
						hit.collider.gameObject.SetActive(false);
						FoMController.timerStart = true;
					}
                    if (hit.collider.name == "SoulOfZoltran") {
                        GameController.gameController.zoltranAbsorbed = true;
                        GameController.gameController.checkSkillPoint++;
                        hit.collider.gameObject.SetActive(false);
                    }
                    if (hit.collider.name == "HeartOfIshira")
                    {
                        GameController.gameController.ishiraAbsorbed = true;
                        GameController.gameController.checkSkillPoint++;
                        hit.collider.gameObject.SetActive(false);
                    }
                    if (hit.collider.name == "PuzzleLever") {
                        PuzzleController.checkPuzzle = true;
                    }
				}
			} else {
				if (tempHit.collider != null && tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>() != null) {
					//tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.shader = unoOutline;
					tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.black);
				}
				if (interactText.activeSelf) {
					interactText.gameObject.SetActive(false);
				}
			}

		}
	}

    void CheckJournalStatus() {
        if (GameController.gameController.houseTrapActivated && !JournalEntryBtn1.interactable) {
            JournalEntryBtn1.interactable = true;
            JournalEntryTXT1.text = "Town Of Telluris";
        }
        if (GameController.gameController.journal2Unlocked && !JournalEntryBtn2.interactable)
        {
            JournalEntryBtn2.interactable = true;
            JournalEntryTXT2.text = "Forest Of Misery";
        }
        if (GameController.gameController.journal3Unlocked && !JournalEntryBtn3.interactable)
        {
            JournalEntryBtn3.interactable = true;
            JournalEntryTXT3.text = "Temple Of Ishira";
        }
        if (GameController.gameController.journal4Unlocked && !JournalEntryBtn4.interactable)
        {
            JournalEntryBtn4.interactable = true;
            JournalEntryTXT4.text = "Lost Crater";
        }
    }

	///Justin's Toggle COde
	/*public void ToggleSentryMode(){
			if (isIronSight < 1) {
				isIronSight++;
			} else {
				isIronSight--;
			}
			if (isIronSight < 1) {
				isIronSight++;
				gun.gameObject.SetActive (false);
				gunEnd.gameObject.SetActive (false);
				gun_IronSight.gameObject.SetActive (true);
				gunEnd_IronSight.gameObject.SetActive (true);
			} else {
				isIronSight--;
				gun.gameObject.SetActive (true);
				gunEnd.gameObject.SetActive (true);
				gun_IronSight.gameObject.SetActive (false);
				gunEnd_IronSight.gameObject.SetActive (false);
			}

		}*/

    void TelePlayerToGate()
    {
        if (SceneManager.GetActiveScene().name == "Temple of Aphelion")
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                transform.position = new Vector3(0, 55, 480);
            }
        }
    }
}
