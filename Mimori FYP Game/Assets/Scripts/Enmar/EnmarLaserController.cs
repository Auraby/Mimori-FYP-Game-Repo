using UnityEngine;
using System.Collections;

public class EnmarLaserController : MonoBehaviour {


    ParticleSystem part;

    //The empty gameobject that contains the sphere collider
    public GameObject laserBlastImpact;

    //Instantiate clone
    GameObject LBI;

    public static EnmarLaserController instance { get; set; }
    // Use this for initialization
    void Start () {
        instance = this;
        part = gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {

        //if (LBI.GetComponent<SphereCollider>().radius < 3)
        //{
        //    LBI.GetComponent<SphereCollider>().radius += Time.deltaTime;
        //}

        //var subEmitter = part.subEmitters;
        //if (subEmitter.collision1.isStopped == true)
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
        }

        var ps = part.subEmitters;
        //LBI = (GameObject)Instantiate(laserBlastImpact, ps.collision1.transform.position, ps.collision1.transform.rotation,gameObject.transform);

        
        

    }
}
