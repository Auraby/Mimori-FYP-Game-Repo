using UnityEngine;
using System.Collections;

public class BulletImpactController : MonoBehaviour {
    float destroyCD = 0.3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (destroyCD > 0) {
            destroyCD -= Time.deltaTime;
        }
        if (destroyCD <= 0) {
            Destroy(this.gameObject);
        }
	}
}
