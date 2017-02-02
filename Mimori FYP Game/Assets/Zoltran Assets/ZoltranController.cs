using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoltranController : MonoBehaviour {

    public enum ZoltranStates { Start, FindWaypoint, Moving, Attacking, Vanish, Appear, Chase, Dying}
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
    public bool isPattern1 = false;
    public bool isPattern2 = false;
    public bool isPattern3 = false;
    public bool isIllusion = false;
    public bool diedInBH = false;

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
    public GameObject orbEnd1;
    public GameObject orbEnd2;
    public GameObject orbEnd3;


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
    public GameObject mainZoltran;
    #endregion

    #region Misc
    [Header("Misc")]
    public GameObject player;
    public GameObject MovementArea;
    public GameObject zoltranIllusions;
    public float temptime = 0;
    NavMeshAgent navAgent;
    Vector3 newWaypoint;
    private float distToTarget;
    #endregion

    
    

    public static ZoltranController instance { get; set; }
	// Use this for initialization
	void Start () 
    {
        instance = this;
        navAgent = GetComponent<NavMeshAgent>();
        //currentState = ZoltranStates.FindWaypoint;
        zCurrentHealth = zMaxHealth;
        isExplodeMode = false;
        exploded = false;

        if(isIllusion == false)
        {
            illusionList.Clear();
        }
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log("Illusion List COunt: " + illusionList.Count);
        Debug.DrawRay(orbEnd1.transform.position, orbEnd1.transform.forward, Color.red);

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
                            if (zCurrentHealth <= 80 && illusionList.Count == 2 && isPattern1 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.One;
                                isPattern1 = true;
                                BulletHellController.instance.patternNo = 1;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                currentAttackMode = AttackMode.BulletHell;
                            }

                            if (zCurrentHealth <= 60 && illusionList.Count == 2 && isPattern2 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Two;
                                isPattern2 = true;
                                BulletHellController.instance.patternNo = 2;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                currentAttackMode = AttackMode.BulletHell;
                            }

                            if (zCurrentHealth <= 30 && illusionList.Count == 2 && isPattern3 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Three;
                                isPattern3 = true;
                                BulletHellController.instance.patternNo = 3;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                currentAttackMode = AttackMode.BulletHell;
                            }
                        }

                        else
                        {
                            if (mainZoltran.GetComponent<ZoltranController>().zCurrentHealth <= 80 && illusionList.Count == 2 && isPattern1 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.One;
                                isPattern1 = true;
                                BulletHellController.instance.patternNo = 1;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                currentAttackMode = AttackMode.BulletHell;
                            }

                            if (mainZoltran.GetComponent<ZoltranController>().zCurrentHealth <= 60 && illusionList.Count == 2 && isPattern2 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Two;
                                isPattern2 = true;
                                BulletHellController.instance.patternNo = 2;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                currentAttackMode = AttackMode.BulletHell;
                            }

                            if (mainZoltran.GetComponent<ZoltranController>().zCurrentHealth <= 30 && illusionList.Count == 2 && isPattern3 == false)
                            {
                                BulletHellController.instance.currentPattern = BulletHellController.Pattern.Three;
                                isPattern3 = true;
                                BulletHellController.instance.patternNo = 3;
                                BulletHellController.instance.totalDiedinBH = 0;
                                ResetRotations();
                                currentAttackMode = AttackMode.BulletHell;
                            }
                        }

                        if (zCurrentHealth <= 0) {
                            currentState = ZoltranStates.Dying;
                        }

                        #region Normal Mode
                        switch (currentState)
                        {
                            case ZoltranStates.Start:
                                {
                                    if (isIllusion == true)
                                    {
                                        if (illusionList.Count == 1)
                                        {
                                            illusionList[0].gameObject.GetComponent<ZoltranController>().zoltranID = 2;
                                        }

                                        if (illusionList.Count == 2)
                                        {
                                            illusionList[1].gameObject.GetComponent<ZoltranController>().zoltranID = 3;
                                        }
                                    }
                                    

                                    currentState = ZoltranStates.FindWaypoint;
                                }
                                break;

                            case ZoltranStates.FindWaypoint:
                                {
                                    //Find random point to move to
                                    newWaypoint = generateRandomPositionInArea();
                                    ResetRotations();
                                    currentState = ZoltranStates.Moving;
                                }
                                break;

                            case ZoltranStates.Moving:
                                {
                                    navAgent.Resume();
                                    navAgent.SetDestination(newWaypoint);
                                    if (gameObject.transform.position == newWaypoint)
                                    {
                                        RotateTowardsPlayer();
                                        attackRNG = CalculateRNGForAttack(2);
                                        distToTarget = Vector3.Distance(player.transform.position, transform.position);
                                        //currentState = ZoltranStates.Attacking;
                                        StartCoroutine(SwitchToAttackState());
                                    }
                                }
                                break;

                            case ZoltranStates.Attacking:
                                {
                                    //transform.LookAt(player.transform.position);

                                    navAgent.Stop();


                                    if (distToTarget >= 50f)
                                    {
                                        FireSoulBeam();
                                    }

                                    if (distToTarget >= 40 && distToTarget <= 50)
                                    {
                                        StrafeShot();
                                    }

                                    if (distToTarget >= 30 && distToTarget <= 40)
                                    {
                                        SoulShot();
                                    }

                                    if (distToTarget >= 20 && distToTarget <= 30)
                                    {
                                        Tryshot();
                                    }

                                    //switch (attackRNG)
                                    //{
                                    //    case 0:
                                    //        {
                                    //            //SoulShot();
                                    //            //SpiralBurstRight();
                                    //            //FireSoulBeam();
                                    //            //SpiralBurstLeft();
                                    //            //Tryshot();
                                    //            StrafeShot();
                                    //        }
                                    //        break;

                                    //    case 1:
                                    //        {
                                    //            //SpiralBurstLeft();
                                    //            //FireSoulBeam();
                                    //            //SoulShot();
                                    //            // Tryshot();
                                    //            StrafeShot();
                                    //        }
                                    //        break;

                                    //    case 3:
                                    //        {
                                    //            //SoulShot();
                                    //            //FireSoulBeam();
                                    //            //SpiralBurstLeft();
                                    //            //Tryshot();
                                    //            StrafeShot();
                                    //        }
                                    //        break;
                                    //}
                                    //}

                                    //if (isIllusion == true)
                                    //    {
                                    //        currentState = ZoltranStates.Chase;
                                    //        isExplodeMode = true;
                                    //    }

                                    //SpiralBurst();
                                    //navAgent.stoppingDistance
                                    //if (attackTime <= Time.deltaTime)
                                    //{
                                    //    attackRNG = Random.Range(0, actions.Count);

                                    //    attackTime = Time.deltaTime + actions[attackRNG].coolDown;

                                    //    gameObject.GetComponent<Animation>().Play(actions[attackRNG].Anim.name);

                                    //    playerTarget.SendMessageUpwards(actions[0].TargetFunctionName, actions[0].damage, SendMessageOptions.DontRequireReceiver);
                                    //}
                                    temptime += Time.deltaTime;
                                    if (temptime > 5)
                                    {
                                        currentState = ZoltranStates.FindWaypoint;
                                        temptime = 0;
                                        ResetRotations();
                                        if (isIllusion == false && illusionList.Count < IllusionLimit)
                                        {
                                            SpawnIllusions();
                                        }

                                    }
                                }
                                break;

                            case ZoltranStates.Vanish:
                                {
                                    Poof(gameObject.transform.position);
                                    //gameObject.transform.position = new Vector3(5000, 0, 5000);
                                    gameObject.SetActive(false);
                                    newWaypoint = generateRandomPositionInArea();
                                    temptime = 0;
                                    StartCoroutine(SwitchToAppearState());

                                }
                                break;


                            case ZoltranStates.Appear:
                                {
                                    gameObject.transform.position = newWaypoint;
                                    RotateTowardsPlayer();
                                    ResetRotations();
                                    StartCoroutine(SwitchToAttackState());
                                }
                                break;

                            case ZoltranStates.Chase:
                                {
                                    RushToPlayer();
                                }
                                break;

                            case ZoltranStates.Dying:
                                {
                                    Debug.Log("Dead");
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

                            gameObject.transform.rotation = BulletHellController.instance.rot1;
                        }

                        if (zoltranID == 2)
                        {
                            gameObject.transform.position = new Vector3(BulletHellController.instance.pos2.x, gameObject.transform.position.y,
                                                                        BulletHellController.instance.pos2.z);

                            gameObject.transform.rotation = BulletHellController.instance.rot2;
                        }

                        if (zoltranID == 3)
                        {
                            gameObject.transform.position = new Vector3(BulletHellController.instance.pos3.x, gameObject.transform.position.y,
                                                                        BulletHellController.instance.pos3.z);

                            gameObject.transform.rotation = BulletHellController.instance.rot3;
                        }


                        if (zCurrentBHHealth <= 0 && diedInBH == false)
                        {
                            BulletHellController.instance.totalDiedinBH += 1;
                            diedInBH = true;
                        }

                        if (diedInBH == true)
                        {
                            Poof(gameObject.transform.position);
                            gameObject.transform.position = new Vector3(1000, 0, 1000);
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
                currentAttackMode = AttackMode.Normal;
                currentState = ZoltranStates.Vanish;
                currentActivity = ZoltranActivity.NormalMoving;
                ResetRotations();
            }
        }
        


        //SpiralBurst();
        



    }

    void FixedUpdate()
    {
        CheckPlayerProximity();
    }


    public void SpawnIllusions()
    {
        if (illusionList.Count < IllusionLimit)
        {
            Vector3 spawnLocation = new Vector3();
            spawnLocation = generateRandomPositionInArea();
            GameObject zolIllu = (GameObject)Instantiate(zoltranIllusions, spawnLocation, Quaternion.identity);
            zolIllu.SetActive(true);
            zolIllu.GetComponent<ZoltranController>().isIllusion = true;
            illusionList.Add(zolIllu);
        }
        
    }

    public void Poof(Vector3 lastKnownPos)
    {
         vanishParticleGO = (GameObject)Instantiate(vanishParticle, lastKnownPos, Quaternion.identity);
         StartCoroutine(WaitToDestroyStuff(2, vanishParticleGO));
    }

    #region Attacks
    public void SoulShot()
    {
       //orbEnd2.transform.LookAt(player.transform.position);
       //orbEnd3.transform.LookAt(player.transform.position);
        //RotateTowardsPlayer();
       mouthEnd.transform.LookAt(player.transform.position);

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

    //public void SpiralBurstLeft()
    //{
    
    //    orbEnd1.transform.RotateAround(orbEnd1.transform.position, orbEnd1.transform.up, Time.deltaTime * burstTurnSpeed);

        
    //    if (Time.time > tsNextFire)
    //                {
    //                    tsNextFire = Time.time + tsFireRate;
    //        //    SoulChain();
    //        //tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(0).position, orbEnd1.transform.GetChild(0).rotation);
    //        //tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(0).forward * tsBulletSpeed;

    //        //tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(2).position, orbEnd1.transform.GetChild(2).rotation);
    //        //tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(2).forward * tsBulletSpeed;

    //        //tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(4).position, orbEnd1.transform.GetChild(4).rotation);
    //        //tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(4).forward * tsBulletSpeed;

    //        //tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(6).position, orbEnd1.transform.GetChild(6).rotation);
    //        //tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(6).forward * tsBulletSpeed;

    //        soulShotGO = zoltranSoulShotPool.RetrieveInstance();
    //        if (soulShotGO)
    //        {
    //            soulShotGO.transform.position = mouthEnd.transform.position;
    //            soulShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(0).forward * tsBulletSpeed;
    //        }

    //        go = zoltranSoulShotPool.RetrieveInstance();
    //        if (go)
    //        {
    //            go.transform.position = mouthEnd.transform.position;
    //            go.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(2).forward * tsBulletSpeed;
    //        }

    //        go = zoltranSoulShotPool.RetrieveInstance();
    //        if (go)
    //        {
    //            go.transform.position = mouthEnd.transform.position;
    //            go.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(4).forward * tsBulletSpeed;
    //        }

    //        go = zoltranSoulShotPool.RetrieveInstance();
    //        if (go)
    //        {
    //            go.transform.position = mouthEnd.transform.position;
    //            go.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(6).forward * tsBulletSpeed;
    //        }
    //    }    
    //}

    //public void SpiralBurstRight()
    //{

    //    orbEnd1.transform.RotateAround(orbEnd1.transform.position, orbEnd1.transform.up, Time.deltaTime * -burstTurnSpeed);


    //    if (Time.time > tsNextFire)
    //    {
    //        tsNextFire = Time.time + tsFireRate;
    //        //    SoulChain();
    //        tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(0).position, orbEnd1.transform.GetChild(0).rotation);
    //        tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(0).forward * tsBulletSpeed;

    //        tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(2).position, orbEnd1.transform.GetChild(2).rotation);
    //        tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(2).forward * tsBulletSpeed;

    //        tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(4).position, orbEnd1.transform.GetChild(4).rotation);
    //        tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(4).forward * tsBulletSpeed;

    //        tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(6).position, orbEnd1.transform.GetChild(6).rotation);
    //        tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(6).forward * tsBulletSpeed;
    //    }
    //}

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
    }

    public void Explode()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);
        foreach (Collider col in objectsInRange)
        {
            if (col.gameObject.tag == "Player")
            {
                //Minus Player health here 
            }

            StartCoroutine(WaitToExplode());

        }


    }
    #endregion

    #region Technical stuff and calculations
    public void ResetRotations()
    {
        orbEnd1.transform.rotation = Quaternion.identity;
        mouthEnd.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        mouthEnd.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 30, 0));
        //Debug.Log("Resetted rotation");
    }

    public void CheckPlayerProximity()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(gameObject.transform.position, 4);
        foreach (Collider col in objectsInRange)
        {
            if (col.gameObject.tag == "Player")
            {
                currentState = ZoltranStates.Vanish;
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
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
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
                    zCurrentHealth -= 5;
                }

                else
                {
                    gameObject.GetComponent<ZoltranIllusionController>().illusionCurrentHealth -= 5;
                }
                
            }

            if (currentAttackMode == AttackMode.BulletHell)
            {
                zCurrentBHHealth -= 5;
            }
        }
    }
    #endregion

    #region IEnumerators
    private IEnumerator SwitchToAttackState()
    {
        
        yield return new WaitForSeconds(1);
        currentState = ZoltranStates.Attacking;
    }

    private IEnumerator SwitchToAppearState()
    {
        yield return new WaitForSeconds(2);
        currentState = ZoltranStates.Appear;
    }

    private IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(GameObject zol in illusionList)
        {
            if(zol.GetComponent<ZoltranController>().exploded == true)
            {
                illusionList.Remove(zol);
            }
        }
        Destroy(gameObject);
    }

    private IEnumerator WaitToDestroyStuff(float sec, GameObject thingToDestroy)
    {
        yield return new WaitForSeconds(sec);
        Destroy(thingToDestroy);
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
