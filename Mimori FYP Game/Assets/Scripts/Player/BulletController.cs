using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public GameObject bulletImpact;
	public double playerbasedamage = 10;
	public double playercurrdamage;

	public float playertakendamage = 2.5f;
	public float playercurrtakendamage;
    private GameObject player;
	//public SkillTree sktree;
	private float liveTime = 1f;
	public int fullychargedbullet = 2;
	public bool fullychargedbulletactivated;
	public bool counterupdateloop = false;
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
		if (player.GetComponent<SkillTree> ().unlockLifePassive1 && player.GetComponent<Health> ().currentHealth < player.GetComponent<Health> ().maxhealth / 2) {
			playercurrdamage = playerbasedamage / 100 * 120;
		} else {
			playercurrdamage = playerbasedamage;
		}
		if (player.GetComponent<SkillTree> ().unlockManaPassive1 && player.GetComponent<Health> ().manabar >= player.GetComponent<Health> ().maxmana) {
			if (fullychargedbullet > 0 && fullychargedbullet <= 2) {
				fullychargedbulletactivated = true;
				playercurrdamage = playerbasedamage * 2;	
			}
		} else {
			if (playercurrdamage == playerbasedamage * 2) {
				playercurrdamage = playerbasedamage;
				fullychargedbulletactivated = false;
				fullychargedbullet = 2;
			}
		}
		if (player.GetComponent<SkillTree> ().unlockPowerPassive1) {
			playerbasedamage = 25;
		}
		if (player.GetComponent<SkillTree> ().thunderrushactivated) {
			playercurrdamage = playerbasedamage + 5;
		} else {
			playercurrdamage = playerbasedamage;
		}

		if (player.GetComponent<SkillTree> ().berserkeractivated) {
			playercurrdamage = playerbasedamage + 15;
			playercurrtakendamage = playertakendamage + 15;
		} else {
			playercurrdamage = playerbasedamage;
			playercurrtakendamage = playertakendamage;
		}
	}
	void OnTriggerEnter(Collider other){
        if(Vector3.Distance(other.transform.position, player.transform.position) <= 40)
        {
            if (this.gameObject.tag != "EnemySkill")
            {
                if (other.gameObject.tag == "MeleeMinion")
                {
                    other.GetComponent<MeleeMinionFSM>().getHit(playercurrdamage);
					if (player.GetComponent<SkillTree>().leechingbulletactivated) {
						player.gameObject.GetComponent<Health> ().manabar += 10;
						player.gameObject.GetComponent<Health> ().manabarslider.value += 10;
					}
					if (fullychargedbulletactivated) {
						fullychargedbullet--;
					}
                    DestroyBullet();
                }
                else if (other.gameObject.tag == "RangeMinion")
                {
					other.GetComponent<RangeMinionFSM>().getHit(playercurrdamage);
					if (player.GetComponent<SkillTree>().leechingbulletactivated) {
						player.gameObject.GetComponent<Health> ().manabar += 10;
						player.gameObject.GetComponent<Health> ().manabarslider.value += 10;
					}
					if (fullychargedbulletactivated) {
						fullychargedbullet--;
					}
                    DestroyBullet();
                }

                else if (other.gameObject.tag == "HordeMeleeMinion")
                {
					other.GetComponent<HordeMeleeMinion>().getHit(playercurrdamage);
					if (player.GetComponent<SkillTree>().leechingbulletactivated) {
						player.gameObject.GetComponent<Health> ().manabar += 10;
						player.gameObject.GetComponent<Health> ().manabarslider.value += 10;
					}
					if (fullychargedbulletactivated) {
						fullychargedbullet--;
					}
                    DestroyBullet();
                }
                else if (other.gameObject.tag == "HordeRangeMinion")
                {
					other.GetComponent<HordeRangeMinion>().getHit(playercurrdamage);
					if (player.GetComponent<SkillTree>().leechingbulletactivated) {
						player.gameObject.GetComponent<Health> ().manabar += 10;
						player.gameObject.GetComponent<Health> ().manabarslider.value += 10;
					}
					if (fullychargedbulletactivated) {
						fullychargedbullet--;
					}
                    DestroyBullet();
                }
				else if(other.gameObject.tag == "InvisibleWall" && other.gameObject.tag == "DialogueTrigger"){
                    GameObject bullet = (GameObject)Instantiate(bulletImpact, this.transform.position, Quaternion.identity);
					if (fullychargedbulletactivated) {
						fullychargedbullet--;
					}
					Destroy(this.gameObject);
                }
            }
        }

        if (this.gameObject.tag == "EnemySkill") {
            if (other.gameObject.tag == "Player") {
                other.GetComponent<Health>().currentHealth -= playercurrtakendamage;
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
