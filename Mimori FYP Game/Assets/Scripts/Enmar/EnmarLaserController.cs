using UnityEngine;
using System.Collections;

public class EnmarLaserController : MonoBehaviour {


    ParticleSystem part;

    //The empty gameobject that contains the sphere collider
    public GameObject laserBlastImpact;

    //Instantiate clone
    GameObject LBI;

    public float laserResideTime;

    bool isLaserHit = false;
    [HideInInspector]
    public bool isSpawnTrigger = false;

    public static EnmarLaserController instance { get; set; }
    // Use this for initialization
    void Start () {
        instance = this;
        part = gameObject.GetComponent<ParticleSystem>();
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
           
            if(isSpawnTrigger == false)
            {
                LBI = (GameObject)Instantiate(laserBlastImpact, other.transform.position, other.transform.rotation);
                isSpawnTrigger = true;
            }
            
            Debug.Log("Hit Floor");
        }

        isLaserHit = true;     
        

    }

   
}
