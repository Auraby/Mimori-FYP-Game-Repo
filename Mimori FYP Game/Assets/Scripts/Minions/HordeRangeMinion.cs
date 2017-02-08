using UnityEngine;
using System.Collections;

public class HordeRangeMinion : MonoBehaviour {

	//public
	public AnimationClip run, die;

	//public float speed;
	GameObject player;

	public AnimationClip attackClip;
	public float attackRange;

	public double impactTime = 0.36;

	public int maxHealth;
	public int  health;
	public int damage;

	public GameObject bulletPrefab;
	public Transform shootPoint;

	//private
	private enum State { Chase };
	private State currentState;
	private bool impacted;
	//private Fighter opponent;
	private int stunTime;

	//private Animator anim;
	private bool playerHitted = false;
	private Vector3 goalPoint;
	private float countDown;
	//GameObject player;
	private NavMeshAgent nav;
	private Animation anim;

	private const float ARRIVE_AT_GOAL = 10f;
	private const float TURN_LIMIT = 60;
	private const float ANIM_DAMP = 0.5f;

	// Use this for initialization
	void Start () 
	{
		//added code
		maxHealth = 100;
		health = maxHealth;
		//opponent = player.GetComponent<Fighter> ();
		//end added code
		anim = GetComponent<Animation> ();
		nav = GetComponent<NavMeshAgent> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		// start AI
	}

	// Update is called once per frame
	void Update () 
	{
		if (currentState == State.Chase)
		{
			DoChase();
		}
		if (currentState != State.Chase && player != null)
		{
			Vector3 direction = player.transform.position - transform.position;
			currentState = State.Chase;
		}
	}

	private void DoChase()
	{
		if (!isDead ())
		{
			if(stunTime<=0)
			{
				if (!inAttackRange ()) {
					GetComponent<NavMeshAgent> ().Resume ();
					anim.Play (run.name);
					MoveToward (player.transform.position);
					goalPoint = player.transform.position;
				} else {
					GetComponent<Animation> ().Play (attackClip.name);
					GetComponent<NavMeshAgent> ().Stop ();
					transform.LookAt (player.transform);
					attack ();
					if (GetComponent<Animation> () [attackClip.name].time > 0.9 * GetComponent<Animation> () [attackClip.name].length) {
						impacted = false;
					}
				}

			}
		}
		else
		{
			dieMethod();
		}
	}

	bool MoveToward(Vector3 goal)
	{
		Vector3 direction = goal - gameObject.transform.position;
		nav.SetDestination (goal);
		return direction.magnitude < ARRIVE_AT_GOAL;
	}

	//added code
	void attack()
	{
		if (GetComponent<Animation>() [attackClip.name].time > GetComponent<Animation>() [attackClip.name].length * impactTime&&!impacted&&GetComponent<Animation>()[attackClip.name].time<0.9*GetComponent<Animation>()[attackClip.name].length) 
		{
			GameObject bullet = (GameObject)Instantiate(bulletPrefab,shootPoint.position,shootPoint.rotation);

			// Add velocity to the bullet
			bullet.transform.LookAt(player.transform);
			bullet.GetComponent<Rigidbody>().velocity = shootPoint.right * 50;
            //bullet.transform.position = Vector3.MoveTowards(shootPoint.position, player.transform.position, Time.deltaTime * 10);
            //opponent.getHit(damage);
            //player.GetComponent<Health>().currentHealth -= damage/2;
            impacted = true;
		}
	}

	public void getHit(double damage)
	{
		health = health - (int)damage;

		if(health<0)
		{
			health = 0;
		}
	}

	public void getStun(int seconds)
	{
		CancelInvoke("stunCountDown");
		stunTime += seconds +1;
		InvokeRepeating("stunCountDown", 0f, 1f);
	}

	void stunCountDown()
	{
		Debug.Log(stunTime);
		stunTime = stunTime - 1;

		if(stunTime==0)
		{
			CancelInvoke("stunCountDown");
		}
	}

	void dieMethod()
	{
		GetComponent<Animation>().Play (die.name);
		GetComponent<NavMeshAgent> ().Stop ();
		if(GetComponent<Animation>()[die.name].time>GetComponent<Animation>()[die.name].length*0.9)
		{
			FoMController.minionCount--;
			Destroy(gameObject);
		}
	}

	bool isDead()
	{
		if (health <= 0)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	//	void OnMouseOver()
	//	{
	//		player.GetComponent<Fighter>().opponent = gameObject;
	//	}

	bool inAttackRange()
	{
		if(Vector3.Distance(transform.position, player.transform.position)<attackRange)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
