using UnityEngine;
using System.Collections;

public class FirePillarController : MonoBehaviour {

    public float pillarDamage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Health.instance.currentHealth -= pillarDamage;
        }
    }
}
