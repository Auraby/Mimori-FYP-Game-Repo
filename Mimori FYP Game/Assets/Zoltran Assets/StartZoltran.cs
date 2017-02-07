using UnityEngine;
using System.Collections;

public class StartZoltran : MonoBehaviour {
    public GameObject entranceBarrier, templeBarrier,zoltran;

    public bool zoltranDied = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        if(zoltranDied == true)
        {
            entranceBarrier.SetActive(false);
            templeBarrier.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            entranceBarrier.gameObject.SetActive(true);
            templeBarrier.gameObject.SetActive(true);
            zoltran.gameObject.SetActive(true);
        }
    }
}
