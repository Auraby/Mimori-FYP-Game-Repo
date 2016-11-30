using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {
	public Camera camera;
	public GameObject interactText;
	public GameObject crosshair;
	public GameObject gun;
	public GameObject bulletPrefab;
	public Transform gunEnd;
	public Image eoeImg;

	public GameObject eoe;
	public GameObject eoeParticle;

	//IronSights
	//[Header("Iron Sights")]
	public GameObject gun_IronSight;
	public Transform  gunEnd_IronSight;
	public int isIronSight;

	//objectives
	public Text Objective;

	//SkillTree /Pause Game
	public bool isPaused;
	public Image SkillTreePanel;
	[HideInInspector]
	public CursorLockMode curseMode;
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
	RaycastHit tempHit;
	float shootDelay = 0;

	// Use this for initialization
	void Start () {
		//Continue Player
		if(GameController.loadingGame){
			transform.position = new Vector3 (
				GameController.gameController.playerPositionX,
				GameController.gameController.playerPositionY,
				GameController.gameController.playerPositionZ
			);
		}

		isIronSight = 0;
		isPaused = false;
		Cursor.visible = false;
		curseMode = CursorLockMode.Locked;
		//interactDistance = Vector3.Distance (interactObj.transform.position, this.gameObject.transform.position);
		normal = Shader.Find ("Standard");
		outline = Shader.Find ("Outlined/Silhouetted Diffuse");
		fpc = GameObject.FindObjectOfType<FirstPersonController> ();
	}

	public void OnTriggerEnter(Collider obj){
		if (obj.gameObject.tag == "Objective2") {
			Objective.text = "Kill 10 Mobs and obtain skillpoint".ToString ();
		}
		if (obj.gameObject.tag == "Objective1") {
			Objective.text = "Complete puzzle to unlock hidden treasure".ToString ();
		}
	}



	// Update is called once per frame
	void Update () {
		if (shootDelay > 0) {
			shootDelay -= Time.deltaTime;
		}
		if (Input.GetMouseButtonDown (2)) {
			if (isIronSight < 1) {
				isIronSight++;
			} else {
				isIronSight--;
			}
			/*if (isIronSight < 1) {
				isIronSight ++;
				gun.gameObject.SetActive (false);
				gunEnd.gameObject.SetActive (false);
				gun_IronSight.gameObject.SetActive (true);
				gunEnd_IronSight.gameObject.SetActive (true);
			} else {
				isIronSight --;
				gun.gameObject.SetActive (true);
				gunEnd.gameObject.SetActive (true);
				gun_IronSight.gameObject.SetActive (false);
				gunEnd_IronSight.gameObject.SetActive (false);
			}*/

		}

		if (isIronSight < 1) {
			gun.transform.localPosition = Vector3.Lerp (gun.transform.localPosition, new Vector3(0.357f,gun.transform.localPosition.y,gun.transform.localPosition.z), Time.deltaTime * 8);
			fpc.m_WalkSpeed = 5.0f;
			fpc.IronSight = false;
		} else {
			gun.transform.localPosition = Vector3.Lerp (gun.transform.localPosition, new Vector3 (0, gun.transform.localPosition.y, gun.transform.localPosition.z), Time.deltaTime * 8);
			//when ironsight , isWalking is enabled
			fpc.m_IsWalking = true;
			fpc.IronSight = true;
			//fpc.m_RunSpeed = 2.0f;
			//Disables The use of Sprinting Calls DisableSprint Function
			//When IronSight Activated , Decrease WalkSpeed
			fpc.m_WalkSpeed = 2.0f;
		}


		///Check If Tab is pressed
		if (Input.GetKeyUp (KeyCode.Tab)) {
			Debug.Log (isPaused);	
			//Paused Game If it Isn't Paused Yet
			//isPaused = true;
			if (isPaused == false) {
				Cursor.visible = true;
				curseMode = CursorLockMode.None;
				SkillTreePanel.gameObject.SetActive (true);
				//Freeze Time
				Time.timeScale = 0;
				isPaused = true;
			} else {
				isPaused = false;
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
			if (shootDelay <= 0) {
				//Check if Player has enabled IronSight
				if (isIronSight >= 1) {
					GameObject bullet_IronSight = (GameObject)Instantiate (bulletPrefab, gunEnd_IronSight.position, gunEnd_IronSight.rotation);
					bullet_IronSight.GetComponent<Rigidbody> ().velocity = gunEnd_IronSight.forward * 50;
				} else {
					// Create the Bullet from the Bullet Prefab
					GameObject bullet = (GameObject)Instantiate (bulletPrefab, gunEnd.position, gunEnd.rotation);

					// Add velocity to the bullet
					bullet.GetComponent<Rigidbody> ().velocity = gunEnd.forward * 150;
				}
				// Destroy the bullet after 2 seconds
				//Destroy(bulshoolet, 2.0f);
				shootDelay = 0.2f;
			}

		}

		//Debug.DrawRay(camera.transform.position, camera.transform.forward * 30, Color.yellow);
		//Debug.DrawRay(gun.transform.position, gun.transform.forward * 30, Color.yellow);
		ray = camera.ScreenPointToRay(crosshair.transform.position);
		if (Physics.Raycast(ray, out hit)) {
			// Do something with the object that was hit by the raycast.

			if (hit.collider.tag == "InteractObject" && Vector3.Distance (hit.transform.position, this.gameObject.transform.position) < 5) {
				//Debug.Log (" Looking At " + hit.collider.name.ToString());
				tempHit = hit;
				if (tempHit.collider != null) {
					tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.shader = outline;
				}

				if (!interactText.active) {
					interactText.active = true;
				}
				if (Input.GetKeyDown (KeyCode.F)) {
					if (hit.collider.name == "enmarDead") {
						eoeParticle.SetActive (true);
						eoe.SetActive (true);
						hit.collider.GetComponent<FadeObjectInOut> ().FadeOut (4f);
						hit.collider.tag = "Untagged";
					}

					if (hit.collider.name == "EoE") {
						eoe.SetActive (false);
						eoeImg.gameObject.SetActive (true);
					}

					if (hit.collider.name == "Door_a") {
						hit.collider.GetComponent<Animation> ().Play ();
					}
				}
			} else {
				if (tempHit.collider != null) {
					tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.shader = normal;
				}
				if (interactText.active) {
					interactText.active = false;
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
}
