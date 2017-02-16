using UnityEngine;
using System.Collections;

public class FarallonSlamColliderDetection : MonoBehaviour {

    public float slamShockWaveDamage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Health.instance.currentHealth -= slamShockWaveDamage;
            Health.instance.healthbarslider.value -= slamShockWaveDamage;
        }
    }
}
