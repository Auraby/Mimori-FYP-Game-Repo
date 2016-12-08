﻿//#define LOG_TRACE_INFO
//#define LOG_EXTRA_INFO

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeMinionFSM : MonoBehaviour
{
	//public
	public float detectRange = 10;
	public float detectFOV = 360;
	public AnimationClip idle, run, walk, die;

	//public float speed;
	public Transform player;

	public AnimationClip attackClip;
	public float attackRange;
	public float boundRange;
	
	public double impactTime = 0.36;
	
	public int maxHealth;
	public int  health;
	int damage = 5;
	
	public GameObject[] patrolPoints;
	public GameObject boundPoint;

	//private
	private enum State { Patrol, Idle, Chase };
	private State currentState;
	private bool impacted;
	private bool goBack = false;

	//private Fighter opponent;
	private int stunTime;

	private float restTime;
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
		PickState();
	}

	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (Vector3.Distance (transform.position, boundPoint.transform.position));
		//Debug.Log(currentState);
		if (Vector3.Distance (transform.position, boundPoint.transform.position) > boundRange) {
			goBack = true;
		} else if(Vector3.Distance (transform.position, boundPoint.transform.position) < boundRange/2){
			goBack = false;
		}

		if (goBack) {
			currentState = State.Patrol;
		}

		if (playerHitted == true) {
			restTime -= Time.deltaTime;
		}
		if (currentState == State.Idle)
		{
			DoIdle();
		}
		else if (currentState == State.Patrol)
		{
			DoPatrol();
		}
		else if (currentState == State.Chase)
		{
			DoChase();
		}
		if (currentState != State.Chase && player != null)
		{
			Vector3 direction = player.transform.position - transform.position;
			bool inRange=direction.magnitude < detectRange;
			bool inFOV = Vector3.Angle(direction, transform.forward) < detectFOV;
			if (inRange && inFOV)
			{
				currentState = State.Chase;
			}
		}
	}
	
	private void DoChase()
	{
		if (!isDead ())
		{
			if(stunTime<=0)
			{
				if (!goBack) {
					if(!inAttackRange())
					{
						anim.Play (run.name);
						MoveToward(player.transform.position);
						if (Vector3.Distance(player.transform.position, transform.position) > detectRange * 1.2f)
						{
							currentState = State.Patrol;
							goalPoint = player.transform.position;
						}
					}
					else
					{
						GetComponent<Animation>().Play(attackClip.name);
						attack();
						transform.LookAt(player);
						if(GetComponent<Animation>()[attackClip.name].time>0.9*GetComponent<Animation>()[attackClip.name].length)
						{
							impacted = false;
						}

					}
				}
			}
		}
		else
		{
			dieMethod();
		}
	}
	private void DoPatrol()
	{
        if (goBack)
        {
            anim.Play(run.name);
        }
        else {
            anim.Play(walk.name);
        }
		
		// move towards:
		if (MoveToward(goalPoint))
		{
			PickState();
		}
	}
	
	private void DoIdle()
	{
		anim.Play (idle.name);
		countDown -= Time.deltaTime;
		if (countDown <= 0)
		{
			PickState();
		}
	}
	
	bool MoveToward(Vector3 goal)
	{
		Vector3 direction = goal - gameObject.transform.position;
		if (restTime <= 0f) {
			nav.SetDestination (goal);
		}
		return direction.magnitude < ARRIVE_AT_GOAL;
	}
	
	void PickState()
	{
		if (Random.Range(0, 2) == 0)
		{
			currentState = State.Patrol;
			goalPoint = GetPatrolPoint();
		}
		else
		{
			currentState = State.Idle;
			countDown = Random.Range(1.0f, 3.0f);
		}
	}
	Vector3 GetPatrolPoint()
	{
		int r = Random.Range(0, patrolPoints.Length);
		return patrolPoints[r].transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && restTime <= 0f) {
//			other.gameObject.GetComponent<Health>().RemovePlayerHealth(5);
			playerHitted = true;
			restTime = 1f;
			//anim.SetInteger("AlienPar", 0);
		}
	}

	//added code
	void attack()
	{
		if (GetComponent<Animation>() [attackClip.name].time > GetComponent<Animation>() [attackClip.name].length * impactTime &&
            !impacted &&
            GetComponent<Animation>()[attackClip.name].time<0.9*GetComponent<Animation>()[attackClip.name].length) 
		{
            //opponent.getHit(damage);
            player.GetComponent<Health>().currentHealth -= damage;
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
		if(Vector3.Distance(transform.position, player.position)<attackRange)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
