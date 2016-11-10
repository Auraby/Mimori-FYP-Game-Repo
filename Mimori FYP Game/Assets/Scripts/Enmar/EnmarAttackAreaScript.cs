using UnityEngine;
using System.Collections;

public class EnmarAttackAreaScript : MonoBehaviour {

    public enum areaName { Area1, Area2 }

    public areaName aName;

    ParticleSystem part;

    public static EnmarAttackAreaScript instance { get; set; }

    // Use this for initialization
    void Start () {
        instance = this;
        part = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayerEnteredArea1()
    {
        EnmarController.instance.hasEnteredArea1 = true;
        EnmarController.instance.hasEnteredArea2 = false;
    }

    public void PlayerEnteredArea2()
    {
        EnmarController.instance.hasEnteredArea2 = true;
        EnmarController.instance.hasEnteredArea1 = false;
    }

    public void resetBool()
    {
        EnmarController.instance.hasEnteredArea2 = false;
        EnmarController.instance.hasEnteredArea1 = false;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            switch (aName)
            {
                case areaName.Area1:
                    {
                        PlayerEnteredArea1();
                    }
                    break;
                case areaName.Area2:
                    {
                        PlayerEnteredArea2();
                    }
                    break;

                default:
                    break;
            }
        }
    }

    public void OnParticleCollision(GameObject other)
    {
       // Debug.Log("Hit player");

        if(other.gameObject.tag == "Player")
        {
            var ps = part.subEmitters;
            ps.collision1.maxParticles = 0;
            Debug.Log("Hit Player");
        }
    }

  
}
