using UnityEngine;
using System.Collections;

public class OutpostManager : MonoBehaviour {
	public GameObject[] minions;
	public Mesh captured;

	private bool isCaptured = false;
	private bool allDied = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!isCaptured) {
			for (int i = 0; i < minions.Length; i++) {
				allDied = false;
				if (minions [i] == null) {
					allDied = true;
				} else {
					break;
				}
			}

			if (allDied) {
				GetComponent<MeshFilter> ().mesh = captured;
				isCaptured = true;
			}
		}
	}
}
