﻿using UnityEngine;
using System.Collections;

public class EnmarController : MonoBehaviour {

    #region FSM
    public enum FSMState { Walking, AttackDelayState, Attacking, AnimationPlaying, LaserAttack, Dying}
    public enum LaserState { Charging, Shooting, ShootFinish}

    [Header("Enmar FSM current state")]
    public FSMState enmarState;
    [Header("Laser attack current state")]
    public LaserState laserStatus;
    #endregion

    #region Normal Attack
    //Gameobjects
    [Header("Normal Attack")]
    public GameObject rightHand, leftHand;
    public GameObject attackArea1, attackArea2;

    public bool hasEnteredArea1 = false;
    public bool hasEnteredArea2 = false;

    bool isRightHandAttack = false;
    bool isLeftHandAttack = false;

    //Float
    public float slamDamage = 20;
    public float attackDelay = 6f;
    public float attackTime;
    public float tempTime;
    #endregion

    #region Laser Attack
    //Gameobjects
    [Header("Laser Attack")]
    public GameObject laserBeam;
    public GameObject laserCharge;
    public GameObject laserOrigin;

    GameObject laserChargeGO;
    GameObject laserBeamGO;
    Transform chargePulse;
    public GameObject player;
    Vector3 playerLastPos;
    

    //bool
    public bool isCharging = false;

    //Float
    public float rotationValue;
    public float laserDamage = 10;

    //Variables
    [Header("Time variables")]
    public float time;
    public float laserChargeTime;

    #endregion

    #region Health
    [Header("Health")]
    public float enmarMaxHealth = 100;

    #endregion

    #region Transform Points
    [Header("Transform Points")]
    public Transform pointToWalkTo;
    #endregion

    #region Animation
    [Header("Animation")]
    public Animator enmarAnim;
    #endregion

    public static EnmarController instance { get; set; }

    // Use this for initialization
    void Start () {

        instance = this;
        enmarState = FSMState.Walking;
        enmarAnim = gameObject.GetComponent<Animator>();
        //enmarState = FSMState.Attacking;
	}
	
	// Update is called once per frame
	void Update () {
        //laserBeam.transform.Rotate(5, 10, 5);
        
        //enmarHead.transform.Rotate()
        time += Time.deltaTime;
        GetPlayerLocation();
        switch (enmarState)
        {
            case FSMState.Walking:
                {
                    //play animation

                    //walk
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pointToWalkTo.transform.position, Time.deltaTime * 10);

                    if (gameObject.transform.position == pointToWalkTo.transform.position)
                    {
                        enmarAnim.SetTrigger("Reached");
                        //enmarAnim.SetBool("ReachedDestination", true);
                        enmarState = FSMState.AttackDelayState;
                    }
                }
                break;

            case FSMState.AttackDelayState:
                {
                    rightHand.transform.position = Vector3.Lerp(rightHand.transform.position,new Vector3(2.07f, 37.2f, 70f), Time.deltaTime);
                    leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, new Vector3(-21.7f, 37.2f, 70f),Time.deltaTime);
                    attackTime += Time.deltaTime;
                    if (attackTime > attackDelay)
                    {
                        attackTime = 0;
                        enmarState = FSMState.Attacking;
                        
                    }

                    isCharging = false;
                }
                break;

            case FSMState.Attacking:
                {
                    int rng = Random.Range(1, 4);
                    switch (rng)
                    {
                        case 1:
                            {
                                // DetectAreaToAttack
                                enmarState = FSMState.AnimationPlaying;
                            }
                            break;

                        case 2:
                            {
                                enmarState = FSMState.LaserAttack;
                            }
                            break;

                        case 3:
                            {
                                //DetectAreaToAttack();
                                enmarState = FSMState.AnimationPlaying;
                            }
                            break;

                        case 4:
                            {
                                enmarState = FSMState.LaserAttack;
                            }
                            break;

                        default:
                            {
                                //DetectAreaToAttack();
                                enmarState = FSMState.AnimationPlaying;
                            }
                            break;
                    }
                    //DetectAreaToAttack();

                    isCharging = false;
                }
                break;

            case FSMState.AnimationPlaying:
                {
                    DetectAreaToAttack();
                    
                    tempTime += Time.deltaTime;
                    if(tempTime > 7)
                    {
                        tempTime = 0;
                        enmarState = FSMState.AttackDelayState;
                    }
                }
                break;

            #region FSM Laser Attack
            case FSMState.LaserAttack:
                {
                    switch (laserStatus)
                    {
                        case LaserState.Charging:
                            {
                                laserChargeTime += Time.deltaTime;
                                if (isCharging == false)
                                {
                                    ChargeLaser();
                                }

                                float lerpValue = time / 3;
                                lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

                                chargePulse.localScale = Vector3.Lerp(chargePulse.localScale, new Vector3(10, 10, 10), Time.deltaTime * 0.5f);

                                if (laserChargeTime > 6)
                                {
                                    laserChargeTime = 0;
                                    Destroy(laserChargeGO);
                                    laserStatus = LaserState.Shooting;
                                }

                                //laserStatus = LaserState.Shooting;
                            }
                            break;

                        case LaserState.Shooting:
                            {
                                isCharging = false;
                                laserBeamGO = (GameObject)Instantiate(laserBeam, laserOrigin.transform.position, laserOrigin.transform.rotation, laserOrigin.transform);
                                laserBeamGO.transform.LookAt(player.transform.position);
                                laserStatus = LaserState.ShootFinish;
                            }
                            break;

                        case LaserState.ShootFinish:
                            {

                                enmarState = FSMState.AttackDelayState;
                                laserStatus = LaserState.Charging;
                            }
                            break;
                    }
                }
                break;
            #endregion


            case FSMState.Dying:
                {

                }
                break;
            default:
                {

                }
                break;
        }
	}

    public void DetectAreaToAttack()
    {
        if(hasEnteredArea1 == true)
        {
            PrimeRightHand();
            Debug.Log("Area 1 entered");
        }

        if(hasEnteredArea2 == true)
        {
            PrimeLeftHand();
            Debug.Log("Area 2 entered");
        }
    }

    public void GetPlayerLocation()
    {
        Debug.DrawLine(laserOrigin.transform.position, player.transform.position,Color.red);
        playerLastPos = player.transform.position;
    }

    public void ChargeLaser()
    {
        isCharging = true;
        
        //if(time < )
        laserChargeGO = (GameObject)Instantiate(laserCharge, laserOrigin.transform.position, laserOrigin.transform.rotation,laserOrigin.transform);
        chargePulse = laserChargeGO.transform.GetChild(0).transform;
        chargePulse.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        laserChargeGO.transform.position = laserOrigin.transform.position;
        laserChargeGO.transform.rotation = laserOrigin.transform.rotation; 
       
    }

    public void ShootLaserBeam()
    {
        laserBeamGO = (GameObject)Instantiate(laserBeam, laserOrigin.transform.position, laserOrigin.transform.rotation, laserOrigin.transform);
    }

    public void PrimeRightHand()
    {
        rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, new Vector3(rightHand.transform.position.x, 20, rightHand.transform.position.z), Time.deltaTime);
        //enmarState = FSMState.AttackDelayState;
    }

    public void PrimeLeftHand()
    {
        //leftHand.GetComponent<Rigidbody>().AddForce(Vector3.down * 100000);
        leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, new Vector3(leftHand.transform.position.x, 20, leftHand.transform.position.z), Time.deltaTime);
        //enmarState = FSMState.AttackDelayState;
    }
}
