using UnityEngine;
using System.Collections;

public class StartZoltran : MonoBehaviour {
    public GameObject entranceBarrier, templeBarrier,zoltran;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            entranceBarrier.gameObject.SetActive(true);
            templeBarrier.gameObject.SetActive(true);
            zoltran.gameObject.SetActive(true);
        }
    }
}
