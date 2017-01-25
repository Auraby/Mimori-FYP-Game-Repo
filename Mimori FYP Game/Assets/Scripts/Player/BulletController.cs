using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
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
            if (other.gameObject.tag == "MeleeMinion")
            {
                other.GetComponent<MeleeMinionFSM>().getHit(10);
                Destroy(this.gameObject);
                Player.inCombatCD = 10;
            }
            if (other.gameObject.tag == "RangeMinion")
            {
                other.GetComponent<RangeMinionFSM>().getHit(10);
                Destroy(this.gameObject);
                Player.inCombatCD = 10;
            }

			if (other.gameObject.tag == "HordeMeleeMinion")
			{
				other.GetComponent<HordeMeleeMinion>().getHit(10);
				Destroy(this.gameObject);
                Player.inCombatCD = 10;
            }
			if (other.gameObject.tag == "HordeRangeMinion")
			{
				other.GetComponent<HordeRangeMinion>().getHit(10);
				Destroy(this.gameObject);
                Player.inCombatCD = 10;
            }
        }

        if (this.gameObject.tag == "EnemySkill") {
            if (other.gameObject.tag == "Player") {
                other.GetComponent<Health>().currentHealth -= 2.5f;
				Destroy (this.gameObject);
            }
        }
		
	}
}
