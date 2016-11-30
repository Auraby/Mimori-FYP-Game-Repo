using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnmarLaserAoEController : MonoBehaviour {

    public float laserDamageOverTime;
    public float laserImpactRadius;

    public static EnmarLaserAoEController instance { get; set; }
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AOEDamagePlayer(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            //whatever that contains health here
            //"Something".instance."h
            

            //linear falloff effect
            float proximity = (location - FirstPersonController.instance.gameObject.transform.position).magnitude;
            float effect = 1 - (proximity / radius);

            Health.instance.currentHealth -= (damage * effect);

        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //AOEDamagePlayer(gameObject.transform.position, laserImpactRadius, laserDamageOverTime);
            Health.instance.currentHealth -= laserDamageOverTime;
            Debug.Log("Player inside circle");
        }
    }
}
