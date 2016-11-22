using UnityEngine;
using System.Collections;

public class EnmarLaserAoEController : MonoBehaviour {

    public float laserDamage;
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
            //float proximity = (location - player.transform.position).maginitue;
            //float effect = 1 - (proximity / radius);

            //player.health -= (damage * effect);
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AOEDamagePlayer(gameObject.transform.position, laserImpactRadius, laserDamage);
        }
    }
}
