using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnmarController : MonoBehaviour {

    #region FSM
    public enum FSMState { WaitForGameToStart, Walking, AttackDelayState, Attacking, AnimationPlaying, LaserAttack, Dying, GameOver}
    public enum LaserState { Aiming, Charging, Shooting, ShootFinish}

    [Header("Enmar FSM current state")]
    public FSMState enmarState;
    [Header("Laser attack current state")]
    public LaserState laserStatus;
    #endregion

    #region Normal Attack
    //Gameobjects
    [Header("Normal Attack")]
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject attackArea1, attackArea2;

    public bool hasEnteredArea1 = false;
    public bool hasEnteredArea2 = false;

    bool isRightHandAttack = false;
    bool isLeftHandAttack = false;

    //Float
    public float slamDamage = 20;
    public float wallDamage = 20;
    public float attackIntervalDelay = 6f;
    public float attackTime;
    public float tempTime;
    public float slamWaitDelay = 5f;
    #endregion

    #region Laser Attack
    //Gameobjects
    [Header("Laser Attack")]
    public GameObject laserBeam;
    public GameObject laserCharge;
    public GameObject laserOrigin;
    public GameObject laserWarningCircle;

    [HideInInspector]
    public GameObject laserChargeGO;
    [HideInInspector]
    public GameObject laserBeamGO;
    [HideInInspector]
    public GameObject laserWarningCircleGO;
    Transform chargePulse;
    public GameObject player;
    [HideInInspector]
    public Vector3 playerLastPos;
    

    //bool
    public bool isCharging = false;

    //Float
    public float rotationValue;
    public float laserDamage = 10;

    //Variables
    [Header("Time variables")]
    public float walkTime;
    public float laserChargeTime;

    #endregion

    #region Health
    [Header("Health")]
    public float enmarMaxHealth = 100;
    public float enmarCurrentHealth;

    [HideInInspector]
    public bool enmarIsDead = false;

    #endregion

    #region Transform Points
    [Header("Transform Points")]
    public GameObject enmarOrigin;
    public Transform pointToWalkTo;
    //[HideInInspector]
    public bool reached = false;
    #endregion

    #region Animation
    [Header("Animation")]
    public Animator enmarAnim;

    Hashtable walkHash = new Hashtable();
    #endregion

    #region Misc
    public CapsuleCollider[] bodyColliders;
    public TerrainCollider terrainCollider;

    public GameObject fpsCamera;

    public GameObject dustParticles;
    [HideInInspector]
    public GameObject dustParticlesGO;

    bool isPlayerGrounded;
    #endregion

    public static EnmarController instance { get; set; }

    void Awake()
    {
        //walkHash.Add("position", pointToWalkTo.transform.position);
        //walkHash.Add("speed", 30);
        //walkHash.Add("time", 5);
        //walkHash.Add("easetype", iTween.EaseType.linear);
    }
    // Use this for initialization
    void Start () {

        instance = this;
        enmarState = FSMState.WaitForGameToStart;
        enmarAnim = gameObject.GetComponent<Animator>();
        enmarCurrentHealth = enmarMaxHealth;

        //enmarState = FSMState.Attacking;
        

        IgnoreBodyGroundCollisions();

        
	}
	
	// Update is called once per frame
	void Update () {
        //laserBeam.transform.Rotate(5, 10, 5);
        Debug.DrawRay(player.gameObject.transform.position, Vector3.down);
        //enmarHead.transform.Rotate()
        isPlayerGrounded = FirstPersonController.instance.m_CharacterController.isGrounded;

        if(enmarCurrentHealth <= 0)
        {   
            enmarState = FSMState.Dying;
        }
        //Player health, someone's naming anyhow
        if(Health.instance.currentHealth <= 0)
        {
            enmarState = FSMState.GameOver;
        }
        if (enmarIsDead == false)
        {
            switch (enmarState)
            {
                case FSMState.Walking:
                    {
                        //play animation
                        walkTime += Time.deltaTime;
                        //walk
                        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pointToWalkTo.transform.position, Time.deltaTime * 20);
                        //iTween.MoveTo(gameObject, pointToWalkTo.transform.position, 60);
                        //iTween.MoveTo(gameObject, walkHash);
                        //if (gameObject.transform.position == pointToWalkTo.transform.position)
                        //if(walkTime > 5)
                        //shakeCamera(3, 1);
                        enmarAnim.SetTrigger("StartMoving");
                        if (reached == true)
                        {
                            enmarAnim.SetTrigger("Reached");
                            //enmarAnim.Stop();
                            //rightHand.SetActive(true);
                            //leftHand.SetActive(true);
                            //enmarAnim.SetBool("ReachedDestination", true);
                            enmarState = FSMState.AttackDelayState;
                        }
                    }
                    break;

                case FSMState.AttackDelayState:
                    {
                        //rightHand.transform.position = Vector3.Lerp(rightHand.transform.position,new Vector3(2.07f, 37.2f, 70f), Time.deltaTime);
                        //leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, new Vector3(-21.7f, 37.2f, 70f),Time.deltaTime);
                        //rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, new Vector3(2021.15f, 74.36f, 336.36f), Time.deltaTime);
                        // leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, new Vector3(1985.69f, 74.36f, 336.36f), Time.deltaTime);
                        
                        isLeftHandAttack = false;
                        isRightHandAttack = false;
                        
                        attackTime += Time.deltaTime;
                        if (attackTime > attackIntervalDelay)
                        {
                            attackTime = 0;
                            enmarState = FSMState.Attacking;
                            enmarAnim.ResetTrigger("FinishedRightAttack");
                            enmarAnim.ResetTrigger("FinishedLeftAttack");
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
                                    DetectAreaToAttack();
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
                                    DetectAreaToAttack();
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
                                    DetectAreaToAttack();
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
                        if(isRightHandAttack == true)
                        {
                            iTween.RotateTo(gameObject, new Vector3(0, 128.257f, 0), 3);
                        }
                        if(isLeftHandAttack == true)
                        {
                            //iTween.RotateTo(gameObject, new Vector3(0, 130.724f, 0), 3);
                        }

                        tempTime += Time.deltaTime;
                        if (tempTime > slamWaitDelay)
                        {
                            tempTime = 0;
                            enmarAnim.SetTrigger("FinishedRightAttack");
                            enmarAnim.SetTrigger("FinishedLeftAttack");
                            //ResetRotation();ds
                            enmarState = FSMState.AttackDelayState;
                            Destroy(dustParticlesGO);
                        }
                    }
                    break;

                #region FSM Laser Attack
                case FSMState.LaserAttack:
                    {
                        switch (laserStatus)
                        {
                            case LaserState.Aiming:
                                {
                                    //if (isPlayerGrounded == true)
                                    //{
                                    //    DetectFloorBelowPlayer();
                                    //    GetPlayerLocation();
                                    //    ShowLaserWarningCircle();
                                        attackTime = 0;
                                        laserStatus = LaserState.Charging;
                                   // }

                                }
                                break;

                            case LaserState.Charging:
                                {
                                    laserChargeTime += Time.deltaTime;
                                    if (isCharging == false)
                                    {
                                        ChargeLaser();
                                    }

                                    //float lerpValue = time / 3;
                                    //lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

                                    chargePulse.localScale = Vector3.Lerp(chargePulse.localScale, new Vector3(10, 10, 10), Time.deltaTime * 0.5f);

                                    if (laserChargeTime > 8)
                                    {
                                        if (isPlayerGrounded == true)
                                        {
                                            DetectFloorBelowPlayer();
                                            GetPlayerLocation();
                                            ShowLaserWarningCircle();
                                            
                                        }

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
                                    //laserBeamGO.transform.LookAt(player.transform.position);
                                    laserBeamGO.transform.LookAt(playerLastPos);
                                    laserStatus = LaserState.ShootFinish;
                                }
                                break;

                            case LaserState.ShootFinish:
                                {

                                    Destroy(laserWarningCircleGO);
                                    enmarState = FSMState.AttackDelayState;
                                    laserStatus = LaserState.Aiming;
                                }
                                break;
                        }
                    }
                    break;
                #endregion


                case FSMState.Dying:
                    {
                        enmarAnim.SetBool("EnmarDead", true);
                        Destroy(laserChargeGO);
                        //iTween.RotateTo(gameObject, new Vector3(0, 80, 0), 3);
                    }
                    break;

                case FSMState.GameOver:
                    {

                    }
                    break;
                default:
                    {

                    }
                    break;
            }
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
        if(isPlayerGrounded == true)
        {
            playerLastPos = player.transform.position;
        }
        
    }

    public void ChargeLaser()
    {
        isCharging = true;
        
        //if(time < )
        laserChargeGO = (GameObject)Instantiate(laserCharge, laserOrigin.transform.position, laserOrigin.transform.rotation,laserOrigin.transform);
        laserChargeGO.SetActive(true);
        chargePulse = laserChargeGO.transform.GetChild(0).transform;
        chargePulse.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        laserChargeGO.transform.position = laserOrigin.transform.position;
        laserChargeGO.transform.rotation = laserOrigin.transform.rotation; 
       
    }

    public void ShowLaserWarningCircle()
    {
        if(isPlayerGrounded == true)
        {
            laserWarningCircleGO = (GameObject)Instantiate(laserWarningCircle, playerLastPos, player.transform.rotation);
        }
        
    }

    public void ShootLaserBeam()
    {
        laserBeamGO = (GameObject)Instantiate(laserBeam, laserOrigin.transform.position, laserOrigin.transform.rotation);
        laserBeamGO.SetActive(true);
    }

    public void PrimeRightHand()
    {
        //rightHand.SetActive(true);
        //rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, new Vector3(rightHand.transform.position.x, 20, rightHand.transform.position.z), Time.deltaTime);
        enmarAnim.SetTrigger("AttackRight");
        isRightHandAttack = true;
        //enmarState = FSMState.AttackDelayState;
    }

    public void PrimeLeftHand()
    {
        //leftHand.SetActive(true);
        //leftHand.GetComponent<Rigidbody>().AddForce(Vector3.down * 100000);
        //leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, new Vector3(leftHand.transform.position.x, 20, leftHand.transform.position.z), Time.deltaTime);
        enmarAnim.SetTrigger("AttackLeft");
        isLeftHandAttack = true;
        //iTween.RotateTo(gameObject, new Vector3(0, 124, 0), 3);
        //enmarState = FSMState.AttackDelayState;
    }

    public void DetectFloorBelowPlayer()
    {
        RaycastHit hit;
        Ray laserRay = new Ray(player.gameObject.transform.position,Vector3.down);
        if (Physics.Raycast(laserRay, out hit))
        {
            if(hit.normal.y == 90)
            {
                Debug.Log("Square Up");
            }
        }
    }


    public void IgnoreBodyGroundCollisions()
    {
        foreach(CapsuleCollider go in bodyColliders)
        {
            Physics.IgnoreCollision(go, terrainCollider);
        }
    }

    public void SetEnmarDead()
    {
        enmarIsDead = true;
    }

    public void DamageWall()
    {
        Level1Controller.instance.currentWallHealth -= wallDamage;
    }

    public void shakeCamera()
    {
        player.GetComponent<CameraShake>().ShakeCamera(0.2f, 0.2f);
    }

    public void ResetRotation()
    {
        iTween.RotateTo(gameObject, new Vector3(0, 136.638f, 0), 3);
    }

    public void SpawnDustParticle()
    {
        if (isRightHandAttack)
        {
            dustParticlesGO = (GameObject)Instantiate(dustParticles, rightHand.transform.position, rightHand.transform.rotation);
        }

        if (isLeftHandAttack)
        {
            dustParticlesGO = (GameObject)Instantiate(dustParticles, leftHand.transform.position, leftHand.transform.rotation);
        }
    }
   
}
