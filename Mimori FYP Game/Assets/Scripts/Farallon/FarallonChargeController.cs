using UnityEngine;
using System.Collections;

public class FarallonChargeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if(FarallonController.instance.currGroundState == FarallonController.GroundStates.Charge)
        {
            if(other.gameObject.tag == "Player")
            {
                Health.instance.currentHealth -= FarallonController.instance.chargeDamage;
                Health.instance.healthbarslider.value -= FarallonController.instance.chargeDamage;
            }
        }
    }
}
