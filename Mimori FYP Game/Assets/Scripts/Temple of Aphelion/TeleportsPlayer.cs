using UnityEngine;
using System.Collections;

public class TeleportsPlayer : MonoBehaviour {
    public GameObject player;

    bool cdStart = false;
    float teleCD = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (cdStart) {
            teleCD -= Time.deltaTime;
        }

        if (teleCD < 0) {
            teleCD = 0;
        }

        if (teleCD == 0) {
            player.transform.position = Checkpoint.savedPos;
            teleCD = 3f;
            cdStart = false;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            cdStart = true;
        }
    }
}
