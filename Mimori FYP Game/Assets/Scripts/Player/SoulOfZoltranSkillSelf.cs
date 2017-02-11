using UnityEngine;
using System.Collections;

public class SoulOfZoltranSkillSelf : MonoBehaviour {
    float duration = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if (duration <= 0) {
            GameObject.Find("FPSController (1)").tag = "Player";
            Destroy(this.gameObject);
        }
	}
}
