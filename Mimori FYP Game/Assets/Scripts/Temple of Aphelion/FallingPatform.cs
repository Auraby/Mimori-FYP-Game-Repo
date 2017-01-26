using UnityEngine;
using System.Collections;

public class FallingPatform : MonoBehaviour {
    float resetCD = 4f;
    float fallingCD = 1f;
    bool startCD = false;
    bool startReset = false;
    Rigidbody rigid;
    Vector3 originalPos;
    Quaternion originalRot;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        originalPos = transform.position;
        originalRot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        //falling codes
        if (startCD) {
            fallingCD -= Time.deltaTime;
        }
        if (fallingCD < 0) {
            fallingCD = 0;
        }
        if (fallingCD <= 0) {
            rigid.useGravity = true;
            rigid.isKinematic = false;
            rigid.AddForce(1, -30, 1);
            startReset = true;
        }
        //reset platforms;
        if (startReset) {
            resetCD -= Time.deltaTime;
        }
        if (resetCD < 0) {
            resetCD = 0;
        }
        if (resetCD <= 0) {
            startReset = false;
            startCD = false;
            fallingCD = 1f;
            resetCD = 4f;
            transform.position = originalPos;
            transform.rotation = originalRot;
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            startCD = true;
        }
    }
}
