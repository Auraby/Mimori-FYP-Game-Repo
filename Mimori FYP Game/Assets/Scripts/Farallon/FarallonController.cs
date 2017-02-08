using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarallonController : MonoBehaviour {

    public enum FarallonStates { Ground,Flying,Hover,Dying}
    public enum GroundStates { FindLandingSpot, FlyToLand, Landing, GroundIdle, Attack, Flinching , TakeOff }
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

    [Header("Booleans")]
    public bool isWingDamaged = false;
    public bool isFlamePillar = false;
    public bool isFireBall = false;
    [Space]

    [Header("Times")]
    public float groundIdleTime = 5;
    private float tempGroundTime;
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

    [Header("Misc")]
    private int attackRNG;
    public float distToTarget;

    //Temp
    public float strafeSpeed;
    public float strafeSpeedTime;
    public float strafeAngle;
    #endregion;

    public static FarallonController instance { get; set; }

    // Use this for initialization
    void Start () {
        currFaraState = FarallonStates.Flying;
        currHealth = MaxHealth;
        currWingHealth = WingMaxHealth;
        breathattack.SetActive(false);
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        if(currHealth <= 0)
        {
            currFaraState = FarallonStates.Dying;
        }
        if(currWingHealth <= 0)
        {
            isWingDamaged = true;
        }
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
                    Hover();
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
                                        ShootFireBall();
                                        StartCoroutine(WaitToSwitchHoverStates(2, HoverStates.Finished));
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
                    switch (currGroundState)
                    {
                        case GroundStates.FindLandingSpot:
                            {
                                breathattack.SetActive(false);
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    landingspotNum = IntRNG(4);
                                    //nextWaypoint = landingSpotsArray[landingspotNum].transform.position;

                                    if (currPosition != landingSpotsArray[landingspotNum].transform.position)
                                    {
                                        nextWaypoint = landingSpotsArray[landingspotNum].transform.position;
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
                                    RotateTowardsTarget(nextWaypoint);
                                    Fly(nextWaypoint);
                                    if (transform.position == nextWaypoint)
                                    {
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
                                    //landingSpotRot.y = 0;
                                    RotateTowardsTarget(landingSpotRot);
                                    //RotateToADirection(landingSpotRot);
                                    //RotateToADirection(landingSpotsArray[landingspotNum].rotation.eulerAngles);
                                    //transform.rotation.eulerAngles = Vector3.RotateTowards(transform.rotation.eulerAngles, landingSpotsArray[landingspotNum].forward,);
                                    Debug.Log("This is: " + landingSpotsArray[landingspotNum].gameObject.name);

                                    //Landing here

                                    //Then switch to idle

                                    StartCoroutine(WaitToSwitchGroundStates(5, GroundStates.GroundIdle));
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

                                //To melee or fire breath
                                #region Phase 1
                                if (FarallonPhasesController.instance.currentPhase == FarallonPhasesController.Phases.Phase1)
                                {
                                    if(currHealth <= 750)
                                    {
                                        currGroundState = GroundStates.Flinching;
                                    }

                                    tempGroundTime += Time.deltaTime;
                                    if(tempGroundTime >= groundIdleTime)
                                    {
                                        distToTarget = Vector3.Distance(playerTarget.transform.position, transform.position);
                                        tempGroundTime = 0;
                                        currGroundState = GroundStates.Attack;
                                        
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
                                    if (currHealth <= 750)
                                    {
                                        currGroundState = GroundStates.Flinching;
                                    }

                                    if (distToTarget > 70f)
                                    {
                                        //Attack based on choice
                                        FireBreath();
                                        StartCoroutine(WaitToSwitchGroundStates(3, GroundStates.GroundIdle));
                                    }

                                    if(distToTarget < 70)
                                    {
                                        //Claw attack
                                        StartCoroutine(WaitToSwitchGroundStates(3, GroundStates.GroundIdle));
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
                                StartCoroutine(WaitToSwitchGroundStates(2, GroundStates.TakeOff));
                            }
                            break;

                        case GroundStates.TakeOff:
                            {
                                breathattack.SetActive(false);
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
        //Vector3 targetDir = target - transform.position;
        //targetDir.y = 0;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    #endregion

    #region Attack
    public void ShootFireBall()
    {
        fireballGO = fireBallPool.RetrieveInstance();
        mouthEnd.transform.LookAt(playerTarget.transform.position);
        if(Time.time > fbNextFire)
        {
            fbNextFire = Time.time + fbFireRate;
            if (fireballGO)
            {
                fireballGO.transform.position = mouthEnd.transform.position;
                fireballGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.forward * fbSpeed;
            }
        }
        
    }

    public void FireBreath()
    {
        //breathAttackGO = (GameObject)Instantiate(breathattack, mouthEnd.transform.position, mouthEnd.transform.rotation);
        breathattack.SetActive(true);
        strafeSpeedTime += Time.deltaTime;
        float phase = Mathf.Sin(strafeSpeedTime / strafeSpeed);
        mouthEnd.transform.localRotation = Quaternion.Euler(new Vector3(0, (phase * strafeAngle), 0));

        
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

    #endregion
}
