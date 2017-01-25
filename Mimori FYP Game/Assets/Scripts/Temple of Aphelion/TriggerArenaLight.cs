using UnityEngine;
using System.Collections;

public class TriggerArenaLight : MonoBehaviour {
    public GameObject spotLight;

    bool startCD = false;
    float cd = 2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (startCD && cd > 0) {
            cd -= Time.deltaTime;
        }

        if (cd <= 0) {
            LightupSlow.startLight = true;
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            spotLight.SetActive(false);
            startCD = true;
        }
    }
}
