using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnmarLaserController : MonoBehaviour {


    ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    //The empty gameobject that contains the sphere collider
    public GameObject laserBlastImpact;

    //Instantiate clone
    GameObject LBI;

    public float laserResideTime;

    bool isLaserHit = false;
    [HideInInspector]
    public bool isSpawnTrigger = false;

    [HideInInspector]
    public Vector3 laserHitPos;

    public static EnmarLaserController instance { get; set; }
    // Use this for initialization
    void Start () {
        instance = this;
        part = gameObject.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
	
	// Update is called once per frame
	void Update () {

        if(LBI != null)
        {
            if (LBI.GetComponent<SphereCollider>().radius < 3)
            {
                LBI.GetComponent<SphereCollider>().radius += Time.deltaTime;
            }
        }
       

        var subEmitter = part.subEmitters;
        if (subEmitter.collision1.isStopped == true)
        {
            Destroy(LBI);
        }


        if (isLaserHit == true)
        {
            laserResideTime += Time.deltaTime;

            if (laserResideTime > 4)
            {
                laserResideTime = 0;
                isLaserHit = false;
                Destroy(EnmarController.instance.laserBeamGO);
            }
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        // Debug.Log("Hit player");
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            laserHitPos = collisionEvents[i].intersection;

            if (other.gameObject.tag == "Player")
            {
                var pse = part.subEmitters;
                pse.collision1.maxParticles = 0;
                Debug.Log("Hit Player");
            }

            else
            {
                var pse = part.subEmitters;
                pse.collision1.maxParticles = 100;

                if (isSpawnTrigger == false)
                {
                    LBI = (GameObject)Instantiate(laserBlastImpact, laserHitPos, other.transform.rotation);
                    isSpawnTrigger = true;
                }

                Debug.Log("Hit Floor");
            }

            isLaserHit = true;

            i++;
        }

        
        

    }

   
}
