using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoltranController : MonoBehaviour {

    public enum ZoltranStates { Start, FindWaypoint, Moving, Attacking, Vanish, Appear, Stunned, Chase, Dying}
    public enum AttackMode { Normal, BulletHell}
    public enum ZoltranActivity { NormalMoving, Waiting }

    [Header("Zoltran Properties")]
    public ZoltranStates currentState;
    public AttackMode currentAttackMode;
    public ZoltranActivity currentActivity;
    public float zMaxHealth = 100;
    public float zCurrentHealth;
    public float zMaxBHHealth = 100;
    public float zCurrentBHHealth;
    public float rotateSpeed;
    public int zoltranID;
    [Space]
    public bool isPattern1 = false;
    public bool isPattern2 = false;
    public bool isPattern3 = false;
    public bool isIllusion = false;
    public bool diedInBH = false;
    [Space]
    public float maxrunningSpeed;
    //the rate to increase the speed
    public float spdIncRate;
    private float nextspdInc;

    #region Attacks
    //public List<AttackInfo> actions = new List<AttackInfo>();
    //private float attackTime;
    //private int attackRNG;
    //private Transform playerTarget;
    [Header("Bullet Properties")]
    public ObjectPooling zoltranSoulShotPool;
    private GameObject soulShotGO;

    [Header("Bullet Spawn Points")]
    public GameObject mouthEnd;

    [Header("SoulShot Properties")]
    public float ssfireRate;
    public float ssBulletSpeed;
    float ssnextFire;

    [Header("StrafeShot Properties")]
    public float strafeSpeed;
    public float strafeFireRate;
    public float strafeBulletSpeed;
    public float strafeAngle;
    private float strafeNextFire;
    float strafeSpeedTime;

    [Header("Tryshot Properties")]   
    public float tsFireRate;
    public float tsBulletSpeed;
    float tsNextFire;

    [Header("Spiral Burst Properties")]
    public float burstTurnSpeed;
    public float sbFireRate;
    public float sbBulletSpeed;

    [Header("Soulbeam")]
    public GameObject soulBeam;
    public GameObject innerBeam;
    public Transform soulBeamParent;

    [Header("Explode(Illusion Only)")]
    public float explosionRadius = 4;
    public float explosionDamage = 20;
    public bool isExplodeMode = false;
    bool exploded = false;

    [Header("Vanish Particle")]
    public GameObject vanishParticle;
    GameObject vanishParticleGO;


    private int attackRNG;
    #endregion

    [Header("Illusion Count")]
    #region Illusions
    public static List<GameObject> illusionList = new List<GameObject>();
    public int IllusionLimit = 2;

    //Only for illusions
    public GameObject mainZoltran;

    //Only for main zoltran
    public GameObject illusionOne;
    public GameObject illusionTwo;
    #endregion

    #region Misc
    [Header("Misc")]
    public GameObject player;
    public float playerBulletDamage;
    public GameObject MovementArea;
    public Transform waitingArea;
    public GameObject respawnPoints;
    public GameObject zolAura;
    [HideInInspector]
    public float temptime = 0;
    NavMeshAgent navAgent;
    Vector3 newWaypoint;
    private float distToTarget;
    private float distToWaypoint;
    [HideInInspector]
    public Vector3 lastPos;
    private bool reappearFromBH = false;
    [HideInInspector]
    private bool spawnCloud = false;
    private bool vanishCloud = false;
    private bool illuAppear = false;
    private float zolYPosValue;
    private Vector3 resPoint1, resPoint2, resPoint3;
    private bool isPlayerClose = false;
    private bool isPounced = false;
    #endregion

    #region Animation
    [Header("Animation Settings")]
    public float increaseDelay = 2;
    Animator zolAnim;
    private float spdPara = 0;
    private float delayPeriod;
    #endregion




    public static ZoltranController instance { get; set; }
	// Use this for initialization
	void Start () 
    {
        instance = this;
        navAgent = GetComponent<NavMeshAgent>();
        //navAgent.speed = 0;
        //currentState = ZoltranStates.FindWaypoint;
        zCurrentHealth = zMaxHealth;
        isExplodeMode = false;
        exploded = false;
        zolAnim = GetComponent<Animator>();
        zolYPosValue = gameObject.transform.position.y;

        resPoint1 = respawnPoints.transform.GetChild(0).transform.position;
        resPoint2 = respawnPoints.transform.GetChild(1).transform.position;
        resPoint3 = respawnPoints.transform.GetChild(2).transform.position;

        if(isIllusion == false)
        {
            illusionList.Clear();
        }
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("Illusion List COunt: " + illusionList.Count);
        //Debug.DrawRay(orbEnd1.transform.position, orbEnd1.transform.forward, Color.red);
        //Debug.Log("Waiting area Pos: " + waitingArea.position);
        //Debug.Log("Current Pos: " + gameObject.transform.position);
        //currentPos = gameObject.transform.position;

        //Cheats
        if (Input.GetKeyDown(KeyCode.M))
        {
            CheatToClearLevel();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            CheatToShowMainZoltran();
        }
        //..


        if (currentActivity == ZoltranActivity.NormalMoving)
        {
            switch (currentAttackMode)
            {
                #region Normal Attack Mode
                case AttackMode.Normal:
                    {
                        zCurrentBHHealth = zMaxBHHealth;
                        diedInBH = false;

                        if (isIllusion == false)
                        {
                            if (zCurrentHealth <= 200 && isPattern1 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.One;
                                isPattern1 = true;
                                BulletHellController.instance.patternNo = 1;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                zolAnim.SetTrigger("BhAtk");
                                currentAttackMode = AttackMode.BulletHell;                                
                            }

                            if (zCurrentHealth <= 130 && isPattern2 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Two;
                                isPattern2 = true;
                                BulletHellController.instance.patternNo = 2;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                zolAnim.SetTrigger("BhAtk");
                                currentAttackMode = AttackMode.BulletHell;
                            }

                            if (zCurrentHealth <= 80 && isPattern3 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Three;
                                isPattern3 = true;
                                BulletHellController.instance.patternNo = 3;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                zolAnim.SetTrigger("BhAtk");
                                currentAttackMode = AttackMode.BulletHell;
                            }
                        }

                        else
                        {
                            if (mainZoltran.GetComponent<ZoltranController>().zCurrentHealth <= 200 && isPattern1 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.One;
                                isPattern1 = true;
                                BulletHellController.instance.patternNo = 1;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                zolAnim.SetTrigger("BhAtk");
                                currentAttackMode = AttackMode.BulletHell;
                            }

                            if (mainZoltran.GetComponent<ZoltranController>().zCurrentHealth <= 130 && isPattern2 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Two;
                                isPattern2 = true;
                                BulletHellController.instance.patternNo = 2;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                zolAnim.SetTrigger("BhAtk");
                                currentAttackMode = AttackMode.BulletHell;
                            }

                            if (mainZoltran.GetComponent<ZoltranController>().zCurrentHealth <= 80 && isPattern3 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Three;
                                isPattern3 = true;
                                BulletHellController.instance.patternNo = 3;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                zolAnim.SetTrigger("BhAtk");
                                currentAttackMode = AttackMode.BulletHell;
                            }
                        }

                        if (zCurrentHealth <= 0)
                        {
                            if(isIllusion == false)
                            {
                                zolAnim.SetTrigger("Dead");
                                currentState = ZoltranStates.Dying;
                                illusionOne.GetComponent<ZoltranController>().currentState = ZoltranStates.Dying;
                                illusionTwo.GetComponent<ZoltranController>().currentState = ZoltranStates.Dying;
                            }

                            else
                            {
                                currentState = ZoltranStates.Vanish;
                            }
                            
                        }

                        #region Normal Mode
                        switch (currentState)
                        {
                            case ZoltranStates.Start:
                                {
                                    //if(isIllusion == false)
                                    //{
                                    //    SpawnIllusions();
                                    //}

                                    //if (isIllusion == true)
                                    //{
                                    //    if (illusionList.Count == 1)
                                    //    {
                                    //        illusionList[0].gameObject.GetComponent<ZoltranController>().zoltranID = 2;
                                    //    }

                                    //    if (illusionList.Count == 2)
                                    //    {
                                    //        illusionList[1].gameObject.GetComponent<ZoltranController>().zoltranID = 3;
                                    //    }
                                    //}


                                    //if(illusionList.Count == 2)
                                    //{

                                    //}
                                    if(isIllusion == false)
                                    {

                                        illusionOne.transform.position = generateRandomPositionInArea();
                                        illusionTwo.transform.position = generateRandomPositionInArea();
                                        illusionOne.SetActive(true);
                                        illusionTwo.SetActive(true);

                                    }

                                    Poof(gameObject.transform.position);
                                    //StartCoroutine(WaitToSwitchNormalAttackStates(4, ZoltranStates.FindWaypoint));
                                    currentState = ZoltranStates.FindWaypoint;
                                }
                                break;

                            case ZoltranStates.FindWaypoint:
                                {
                                    Debug.Log("Find Waypoint");
                                    zolAnim.SetBool("Stunned", false);
                                    zolAnim.ResetTrigger("NormalAtk");
                                    
                                    //Find random point to move to
                                    newWaypoint = generateRandomPositionInArea();
                                    ResetRotations();
                                    StartCoroutine(WaitToSwitchNormalAttackStates(1, ZoltranStates.Moving)); 
                                    //currentState = ZoltranStates.Moving;
                                }
                                break;

                            case ZoltranStates.Moving:
                                {
                                    zolAnim.ResetTrigger("NormalAtk");
                                    navAgent.Resume();
                                    navAgent.SetDestination(newWaypoint);
                                    lastPos = newWaypoint;

                                    float tempdist = Vector3.Distance(transform.position, newWaypoint);
                                   
                                    if (tempdist < 15)
                                    {
                                        zolAnim.SetFloat("Speed", 0.0f);
                                        spdPara = 0;
                                        navAgent.speed = 0;
                                        RotateTowardsPlayer();
                                        attackRNG = CalculateRNGForAttack(4);
                                        distToTarget = Vector3.Distance(player.transform.position, transform.position);
                                        //currentState = ZoltranStates.Attacking;
                                        StartCoroutine(SwitchToAttackState());
                                    }
                                }
                                break;

                            case ZoltranStates.Attacking:
                                {
                                    //transform.LookAt(player.transform.position);
                                    
                                    reappearFromBH = false;

                                    navAgent.Stop();                     

                                    if (!isIllusion)
                                    {
                                        DoNormalRangeAttacks();
                                    }

                                    //For illusions
                                    else
                                    {
                                        //Only after first bullet hell pattern then they start to rush towards player
                                        if(isPattern1 == true)
                                        {
                                            switch (attackRNG)
                                            {
                                                case 0:
                                                    {
                                                        DoNormalRangeAttacks();
                                                    }
                                                    break;

                                                case 1:
                                                    {
                                                        zolAnim.SetFloat("Speed", 0);
                                                        navAgent.speed = 0;
                                                        isExplodeMode = true;
                                                        currentState = ZoltranStates.Chase;
                                                    }
                                                    break;

                                                case 2:
                                                    {
                                                        DoNormalRangeAttacks();
                                                    }
                                                    break;

                                                case 3:
                                                    {
                                                        zolAnim.SetFloat("Speed", 0);
                                                        navAgent.speed = 0;
                                                        isExplodeMode = true;
                                                        currentState = ZoltranStates.Chase;
                                                    }
                                                    break;
                                            }
                                        }

                                        //If starting only, do normal attacks
                                        else
                                        {
                                            DoNormalRangeAttacks();
                                        }
                                       
                                    }
                                }
                                break;

                            case ZoltranStates.Vanish:
                                {
                                    spawnCloud = false;
                                    navAgent.Stop();
                                    navAgent.speed = 0;
                                    //spdPara = 0;
                                    zolAnim.SetFloat("Speed", 0);
                                    if (isIllusion)
                                    {
                                        zCurrentHealth = zMaxHealth;
                                    }
                                    if(vanishCloud == false)
                                    {
                                        Poof(gameObject.transform.position);
                                        vanishCloud = true;
                                    }
                                    
                                    gameObject.transform.position = waitingArea.position;
                                    zolAura.SetActive(false);
                                    //gameObject.SetActive(false);
                                    newWaypoint = generateRandomPositionInArea();
                                    Debug.Log(newWaypoint);
                                    temptime = 0;
                                    
                                    StartCoroutine(SwitchToAppearState(4));

                                }
                                break;


                            case ZoltranStates.Appear:
                                {
                                    
                                    vanishCloud = false;
                                    navAgent.Stop();
                                    

                                    if (zoltranID == 1)
                                    {
                                        gameObject.transform.position = new Vector3(resPoint1.x, zolYPosValue, resPoint1.z);
                                    }

                                    else if (zoltranID == 2)
                                    {
                                        gameObject.transform.position = new Vector3(resPoint2.x, zolYPosValue, resPoint2.z);
                                    }

                                    else
                                    {
                                        gameObject.transform.position = new Vector3(resPoint3.x, zolYPosValue, resPoint3.z);
                                    }

                                    // gameObject.SetActive(true);
                                    zolAura.SetActive(true);
                                    RotateTowardsPlayer();
                                    ResetRotations();
                                    if (spawnCloud == false)
                                    {
                                        Poof(gameObject.transform.position);
                                        zolAnim.SetTrigger("Roar");
                                        spawnCloud = true;
                                    }
                                    //StopAllCoroutines();
                                    StartCoroutine(WaitToSwitchNormalAttackStates(1,ZoltranStates.FindWaypoint));
                                   // currentState = ZoltranStates.FindWaypoint;
                                }
                                break;

                            case ZoltranStates.Stunned:
                                {
                                    if (isIllusion == false)
                                    {
                                        navAgent.Stop();
                                        
                                        //wait to switch
                                        StartCoroutine(WaitToSwitchNormalAttackStates(2, ZoltranStates.FindWaypoint));
                                    }

                                    else
                                    {
                                        //Poof(gameObject.transform.position);
                                        //gameObject.transform.position = waitingArea.position;
                                        //temptime = 0;
                                        //StartCoroutine(SwitchToAppearState(3));
                                        currentState = ZoltranStates.Vanish;
                                    }
                                }
                                break;

                            case ZoltranStates.Chase:
                                {
                                    //float tempIncrease = 0;

                                    //if (tempIncrease <= 1)
                                    //{
                                    //    tempIncrease += Time.deltaTime / 10;
                                    //    zolAnim.SetFloat("Speed", tempIncrease);
                                    //}
                                    //zolAnim.SetFloat("Speed", 1.0f);
                                    //RushToPlayer();
                                    ChasePlayer();
                                    if(zolAnim.GetCurrentAnimatorStateInfo(0).IsName("Roar"))
                                    {
                                        currentState = ZoltranStates.FindWaypoint;
                                        zolAnim.applyRootMotion = false;
                                        isPlayerClose = false;
                                        isPounced = false;
                                    }

                                }
                                break;

                            case ZoltranStates.Dying:
                                {
                                    
                                    StartZoltran.zoltranDied = true;
                                    GameController.gameController.fightingBoss = false;
                                    navAgent.Stop();

                                   
                                    if(isIllusion == false)
                                    {
                                        zolAnim.SetBool("DieAlready", true);
                                        //Disappear
                                        StartCoroutine(WaitToDisableStuff(4, gameObject));
                                    }

                                    else
                                    {
                                        Poof(gameObject.transform.position);
                                        StartCoroutine(WaitToDisableStuff(0.5f, gameObject));
                                    }
                                    
                                }
                                break;

                            default:
                                {

                                }
                                break;
                        }
                        #endregion
                    }
                    break;

                #endregion

                #region Bullet Hell Mode
                case AttackMode.BulletHell:
                    {

                        navAgent.Stop();

                        if (zoltranID == 1)
                        {
                            gameObject.transform.position = new Vector3(BulletHellController.instance.pos1.x, gameObject.transform.position.y,
                                                                        BulletHellController.instance.pos1.z);

                            RotateTowardsPlayer();
                            //gameObject.transform.rotation = BulletHellController.instance.rot1;
                           // gameObject.transform.LookAt(player.transform.position);
                        }

                        if (zoltranID == 2)
                        {
                            gameObject.transform.position = new Vector3(BulletHellController.instance.pos2.x, gameObject.transform.position.y,
                                                                        BulletHellController.instance.pos2.z);

                            RotateTowardsPlayer();
                            //gameObject.transform.rotation = BulletHellController.instance.rot2;
                            //gameObject.transform.LookAt(player.transform.position);
                        }

                        if (zoltranID == 3)
                        {
                            gameObject.transform.position = new Vector3(BulletHellController.instance.pos3.x, gameObject.transform.position.y,
                                                                        BulletHellController.instance.pos3.z);

                            RotateTowardsPlayer();
                            //gameObject.transform.rotation = BulletHellController.instance.rot3;
                            //gameObject.transform.LookAt(player.transform.position);
                        }


                        if (zCurrentBHHealth <= 0 && diedInBH == false)
                        {
                            BulletHellController.instance.totalDiedinBH += 1;
                            diedInBH = true;
                            lastPos = gameObject.transform.position;
                        }

                        if (diedInBH == true)
                        {
                            Poof(gameObject.transform.position);
                            navAgent.Stop();
                            gameObject.transform.position = waitingArea.position;
                            reappearFromBH = true;
                            currentAttackMode = AttackMode.Normal;
                            currentActivity = ZoltranActivity.Waiting;
                        }

                        #region Bullet Hell
                        if (BulletHellController.instance.patternNo == 1)
                        {

                            if (zoltranID == 1)
                            {
                                Tryshot();
                            }

                            if (zoltranID == 2)
                            {
                                StrafeShot();
                            }

                            if (zoltranID == 3)
                            {
                                StrafeShot();
                            }

                        }

                        if (BulletHellController.instance.patternNo == 2)
                        {

                            if (zoltranID == 1)
                            {
                                StrafeShot();
                            }

                            if (zoltranID == 2)
                            {
                                Tryshot();
                            }

                            if (zoltranID == 3)
                            {
                                Tryshot();
                            }

                        }

                        if (BulletHellController.instance.patternNo == 3)
                        {

                            if (zoltranID == 1)
                            {
                                Tryshot();
                            }

                            if (zoltranID == 2)
                            {
                                Tryshot();
                            }

                            if (zoltranID == 3)
                            {
                                Tryshot();
                            }

                        }



                        #endregion
                    }
                    break;
                #endregion
            }
        }

        if (currentActivity == ZoltranActivity.Waiting)
        {
            if (BulletHellController.instance.totalDiedinBH == 3)
            {
                zolAnim.SetTrigger("BhFinish");
                gameObject.transform.position = lastPos;
                currentAttackMode = AttackMode.Normal;
                currentState = ZoltranStates.Vanish;
                currentActivity = ZoltranActivity.NormalMoving;
                ResetRotations();
            }
            gameObject.transform.position = waitingArea.position;
        }
        


        //SpiralBurst();
        



    }

    void FixedUpdate()
    {
        CheckPlayerProximity();

        #region Walking Running Blend Tree & Nav Agent speed
        if (currentState == ZoltranStates.Moving || currentState == ZoltranStates.Chase)
        {
            distToWaypoint = Vector3.Distance(gameObject.transform.position, newWaypoint);

            if (distToWaypoint > 40)
            {
                if(spdPara <= 1)
                {
                    if(Time.time > delayPeriod)
                    {
                        delayPeriod = Time.time + increaseDelay;
                        spdPara += 0.1f;
                    }

                }

            }

            
            if (distToWaypoint < 20)
            {
                if(spdPara >= 0)
                {
                    if (Time.time > delayPeriod)
                    {
                        delayPeriod = Time.time + increaseDelay;
                        spdPara -= 0.1f;
                    }
                }

            }

            zolAnim.SetFloat("Speed", spdPara);

            #region Nav agent speed
            if (distToWaypoint > 5)
            {
                if (navAgent.speed <= maxrunningSpeed)
                {
                    if (Time.time > nextspdInc)
                    {
                        nextspdInc = Time.time + spdIncRate;
                        navAgent.speed += 1f;
                    }

                }

            }


            if (distToWaypoint < 10)
            {
                if (navAgent.speed >= 0)
                {
                    if (Time.time > nextspdInc)
                    {
                        nextspdInc = Time.time + spdIncRate;
                        navAgent.speed -= 1f;
                    }
                }

            }

            #endregion
        }
        #endregion


    }


    //public void SpawnIllusions()
    //{
    //    if (illusionList.Count < IllusionLimit)
    //    {
    //        Vector3 spawnLocation = new Vector3();
    //        spawnLocation = generateRandomPositionInArea();
    //        GameObject zolIllu = (GameObject)Instantiate(zoltranIllusions, spawnLocation, Quaternion.identity);
    //        zolIllu.SetActive(true);
    //        zolIllu.GetComponent<ZoltranController>().isIllusion = true;
    //        illusionList.Add(zolIllu);
    //    }

    //}

    public void Poof(Vector3 lastKnownPos)
    {
         vanishParticleGO = (GameObject)Instantiate(vanishParticle, lastKnownPos, Quaternion.identity);
         StartCoroutine(WaitToDestroyStuff(2, vanishParticleGO));
    }

    #region Attacks

    public void DoNormalRangeAttacks()
    {
        if (distToTarget >= 130f)
        {
            zolAnim.SetTrigger("BeamAtk");
            FireSoulBeam();
            temptime += Time.deltaTime;
            if (temptime > 2.5f)
            {
                currentState = ZoltranStates.FindWaypoint;
                temptime = 0;
                ResetRotations();
            }
        }

        if (distToTarget >= 100 && distToTarget <= 130)
        {
            zolAnim.SetTrigger("NormalAtk");
            StrafeShot();
            temptime += Time.deltaTime;
            if (temptime > 4f)
            {
                currentState = ZoltranStates.FindWaypoint;
                temptime = 0;
                ResetRotations();
            }
        }

        if (distToTarget >= 70 && distToTarget <= 100)
        {
            zolAnim.SetTrigger("NormalAtk");
            SoulShot();
            temptime += Time.deltaTime;
            if (temptime > 4f)
            {
                currentState = ZoltranStates.FindWaypoint;
                temptime = 0;
                ResetRotations();
            }
        }

        if (distToTarget >= 40 && distToTarget <= 70)
        {
            zolAnim.SetTrigger("NormalAtk");
            Tryshot();
            temptime += Time.deltaTime;
            if (temptime > 4f)
            {
                currentState = ZoltranStates.FindWaypoint;
                temptime = 0;
                ResetRotations();
            }
        }

        if (distToTarget >= 0 && distToTarget <= 40)
        {
            zolAnim.SetTrigger("NormalAtk");
            Tryshot();
            temptime += Time.deltaTime;
            if (temptime > 4f)
            {
                currentState = ZoltranStates.FindWaypoint;
                temptime = 0;
                ResetRotations();
            }
        }

        //temptime += Time.deltaTime;
        //if (temptime > 2.5f)
        //{
        //    currentState = ZoltranStates.FindWaypoint;
        //    temptime = 0;
        //    ResetRotations();
        //}
    }

    public void SoulShot()
    {
       //orbEnd2.transform.LookAt(player.transform.position);
       //orbEnd3.transform.LookAt(player.transform.position);
        //RotateTowardsPlayer();
       mouthEnd.transform.LookAt(new Vector3(player.transform.position.x,player.transform.position.y + 1, player.transform.position.z));

        if (Time.time > ssnextFire)
        {
            ssnextFire = Time.time + ssfireRate;
            //soulShotGO = (GameObject)Instantiate(soulShotBullet, orbEnd2.transform.position, orbEnd2.transform.rotation);
            //soulShotGO.GetComponent<Rigidbody>().velocity = orbEnd2.transform.forward * ssBulletSpeed;

            //soulShotGO = (GameObject)Instantiate(soulShotBullet, orbEnd3.transform.position, orbEnd3.transform.rotation);
            //soulShotGO.GetComponent<Rigidbody>().velocity = orbEnd3.transform.forward * ssBulletSpeed;
            soulShotGO = zoltranSoulShotPool.RetrieveInstance();
            if (soulShotGO)
            {
                soulShotGO.transform.position = mouthEnd.transform.position;
                soulShotGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.forward * ssBulletSpeed;
            }
        
        }
        
        //soulShotGO.transform.LookAt(player.transform.position);
        
        //yield return new WaitForSeconds(1f);

    }


    public void Tryshot()
    {

        if (Time.time > tsNextFire)
        {
            tsNextFire = Time.time + tsFireRate;
            soulShotGO = zoltranSoulShotPool.RetrieveInstance();
            if (soulShotGO)
            {
                soulShotGO.transform.position = mouthEnd.transform.position;
                soulShotGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.forward * tsBulletSpeed;
            }

            soulShotGO = zoltranSoulShotPool.RetrieveInstance();
            if (soulShotGO)
            {
                soulShotGO.transform.position = mouthEnd.transform.GetChild(0).position;
                soulShotGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.GetChild(0).forward * tsBulletSpeed;
            }

            soulShotGO = zoltranSoulShotPool.RetrieveInstance();
            if (soulShotGO)
            {
                soulShotGO.transform.position = mouthEnd.transform.GetChild(1).position;
                soulShotGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.GetChild(1).forward * tsBulletSpeed;
            }

            soulShotGO = zoltranSoulShotPool.RetrieveInstance();
            if (soulShotGO)
            {
                soulShotGO.transform.position = mouthEnd.transform.GetChild(2).position;
                soulShotGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.GetChild(2).forward * tsBulletSpeed;
            }

            soulShotGO = zoltranSoulShotPool.RetrieveInstance();
            if (soulShotGO)
            {
                soulShotGO.transform.position = mouthEnd.transform.GetChild(3).position;
                soulShotGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.GetChild(3).forward * tsBulletSpeed;
            }

        }
        
    }

    public void StrafeShot()
    {

        //if (mouthEnd.transform.GetChild(0).eulerAngles == new Vector3(0,30,0))
        //{
        //    mouthEnd.transform.GetChild(0).rotation = Quaternion.Lerp(mouthEnd.transform.GetChild(0).rotation, Quaternion.Euler(new Vector3(0, 330, 0)), strafeSpeed);
        //    //mouthEnd.transform.GetChild(0).RotateAround(mouthEnd.transform.GetChild(0).position, -mouthEnd.transform.GetChild(0).up, Time.deltaTime * burstTurnSpeed);
        //    Debug.Log("Turning");
        //}

        //if (mouthEnd.transform.GetChild(0).eulerAngles == new Vector3(0, 330, 0))
        //{
        //    mouthEnd.transform.GetChild(0).rotation = Quaternion.Lerp(mouthEnd.transform.GetChild(0).rotation, Quaternion.Euler(new Vector3(0, 30, 0)), strafeSpeed);
        //    //mouthEnd.transform.GetChild(0).RotateAround(mouthEnd.transform.GetChild(0).position, mouthEnd.transform.GetChild(0).up, Time.deltaTime * burstTurnSpeed);
        //    Debug.Log("Turning Back");
        //}

        //mouthEnd.transform.rotation = Quaternion.Euler(0, 30 * Mathf.Sin(Time.time * strafeSpeed), 0);

        strafeSpeedTime += Time.deltaTime;
        float phase = Mathf.Sin(strafeSpeedTime / strafeSpeed);
        mouthEnd.transform.localRotation = Quaternion.Euler(new Vector3(0, (phase * strafeAngle), 0));

        if (Time.time > strafeNextFire)
        {
            strafeNextFire = Time.time + strafeFireRate;

            soulShotGO = zoltranSoulShotPool.RetrieveInstance();
            if (soulShotGO)
            {
                soulShotGO.transform.position = mouthEnd.transform.position;
                soulShotGO.GetComponent<Rigidbody>().velocity = mouthEnd.transform.forward * strafeBulletSpeed;
            }

        }
    }

    public void FireSoulBeam()
    {
        //Fire
        GameObject s1 = (GameObject)Instantiate(soulBeam, mouthEnd.transform.position, mouthEnd.transform.rotation);
        s1.GetComponent<BeamParam>().SetBeamParam(s1.GetComponent<BeamParam>());

        GameObject s2 = (GameObject)Instantiate(innerBeam, mouthEnd.transform.position, mouthEnd.transform.rotation);
        s1.GetComponent<BeamParam>().SetBeamParam(s1.GetComponent<BeamParam>());
    }

    public void RushToPlayer()
    {
        navAgent.Resume();
        navAgent.SetDestination(player.transform.position);
        navAgent.speed = 25;
    }

    public void ChasePlayer()
    {
        

        float tempdistToTarget = Vector3.Distance(player.transform.position, transform.position);
        

        if(tempdistToTarget >= 35)
        {
            navAgent.Resume();
            navAgent.SetDestination(player.transform.position);

            if (spdPara <= 1)
            {
                if (Time.time > delayPeriod)
                {
                    delayPeriod = Time.time + increaseDelay;
                    spdPara += 0.1f;
                }

            }

            if (navAgent.speed <= maxrunningSpeed)
            {
                if (Time.time > nextspdInc)
                {
                    nextspdInc = Time.time + spdIncRate;
                    navAgent.speed += 1f;
                }

            }
            zolAnim.SetFloat("Speed", spdPara);
        }

        else
        {
            isPlayerClose = true;
            isPounced = false;
            navAgent.Stop();
            zolAnim.applyRootMotion = true;
        }


        if(isPlayerClose == true)
        {
            Debug.Log("player is close");
            if(isPounced == false)
            {
                Debug.Log("Pouncing");
                navAgent.Stop();
                zolAnim.SetTrigger("Pounce");
                isExplodeMode = true;
                isPounced = true;
            }
            
        }
    }

    public void Explode()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);
        foreach (Collider col in objectsInRange)
        {
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Exploded");
                //Minus Player health here 
                player.GetComponent<Health>().currentHealth -= explosionDamage;
                isExplodeMode = false;
                currentState = ZoltranStates.Vanish;
            }

            else
            {
                isExplodeMode = false;
                currentState = ZoltranStates.Vanish;
            }

            //StartCoroutine(WaitToSwitchNormalAttackStates(0.1f,ZoltranStates.Vanish));

        }


    }
    #endregion

    #region Technical stuff and calculations
    public void ResetRotations()
    {
        //orbEnd1.transform.rotation = Quaternion.identity;
        mouthEnd.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        mouthEnd.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 30, 0));
        //Debug.Log("Resetted rotation");
    }

    public void CheckPlayerProximity()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(gameObject.transform.position, 5);
        foreach (Collider col in objectsInRange)
        {
            if (col.gameObject.tag == "Player")
            {
                currentState = ZoltranStates.Vanish;

                //For debugging explosion damage
                player.GetComponent<Health>().currentHealth -= explosionDamage;
            }

            if(col.gameObject.tag == "Player" && isExplodeMode == true)
            {
                Explode();
            }
        }
    }


    public Vector3 generateRandomPositionInArea()
    {
        float xTerrainMin = MovementArea.GetComponent<Collider>().bounds.min.x;
        float xTerrainMax = MovementArea.GetComponent<Collider>().bounds.max.x;
        float zTerrainMin = MovementArea.GetComponent<Collider>().bounds.min.z;
        float zTerrainMax = MovementArea.GetComponent<Collider>().bounds.max.z;
        Vector3 position = new Vector3(Random.Range(xTerrainMin, xTerrainMax), gameObject.transform.position.y, Random.Range(zTerrainMin, zTerrainMax));
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 5f, 1);
        position = hit.position;
        return position;
        //GameObject go = (GameObject)Instantiate(zoltran, position, Quaternion.identity);
    }


    public void RotateTowardsPlayer()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        targetDir.y = 0;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void IncreaseBlendTreeSpeedAndNavSpeed(float tempDist)
    {
        //distToWaypoint = Vector3.Distance(gameObject.transform.position, newWaypoint);

        if (tempDist > 40)
        {
            if (spdPara <= 1)
            {
                if (Time.time > delayPeriod)
                {
                    delayPeriod = Time.time + increaseDelay;
                    spdPara += 0.1f;
                }

            }

        }


        if (tempDist < 20)
        {
            if (spdPara >= 0)
            {
                if (Time.time > delayPeriod)
                {
                    delayPeriod = Time.time + increaseDelay;
                    spdPara -= 0.1f;
                }
            }

        }

        zolAnim.SetFloat("Speed", spdPara);

        #region Nav agent speed
        if (tempDist > 20)
        {
            if (navAgent.speed <= maxrunningSpeed)
            {
                if (Time.time > nextspdInc)
                {
                    nextspdInc = Time.time + spdIncRate;
                    navAgent.speed += 1f;
                }

            }

        }


        if (tempDist < 5)
        {
            if (navAgent.speed >= 0)
            {
                if (Time.time > nextspdInc)
                {
                    nextspdInc = Time.time + spdIncRate;
                    navAgent.speed -= 1f;
                }
            }

        }

        #endregion
    }



    public int CalculateRNGForAttack(int maxNumber)
    {
        int rng = Random.Range(0, maxNumber);
        return rng;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (currentAttackMode == AttackMode.Normal)
            {
                if (!isIllusion)
                {
                    zCurrentHealth -= playerBulletDamage;
                }

                else
                {
                    zCurrentHealth -= (playerBulletDamage * 5);
                }
                
            }

            if (currentAttackMode == AttackMode.BulletHell)
            {
                zCurrentBHHealth -= playerBulletDamage;
            }
        }

        if(other.gameObject.tag == "EoESkill")
        {
            if (!isIllusion)
            {
                if(currentAttackMode == AttackMode.Normal)
                {
                    currentState = ZoltranStates.Stunned;
                    //Play animation
                    zolAnim.SetBool("Stunned", true);
                    navAgent.Stop();
                    navAgent.speed = 0;
                    spdPara = 0;
                    zolAnim.SetFloat("Speed", 0);
                    //StopAllCoroutines();
                }

                if(currentAttackMode == AttackMode.BulletHell)
                {
                    zCurrentBHHealth = 0;
                }
            }

            else
            {
                currentState = ZoltranStates.Vanish;
            }
        }
    }

    public void CheatToClearLevel()
    {
        isPattern1 = true;
        isPattern2 = true;
        isPattern3 = true;

        if (!isIllusion)
        {
            zCurrentHealth = 10;
        }
    }

    public void CheatToShowMainZoltran()
    {
        if (isIllusion)
        {
            zolAura.SetActive(false);
        }
    }
    #endregion

    #region IEnumerators
    private IEnumerator WaitToSwitchNormalAttackStates(float sec, ZoltranStates state)
    {
        yield return new WaitForSeconds(sec);
        currentState = state;
    }

    private IEnumerator SwitchToAttackState()
    {
        
        yield return new WaitForSeconds(3);
        currentState = ZoltranStates.Attacking;
    }

    private IEnumerator SwitchToAppearState(float sec)
    {
        yield return new WaitForSeconds(sec);
        currentState = ZoltranStates.Appear;
    }

    //private IEnumerator WaitToExplode()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    foreach(GameObject zol in illusionList)
    //    {
    //        if(zol.GetComponent<ZoltranController>().exploded == true)
    //        {
    //            illusionList.Remove(zol);
    //        }
    //    }
    //    Destroy(gameObject);
    //}

    private IEnumerator WaitToDestroyStuff(float sec, GameObject thingToDestroy)
    {
        yield return new WaitForSeconds(sec);
        Destroy(thingToDestroy);
    }

    private IEnumerator WaitToDisableStuff(float sec, GameObject thingToDisable)
    {
        yield return new WaitForSeconds(sec);
        thingToDisable.SetActive(false);
    }

    private IEnumerator WaitToSpawnIllusionsAtStart(float sec)
    {
        yield return new WaitForSeconds(sec);
       // Poof(gameObject.transform.position);
        Poof(gameObject.transform.position);
        //illusionOne.transform.position = generateRandomPositionInArea();
        //illusionTwo.transform.position = generateRandomPositionInArea();
        illusionOne.SetActive(true);
        illusionTwo.SetActive(true);
        illuAppear = true;
    }
    #endregion

    //GameObject terrain = GameObject.FindWithTag ("terrainHome");
//  float xTerrainMin = terrain.GetComponent<Renderer>().bounds.min.x;
//  float xTerrainMax = terrain.GetComponent<Renderer>().bounds.max.x;
//  float zTerrainMin = terrain.GetComponent<Renderer>().bounds.min.z;
//  float zTerrainMax = terrain.GetComponent<Renderer>().bounds.max.z;
//  Vector3 position = new Vector3(Random.Range(xTerrainMin, xTerrainMax), 0, Random.Range(zTerrainMin, zTerrainMax));
//  NavMeshHit hit;
//  NavMesh.SamplePosition(position, out hit, 10f, 1);
//  position = hit.position;
//  return position;
   
}

[System.Serializable]
public class AttackInfo
{
    public AnimationClip Anim;
    public float coolDown;
    public float damage;
    public AttackType attackType = AttackType.ShadowShot;
    public string TargetFunctionName = "";
}

public enum AttackType
{
    ShadowShot = 0,
    Duplicate = 1,
    TriShot = 2,

}
