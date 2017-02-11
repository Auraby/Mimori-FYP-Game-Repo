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
    public GameObject muzzleFlash;
	public Transform gunEnd;
	public Image eoeImg;

	public GameObject eoe;
	public GameObject eoeParticle;

    public GameObject dialogueBox;

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
	public float fireDelay = 0.6f;
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
                        if (DialogueManager.enmarDialogueCount == 2 && Level1Controller.enmarAbsorbed)
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
        //--------

		if (isIronSight < 1) {
			//gun.transform.localPosition = Vector3.Lerp (gun.transform.localPosition, new Vector3(0.357f,gun.transform.localPosition.y,gun.transform.localPosition.z), Time.deltaTime * 8);
			//fpc.m_WalkSpeed = 5.0f;
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

		if (Input.GetButton ("Fire1"))
		{
			if (shootrate <= 0 && !FirstPersonController.isPaused) {
				//Check if Player has enabled IronSight
				if (isIronSight >= 1) {
					GameObject bullet_IronSight = (GameObject)Instantiate (bulletPrefab, gunEnd_IronSight.position, gunEnd_IronSight.rotation);
					bullet_IronSight.GetComponent<Rigidbody> ().velocity = gunEnd_IronSight.right * 150;
				} else {
					// Create the Bullet from the Bullet Prefab
					GameObject bullet = (GameObject)Instantiate (bulletPrefab, gunEnd.position, gunEnd.rotation);
                    //GameObject muzzle = (GameObject)Instantiate(muzzleFlash, gunEnd.position, gunEnd.rotation,gunEnd);
                    transform.GetChild(0).GetComponent<AudioSource>().Play();
					// Add velocity to the bullet
					bullet.GetComponent<Rigidbody> ().velocity = gunEnd.right * 150;
				}
				// Destroy the bullet after 2 seconds
				//Destroy(bulshoolet, 2.0f);
				shootrate = currfireDelay;
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
				if (tempHit.collider != null) {
					//tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.shader = outline;
					tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.green);
				}

				if (!interactText.activeSelf) {
					interactText.gameObject.SetActive(true);
				}
				if (Input.GetKeyDown (KeyCode.F)) {
					if (hit.collider.name == "enmarDead") {
						eoeParticle.SetActive (true);
						eoe.SetActive (true);
						//hit.collider.GetComponent<FadeObjectInOut> ().FadeOut (4f);
						hit.collider.GetComponent<FadeObjectInOut>().interacted = true;
						hit.collider.tag = "Untagged";
					}

					if (hit.collider.name == "EoE") {
						eoe.SetActive (false);
						eoeImg.gameObject.SetActive (true);
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
				}
			} else {
				if (tempHit.collider != null) {
					//tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.shader = unoOutline;
					tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.black);
				}
				if (interactText.activeSelf) {
					interactText.gameObject.SetActive(false);
				}
			}

		}



		//Save Load Test
		if(Input.GetKeyDown(KeyCode.F5)){
			GameController.gameController.playerPositionX = transform.position.x;
			GameController.gameController.playerPositionY = transform.position.y;
			GameController.gameController.playerPositionZ = transform.position.z;

			GameController.gameController.Save ();
		}

		if(Input.GetKeyDown(KeyCode.F9)){
			GameController.gameController.Load ();
			transform.position = new Vector3 (
				GameController.gameController.playerPositionX,
				GameController.gameController.playerPositionY,
				GameController.gameController.playerPositionZ
			);
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

}
