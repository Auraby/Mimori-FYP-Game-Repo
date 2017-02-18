using UnityEngine;
using System.Collections;

public class ForestEnvironmentalSound : MonoBehaviour {
    bool soundPlayed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (!soundPlayed) {
                GetComponent<AudioSource>().Play();
                soundPlayed = true;
            }
        }
    }
}
