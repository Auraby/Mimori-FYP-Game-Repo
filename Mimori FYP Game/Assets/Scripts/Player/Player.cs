using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public Camera camera;
	public GameObject interactText;
	public GameObject crosshair;
	public GameObject gun;
	public GameObject bulletPrefab;
	public Transform gunEnd;

	//IronSights
	public GameObject gun_IronSight;
	public Transform  gunEnd_IronSight;
	public int isIronSight;

	//objectives
	public Text Objective;


	RaycastHit hit;
	Ray ray;
	GameObject interactingObj;
	Shader outline;
	Shader normal;
	RaycastHit tempHit;
	// Use this for initialization
	void Start () {
		isIronSight = 0;
		//interactDistance = Vector3.Distance (interactObj.transform.position, this.gameObject.transform.position);
		normal = Shader.Find ("Standard");
		outline = Shader.Find ("Outlined/Silhouetted Diffuse");
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
		if (Input.GetMouseButtonDown (2)) {
			if (isIronSight < 1) {
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
			}
		}

		if (Input.GetButtonDown ("Fire1"))
		{
			//Check if Player has enabled IronSight
			if (isIronSight >= 1) {
				GameObject bullet_IronSight = (GameObject)Instantiate (bulletPrefab, gunEnd_IronSight.position, gunEnd_IronSight.rotation);
				bullet_IronSight.GetComponent<Rigidbody> ().velocity = gunEnd_IronSight.forward * 50;
			} else {
				// Create the Bullet from the Bullet Prefab
				GameObject bullet = (GameObject)Instantiate (bulletPrefab, gunEnd.position, gunEnd.rotation);

				// Add velocity to the bullet
				bullet.GetComponent<Rigidbody> ().velocity = gunEnd.forward * 50;
			}
			// Destroy the bullet after 2 seconds
			//Destroy(bullet, 2.0f);   
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
			} else {
				if (tempHit.collider != null) {
					tempHit.collider.GetComponent<Collider>().GetComponent<Renderer>().material.shader = normal;
				}
				if (interactText.active) {
					interactText.active = false;
				}
			}

		}

	}
}
