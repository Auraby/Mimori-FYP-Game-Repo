using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireBreathController : MonoBehaviour {

    ParticleSystem fireParticleSys;
    [HideInInspector]
    public List<ParticleCollisionEvent> collisionEvents;

    public float fireBreathDamage = 2;

	// Use this for initialization
	void Start () {
        fireParticleSys = gameObject.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnParticleCollision(GameObject other)
    {
        // Debug.Log("Hit player");
        int numCollisionEvents = fireParticleSys.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
           // laserHitPos = collisionEvents[i].intersection;

            if (other.gameObject.tag == "Player")
            {
                Health.instance.currentHealth -= fireBreathDamage;
                Debug.Log("Hit Player");
            }

            i++;
        }

    }
}
