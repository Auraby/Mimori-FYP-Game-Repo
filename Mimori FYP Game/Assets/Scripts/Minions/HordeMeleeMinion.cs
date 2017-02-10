//#define LOG_TRACE_INFO
//#define LOG_EXTRA_INFO

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HordeMeleeMinion : MonoBehaviour
{
	//public
	public AnimationClip run, die, hitted;

	GameObject player;

	public AnimationClip attackClip;
	public float attackRange;
	
	public double impactTime = 0.36;
	
	public int maxHealth;
	public int  health;
	int damage = 5;

	//private
	private enum State { Chase };
	private State currentState;
	private bool impacted;

	//private Fighter opponent;
	private int stunTime;

	private bool playerHitted = false;
	private Vector3 goalPoint;
	private float countDown;
	//GameObject player;
	private NavMeshAgent nav;
	private Animation anim;
	
	private const float ARRIVE_AT_GOAL = 3f;
	private const float TURN_LIMIT = 60;
	private const float ANIM_DAMP = 0.5f;

    private bool attackAnimPlayed = false;

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
		// start AI
		currentState = State.Chase;
		
	}

	// Update is called once per frame
	void Update () 
	{
        player = GameObject.FindGameObjectWithTag("Player");
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
					anim.Play (run.name);
					MoveToward (player.transform.position);
					goalPoint = player.transform.position;
				} else {
					GetComponent<Animation> ().Play (attackClip.name);
					attack ();
					transform.LookAt (player.transform);
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
		if (GetComponent<Animation>() [attackClip.name].time > GetComponent<Animation>() [attackClip.name].length * impactTime &&
            !impacted &&
            GetComponent<Animation>()[attackClip.name].time<0.9*GetComponent<Animation>()[attackClip.name].length) 
		{
            //opponent.getHit(damage);
			if (player.GetComponent<SkillTree> ().manashieldup) {
				player.GetComponent<Health>().currentHealth -= 0;
			} else {
				player.GetComponent<Health>().currentHealth -= damage;
			}
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
        anim.Stop(run.name);
        anim.Stop(attackClip.name);
        anim.Play(hitted.name);
		this.gameObject.GetComponent<NavMeshAgent> ().Stop ();
	}
	
	void stunCountDown()
	{
		stunTime = stunTime - 1;
		
		if(stunTime==0)
		{
			this.gameObject.GetComponent<NavMeshAgent> ().Resume ();
			CancelInvoke("stunCountDown");
		}
	}
	
	void dieMethod()
	{
		GetComponent<NavMeshAgent> ().Stop ();
		GetComponent<Animation>().Play (die.name);
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
