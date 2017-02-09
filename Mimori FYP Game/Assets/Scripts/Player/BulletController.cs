using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public GameObject bulletImpact;

    private GameObject player;
	private float liveTime = 1f;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (liveTime >= 0) {
			liveTime -= Time.deltaTime;
		}

		if (liveTime <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
        if(Vector3.Distance(other.transform.position, player.transform.position) <= 40)
        {
            if (this.gameObject.tag != "EnemySkill")
            {
                if (other.gameObject.tag == "MeleeMinion")
                {
                    other.GetComponent<MeleeMinionFSM>().getHit(10);
                    DestroyBullet();
                }
                else if (other.gameObject.tag == "RangeMinion")
                {
                    other.GetComponent<RangeMinionFSM>().getHit(10);
                    DestroyBullet();
                }

                else if (other.gameObject.tag == "HordeMeleeMinion")
                {
                    other.GetComponent<HordeMeleeMinion>().getHit(10);
                    DestroyBullet();
                }
                else if (other.gameObject.tag == "HordeRangeMinion")
                {
                    other.GetComponent<HordeRangeMinion>().getHit(10);
                    DestroyBullet();
                }
                else if (other.gameObject.tag != "DialogueTrigger" && other.gameObject.tag != "InvisibleWall"){
                    GameObject bullet = (GameObject)Instantiate(bulletImpact, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }

        if (this.gameObject.tag == "EnemySkill") {
            if (other.gameObject.tag == "Player") {
                other.GetComponent<Health>().currentHealth -= 2.5f;
				Destroy (this.gameObject);
            }
        }
		
	}

    void DestroyBullet() {
        GameObject bullet = (GameObject)Instantiate(bulletImpact, this.transform.position, Quaternion.identity);
        Player.inCombatCD = 10;
        Destroy(this.gameObject);
    }
}
