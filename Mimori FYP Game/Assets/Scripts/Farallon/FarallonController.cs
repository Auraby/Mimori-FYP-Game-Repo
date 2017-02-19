using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarallonController : MonoBehaviour {

    public enum FarallonStates { Ground,Flying,Hover,Dying}
    public enum GroundStates { FindLandingSpot, FlyToLand, Landing, GroundIdle, Attack, Charge, Flinching , TakeOff }
    public enum FlyingStates { FindWaypoint, Fly, Reached}
    public enum HoverStates { HoverIdle, HoverThinkOfAttack, Attack, Finished} 

    public FarallonStates currFaraState;
    public GroundStates currGroundState;
    public FlyingStates currFlyingState;
    public HoverStates currHoverState;

    [Header("Farallon Properties")]
    public float MaxHealth = 100;
    public float currHealth;

    public float WingMaxHealth = 50;
    public float currWingHealth;

    public Vector3 currPosition;
    public Vector3 nextWaypoint;

    public float rotateSpeed;
    public GameObject playerTarget;

    public GameObject mouthEnd;

    [Header("Animations")]
    public Animator FaraAnim;

    [Header("Booleans")]
    public bool isWingDamaged = false;
    public bool isFlamePillar = false;
    public bool isFireBall = false;
    public bool isFireBreath = false;
    public bool isClawSlam = false;
    public bool isCharge = false;
    public bool afterCharge = false;
    [Space]

    [Header("Times")]
    public float groundIdleTime = 5;
    private float tempGroundTime;
    public float attackWaitTime = 3;
    private float tempAttackWaitTime;
    public float turnaroundTime = 2;
    private float tempTaroundTime;

    private float tempLandTurnTime;
    [Space]



    [Header("Flight")]
    public float flightSpeed;
    private int waypointNum;
    private int landingspotNum;

    [Header("Hover")]
    public float hoverSpeed;
    public float amplitude;
    public Vector3 tempPosition;

    [Header("Waypoints")]
    public Transform[] airWaypointsArray = new Transform[5];
    public Transform[] landingSpotsArray = new Transform[4];

    #region Attacks
    [Header("Breath Attack")]
    public GameObject breathattack;
    private GameObject breathAttackGO;

    [Header("Fire Ball")]
    public float fbFireRate;
    public float fbNextFire;
    public float fbSpeed;
    public ObjectPooling fireBallPool;
    private GameObject fireballGO;


    [Header("Charge")]
    public float chargeSpeed;
    public float chargeDamage;
    public float chargeRotateSmooth = 0.5f;
    private Vector3 turnAroundAngle;

    [Header("Slam Shockwave")]
    public GameObject slamShockwave;
    public GameObject slamDustParticle;
    private GameObject slamDustParticleGO;
    private GameObject slamShockwaveGO;
    public Transform slamArea;


    [Header("Misc")]
    public float distToTarget;
    private int attackRNG;
    private string landingSpotName;
    public AudioSource dyingSound;
    public AudioSource shootfireballsound;
    public AudioSource fireBreathSound;
    public float playerBulletDmg;
    public Material farallonMat;

    //Temp
    [HideInInspector]
    public float strafeSpeed;
    [HideInInspector]
    public float strafeSpeedTime;
    [HideInInspector]
    public float strafeAngle;
    #endregion;

    public static FarallonController instance { get; set; }

    // Use this for initialization
    void Start () {
        currFaraState = FarallonStates.Flying;
        currHealth = MaxHealth;
        currWingHealth = WingMaxHealth;
        //breathattack.SetActive(false);
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        if(currHealth <= 0)
        {
            FaraAnim.SetTrigger("Died");
            //dyingSound.Play();
            currFaraState = FarallonStates.Dying;
        }
        if(currWingHealth <= 0)
        {
            isWingDamaged = true;
        }

        #region Cheats

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CheatDamageWings();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            CheatSetFarallonHealthTo10();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            CheatTakeOff();
        }
        #endregion

        switch (currFaraState)
        {
            #region Flying
            case FarallonStates.Flying:
                {
                    currGroundState = GroundStates.FindLandingSpot;
                    currHoverState = HoverStates.HoverIdle;
                    if(isWingDamaged == true)
                    {
                        currFaraState = FarallonStates.Ground;
                    }

                    switch (currFlyingState)
                    {
                       
                        case FlyingStates.FindWaypoint:
                            {
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    waypointNum = IntRNG(4);
                                    if (currPosition != airWaypointsArray[waypointNum].position)
                                    {
                                        nextWaypoint = airWaypointsArray[waypointNum].position;
                                        currFlyingState = FlyingStates.Fly;
                                    }

                                    else
                                    {
                                        waypointNum = IntRNG(4);
                                    }
                                }
                                #endregion

                                #region Phase 2
                                if(FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion

                            }
                            break;

                        case FlyingStates.Fly:
                            {
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    RotateTowardsTarget(nextWaypoint);
                                    Fly(nextWaypoint);
                                    if (transform.position == nextWaypoint)
                                    {
                                        currPosition = transform.position;
                                        currFlyingState = FlyingStates.Reached;

                                    }
                                }
                                #endregion

                                #region Phase 2

                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;

                        case FlyingStates.Reached:
                            {
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    currFaraState = FarallonStates.Hover;
                                }
                                //RotateTowardsTarget(playerTarget.transform.position);
                                //StartCoroutine(WaitToSwitchMainStates(3, FarallonStates.Hover));
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;
                    }
                }
                break;

            #endregion

            #region Hover
            case FarallonStates.Hover:
                {
                    currFlyingState = FlyingStates.FindWaypoint;
                    //Hover();
                    if (isWingDamaged == true)
                    {
                        currFaraState = FarallonStates.Ground;
                    }
                    switch (currHoverState)
                    {
                        case HoverStates.HoverIdle:
                            {
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    RotateTowardsTarget(playerTarget.transform.position);

                                    //Buffer period
                                    attackRNG = IntRNG(2);

                                    StartCoroutine(WaitToSwitchHoverStates(2, HoverStates.HoverThinkOfAttack));
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion

                            }
                            break;

                        case HoverStates.HoverThinkOfAttack:
                            {
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    if (attackRNG == 0)
                                    {
                                        isFlamePillar = true;
                                        currHoverState = HoverStates.Attack;
                                    }

                                    if (attackRNG == 1)
                                    {
                                        isFireBall = true;
                                        FaraAnim.SetTrigger("HoverAtk");
                                        currHoverState = HoverStates.Attack;
                                    }
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;

                        case HoverStates.Attack:
                            {
                                //Attack
                                Debug.Log("Attacking!");
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    if (isFlamePillar == true)
                                    {
                                        StartCoroutine(StartFlameEruption(2));

                                        if (EruptionController.instance.isFinishEruption == true)
                                        {
                                            currHoverState = HoverStates.Finished;
                                        }
                                        //StartCoroutine(WaitToSwitchHoverStates(20, HoverStates.Finished));
                                    }

                                    if (isFireBall == true)
                                    {
                                        //Play animation

                                        //Shoot fireball
                                        if (FaraAnim.GetCurrentAnimatorStateInfo(0).IsName("Flying"))
                                        {
                                            currHoverState = HoverStates.Finished;
                                            FaraAnim.ResetTrigger("HoverAtk");
                                        }
                                    }

                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion

                            }
                            break;

                        case HoverStates.Finished:
                            {
                                isFireBall = false;
                                isFlamePillar = false;
                                //Decide to continue hovering or fly to another spot
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    int randomInt = IntRNG(2);

                                    if (randomInt == 0)
                                    {
                                        //Continue Hover
                                        currHoverState = HoverStates.HoverIdle;
                                    }

                                    if (randomInt == 1)
                                    {
                                        currFaraState = FarallonStates.Flying;
                                    }
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;
                    }
                }
                break;

            #endregion

            #region Ground
            case FarallonStates.Ground:
                {
                    currFlyingState = FlyingStates.FindWaypoint;
                    currHoverState = HoverStates.HoverIdle;
                    isWingDamaged = false;
                    currWingHealth = WingMaxHealth;
                    isFireBall = false;
                    isFlamePillar = false;

                    if (currHealth <= 0)
                    {
                        //FaraAnim.SetTrigger("Died");
                        dyingSound.Play();
                        //currFaraState = FarallonStates.Dying;
                    }

                    switch (currGroundState)
                    {
                        case GroundStates.FindLandingSpot:
                            {
                                //breathattack.SetActive(false);
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    landingspotNum = IntRNG(4);
                                    //nextWaypoint = landingSpotsArray[landingspotNum].transform.position;

                                    if (currPosition != landingSpotsArray[landingspotNum].transform.position)
                                    {
                                        landingSpotName = landingSpotsArray[landingspotNum].name;
                                        nextWaypoint = landingSpotsArray[landingspotNum].transform.position;
                                        //FaraAnim.SetBool("WingDamaged", true);
                                        
                                        currGroundState = GroundStates.FlyToLand;
                                    }

                                    else
                                    {
                                        landingspotNum = IntRNG(4);
                                    }
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;

                        case GroundStates.FlyToLand:
                            {

                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    //RotateTowardsTarget(nextWaypoint);
                                    RotateToADirection(nextWaypoint);
                                    Vector3 landingSpotRot = landingSpotsArray[landingspotNum].transform.GetChild(0).transform.position;
                                    //landingSpotRot.x = 0;
                                    RotateTowardsTarget(landingSpotRot);
                                    flightSpeed = 60;
                                    //FaraAnim.SetTrigger("Landing");
                                    FaraAnim.SetBool("WingDamaged", true);
                                    Fly(nextWaypoint);
                                    if (transform.position == nextWaypoint)
                                    {
                                        FaraAnim.SetTrigger("LandTurn");
                                        currPosition = transform.position;
                                        currGroundState = GroundStates.Landing;

                                    }
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;

                        case GroundStates.Landing:
                            {
                                //Quaternion newRotation = landingSpotsArray[landingspotNum].rotation;
                                //transform.rotation = Quaternion.LookRotation(landingSpotsArray[landingspotNum].rotation.eulerAngles);
                                Debug.Log("rotation:" + landingSpotsArray[landingspotNum].eulerAngles);
                                Debug.Log("Local Rotation:" + landingSpotsArray[landingspotNum].localEulerAngles);
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    Vector3 landingSpotRot = landingSpotsArray[landingspotNum].transform.GetChild(0).transform.position;
                                    //landingSpotRot.x = 0;
                                    RotateTowardsTarget(landingSpotRot);
                                    //RotateToADirection(landingSpotRot);
                                    //RotateToADirection(landingSpotsArray[landingspotNum].rotation.eulerAngles);
                                    //transform.rotation.eulerAngles = Vector3.RotateTowards(transform.rotation.eulerAngles, landingSpotsArray[landingspotNum].forward,);
                                    Debug.Log("This is: " + landingSpotsArray[landingspotNum].gameObject.name);
                                    tempLandTurnTime += Time.deltaTime;
                                    //Landing here
                                   // FaraAnim.GetCurrentAnimatorStateInfo(0).IsName("Improvised Idle")
                                    //Then switch to idle
                                    if(tempLandTurnTime > 3)
                                    {
                                        currGroundState = GroundStates.GroundIdle;
                                    }
                                    //StartCoroutine(WaitToSwitchGroundStates(5, GroundStates.GroundIdle));
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;

                        case GroundStates.GroundIdle:
                            {
                                Debug.Log("Idle");
                                Debug.Log("In " + landingSpotName);
                                tempLandTurnTime = 0;
                                FaraAnim.SetBool("WingDamaged", false);
                                //To melee or fire breath
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    //if (currHealth <= 750)
                                    //{
                                    //    currGroundState = GroundStates.Flinching;
                                    //}

                                    if(afterCharge == true)
                                    {

                                        RotateAfterCharge();
                                        tempTaroundTime += Time.deltaTime;
                                        if (tempTaroundTime > turnaroundTime)
                                        {
                                            tempTaroundTime = 0;
                                            afterCharge = false;
                                        }

                                        
                                    }

                                    tempGroundTime += Time.deltaTime;
                                    if(tempGroundTime >= groundIdleTime)
                                    {
                                        afterCharge = false;
                                        isCharge = false;
                                        attackRNG = IntRNG(3);
                                        distToTarget = Vector3.Distance(playerTarget.transform.position, transform.position);

                                        if (attackRNG == 0)
                                        {
                                            //Attack based on choice
                                            //FireBreath();
                                            FaraAnim.SetTrigger("FireBreath");                                          
                                            isFireBreath = true;
                                            currGroundState = GroundStates.Attack;
                                            //StartCoroutine(WaitToSwitchGroundStates(3, GroundStates.GroundIdle));
                                        }

                                        if (attackRNG == 1)
                                        {
                                            //Claw attack
                                            FaraAnim.SetTrigger("Slam");
                                            isClawSlam = true;
                                            currGroundState = GroundStates.Attack;
                                            //StartCoroutine(WaitToSwitchGroundStates(3, GroundStates.GroundIdle));
                                        }

                                        if (attackRNG == 2)
                                        {
                                            //Claw attack
                                            FaraAnim.SetTrigger("Charge");
                                            //isCharge = true;
                                            currGroundState = GroundStates.Attack;
                                            //StartCoroutine(WaitToSwitchGroundStates(3, GroundStates.GroundIdle));
                                        }

                                        tempGroundTime = 0;
                                        
                                        
                                        //StartCoroutine(WaitToSwitchGroundStates(1, GroundStates.Attack));
                                    }
                                   
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion

                            }
                            break;

                        case GroundStates.Attack:
                            {
                                Debug.Log("Attacking");
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    
                                    //if (currHealth <= 750)
                                    //{
                                    //    currGroundState = GroundStates.Flinching;
                                    //}
                                    if(isFireBreath == true)
                                    {
                                        //Debug.Log("Fire Breath");

                                    }

                                    if(isClawSlam == true)
                                    {
                                        //Debug.Log("Claw Slam");
                                        //currGroundState = GroundStates.GroundIdle;
                                    }

                                    

                                    if (FaraAnim.GetCurrentAnimatorStateInfo(0).IsName("Improvised Idle"))
                                    {
                                        //if (isCharge == true)
                                        //{
                                        //    updateCurrentGroundPosition();
                                        //    afterCharge = true;
                                        //}
                                        
                                        
                                        isFireBreath = false;
                                        //isCharge = false;
                                        isClawSlam = false;
                                        currGroundState = GroundStates.GroundIdle;
                                    }

                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;

                        case GroundStates.Charge:
                            {
                                Debug.Log("Attacking");
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    tempAttackWaitTime += Time.deltaTime;
                                    ChargeForward();
                                    if (tempAttackWaitTime > attackWaitTime)
                                    {
                                        if (isCharge == true)
                                        {
                                            afterCharge = true;
                                        }


                                       tempAttackWaitTime = 0;
                                       currGroundState = GroundStates.GroundIdle;
                                    }
                                   
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;

                        case GroundStates.Flinching:
                            {
                                //Play animation and be invulnerable
                                FaraAnim.SetTrigger("TakeOff");
                                StartCoroutine(WaitToSwitchGroundStates(2, GroundStates.TakeOff));
                            }
                            break;

                        case GroundStates.TakeOff:
                            {
                                //breathattack.SetActive(false);
                                Debug.Log("taking off");
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    StartCoroutine(WaitToSwitchMainStates(1, FarallonStates.Flying));
                                }
                                #endregion

                                #region Phase 2
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase2)
                                {

                                }
                                #endregion

                                #region Phase 3
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase3)
                                {

                                }
                                #endregion
                            }
                            break;
                    }
                }
                break;
            #endregion

            #region Dying

            case FarallonStates.Dying:
                {
                    StopBreath();
                }
                break;

            #endregion
        }
    }

    void FixedUpdate()
    {
        //tempPosition.x += flightSpeed;
      
    }

    #region General Functions
    public void Hover()
    {
        tempPosition = transform.position;
        tempPosition.y += Mathf.Sin(Time.realtimeSinceStartup * hoverSpeed) * amplitude;
        transform.position = tempPosition;
    }
    
    public void Fly(Vector3 targetDirection)
    {
        //flightSpeed = 2;
        ////transform.Translate(targetDirection);
        transform.position = Vector3.MoveTowards(transform.position, targetDirection, flightSpeed * Time.deltaTime);
        //Debug.Log("Flying");
        //tempPosition = transform.position;
        //tempPosition.x += Math;
        //tempPosition.z += flightSpeed;
        //tempPosition.y += Mathf.Sin(Time.realtimeSinceStartup * hoverSpeed) * amplitude;
        //transform.position = tempPosition;
    }

    public void RotateTowardsTarget(Vector3 target)
    {
        Vector3 targetDir = target - transform.position;
        //targetDir.y = 0;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void RotateToADirection(Vector3 direction)
    {
        Vector3 targetDir = direction - transform.position;
        //targetDir.x = 0;
        targetDir.y = 0;
        //rection.y = 0;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void RotateAfterCharge()
    {
        turnAroundAngle = transform.eulerAngles + 180f * Vector3.up; // what the new angles should be

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, turnAroundAngle, chargeRotateSmooth * Time.deltaTime); // lerp to new angles
    }

    public void updateCurrentGroundPosition()
    {
        if (landingSpotName == "LandingSpot")
        {  
            landingSpotName = landingSpotsArray[3].name;
        }

        if (landingSpotName == "LandingSpot (1)")
        {
            
            landingSpotName = landingSpotsArray[2].name;

        }

        if (landingSpotName == "LandingSpot (2)")
        {
            
            landingSpotName = landingSpotsArray[1].name;

        }

        if (landingSpotName == "LandingSpot (3)")
        {
           
            landingSpotName = landingSpotsArray[0].name;

        }
    }
    #endregion

    #region Attack
    public void ShootFireBall()
    {
        shootfireballsound.Play();
        fireballGO = fireBallPool.RetrieveInstance();
        mouthEnd.transform.LookAt(playerTarget.transform.position);
        if(Time.time > fbNextFire)
        {
            fbNextFire = Time.time + fbFireRate;
            if (fireballGO)
            {
                fireballGO.transform.position = mouthEnd.transform.position;
                fireballGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.forward * fbSpeed;
                fireballGO.transform.LookAt(playerTarget.transform.position);
            }
        }
        
    }

    public void FireBreath()
    {
        //breathAttackGO = (GameObject)Instantiate(breathattack, mouthEnd.transform.position, mouthEnd.transform.rotation);
        breathattack.SetActive(true);
        fireBreathSound.Play();
        //strafeSpeedTime += Time.deltaTime;
        //float phase = Mathf.Sin(strafeSpeedTime / strafeSpeed);
        //mouthEnd.transform.localRotation = Quaternion.Euler(new Vector3(0, (phase * strafeAngle), 0));


    }

    public void StopBreath()
    {
        breathattack.SetActive(false);
    }

    public void StartCharge()
    {
        Debug.Log("Start Charge");
        isCharge = true;
        currGroundState = GroundStates.Charge;
        shootfireballsound.Play();
    }

    public void ChargeForward()
    {
        //if (landingSpotName == "LandingSpot")
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, landingSpotsArray[3].transform.position, chargeSpeed * Time.deltaTime);
        //    //landingSpotName = landingSpotsArray[3].name;
        //    Debug.Log("Charging to 3");
        //}

        //if (landingSpotName == "LandingSpot (1)")
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, landingSpotsArray[2].transform.position, chargeSpeed * Time.deltaTime);
        //    //landingSpotName = landingSpotsArray[2].name;
        //    //RotateToADirection(landingSpotsArray[2].transform.position);
        //    Debug.Log("Charging to 2");
        //}

        //if (landingSpotName == "LandingSpot (2)")
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, landingSpotsArray[1].transform.position, chargeSpeed * Time.deltaTime);
        //    //landingSpotName = landingSpotsArray[1].name;
        //    //RotateToADirection(landingSpotsArray[1].transform.position);
        //    Debug.Log("Charging to 1");
        //}

        //if (landingSpotName == "LandingSpot (3)")
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, landingSpotsArray[0].transform.position, chargeSpeed * Time.deltaTime);
        //    //landingSpotName = landingSpotsArray[0].name;
        //    //RotateToADirection(landingSpotsArray[0].transform.position);
        //    Debug.Log("Charging to 0");
        //}
       // Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        //transform.position = Vector3.MoveTowards(transform.position, localForward, chargeSpeed * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * chargeSpeed;
    }

    public void SlamAttack()
    {
        //slamShockwaveGO = (GameObject)Instantiate(slamShockwave, new Vector3(slamArea.position.x - 14,slamArea.position.y + 1.5f,slamArea.position.z), slamShockwave.transform.rotation);
        slamShockwaveGO = (GameObject)Instantiate(slamShockwave, new Vector3(slamArea.position.x , slamArea.position.y + 1.5f, slamArea.position.z), slamShockwave.transform.rotation);
        slamShockwaveGO.SetActive(true);

        slamDustParticleGO = (GameObject)Instantiate(slamDustParticle, new Vector3(slamArea.position.x, slamArea.position.y + 1.5f, slamArea.position.z), slamDustParticle.transform.rotation);
        slamDustParticleGO.SetActive(true);
    }
    #endregion
    //public void RotateToSpecificAngle(Vector3)
    #region Calculations
    public int IntRNG(int maxSizeExclusive)
    {
        int num = Random.Range(0, maxSizeExclusive);
        return num;
    }

    public float CheckAngleRelativeToPlayer()
    {
        Vector3 targetDir = playerTarget.transform.position - transform.position;
        targetDir.y = 0;
        float angle = Vector3.Angle(targetDir, transform.forward);
        Vector3 cross = Vector3.Cross(targetDir, transform.forward);
        if (cross.y < 0)
        {
            angle = -angle;
        }

        return angle;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if(currFaraState == FarallonStates.Ground)
            {
                StartCoroutine(flashRed());
                currHealth -= playerBulletDmg;
                FarallonPhasesController.instance.faraHealthSlider.value -= playerBulletDmg;
            }
           
        }
    }
    #endregion

    #region IEnumerators
    private IEnumerator WaitToSwitchMainStates(float sec, FarallonStates stateToSwitch)
    {
        yield return new WaitForSeconds(sec);
        currFaraState = stateToSwitch;
    }

    private IEnumerator WaitToSwitchHoverStates(float sec, HoverStates stateToSwitch)
    {
        yield return new WaitForSeconds(sec);
        currHoverState = stateToSwitch;
    }

    private IEnumerator WaitToSwitchFlyingStates(float sec, FlyingStates stateToSwitch)
    {
        yield return new WaitForSeconds(sec);
        currFlyingState = stateToSwitch;
    }

    private IEnumerator WaitToSwitchGroundStates(float sec, GroundStates stateToSwitch)
    {
        yield return new WaitForSeconds(sec);
        currGroundState = stateToSwitch;
    }

    private IEnumerator StartFlameEruption(float sec)
    {
        yield return new WaitForSeconds(sec);
        EruptionController.instance.isStartEruption = true;

    }

    private IEnumerator flashRed()
    {
        farallonMat.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        farallonMat.color = Color.white;
    }

    #endregion

    #region Cheats
    public void CheatSetFarallonHealthTo10()
    {
        currHealth = 10;
    }

    public void CheatDamageWings()
    {
        isWingDamaged = true;
    }

    public void CheatTakeOff()
    {
        currGroundState = GroundStates.Flinching;
    }

    #endregion
}
