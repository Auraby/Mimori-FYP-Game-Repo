using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Camera camera;
	public GameObject interactText;
	public GameObject crosshair;
	public GameObject gun;
	public GameObject bulletPrefab;
	public Transform gunEnd;

	RaycastHit hit;
	Ray ray;
	GameObject interactingObj;
	Shader outline;
	Shader normal;
	RaycastHit tempHit;
	// Use this for initialization
	void Start () {
		//interactDistance = Vector3.Distance (interactObj.transform.position, this.gameObject.transform.position);
		normal = Shader.Find ("Standard");
		outline = Shader.Find ("Outlined/Silhouetted Diffuse");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1"))
		{
			// Create the Bullet from the Bullet Prefab
			GameObject bullet = (GameObject)Instantiate(bulletPrefab,gunEnd.position,gunEnd.rotation);

			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody>().velocity = gunEnd.forward * 50;

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
