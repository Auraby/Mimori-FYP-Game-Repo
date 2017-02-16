using UnityEngine;
using System.Collections;

public class BillBoardTazo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Camera.main.transform.rotation;
	}
}

