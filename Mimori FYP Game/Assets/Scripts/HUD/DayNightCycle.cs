using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	Material sky;

	public Transform stars;
	//public Transform worldprobe;

	// Use this for initialization
	void Start () {
		sky = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () {
		//stars.transform.rotation = transform.rotation;
		transform.RotateAround (Vector3.zero, Vector3.right, Time.deltaTime * 1.5f);
		transform.LookAt (Vector3.zero);
	}
}
