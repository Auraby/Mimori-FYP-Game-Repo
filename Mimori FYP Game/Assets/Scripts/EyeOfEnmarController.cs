using UnityEngine;
using System.Collections;

public class EyeOfEnmarController : MonoBehaviour {
	private float countDown = 0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (countDown+", "+transform.position.y);
		if (countDown <= 5) {
			countDown += Time.deltaTime;
		}
		if (countDown >= 5 && transform.position.y >= 2) {
			transform.Translate (Vector3.down * 2 * Time.deltaTime);
		}
	}
}
