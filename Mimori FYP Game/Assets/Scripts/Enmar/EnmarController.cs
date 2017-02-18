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

    public static bool enmarDied = false;
    #endregion

    #region Normal Attack
    //Gameobjects
    [Header("Normal Attack")]
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject attackArea1, attackArea2;

    public AudioClip slamRoar;
    public AudioClip slam;
    public AudioClip stompSound;
    public AudioClip dyingSound;

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
    //Old
    public GameObject laserBeam;
    public GameObject laserCharge;
    public GameObject laserOrigin;
    public GameObject laserWarningCircle;

    //New
    public GameObject newLaserBeam;
    public GameObject newLaserInnerBeam;
    public Transform newLaserBeamParent;

    public AudioClip charging;
    public AudioClip shooting;

    public bool chargingSoundPlayed = false;
    public bool shootingSoundPlayed = false;

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

    public GameObject leftSide;
    public GameObject rightSide;

    bool isPlayerGrounded;
    bool dyingSoundPlayed = false;

    private int gameoverState = 1;
    public Transform gameoverTargetPoint;
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

        CheatToSetEnmarHealthTo20();

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
                        Destroy(laserWarningCircleGO);
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
                            iTween.RotateTo(gameObject, new Vector3(0, 142.484f, 0), 3);
                            
                        }
                        if(isLeftHandAttack == true)
                        {
                            iTween.RotateTo(gameObject, new Vector3(0, 127.458f, 0), 3);
                            
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
                                    if (!chargingSoundPlayed) {
                                        GetComponent<AudioSource>().clip = charging;
                                        GetComponent<AudioSource>().volume = 0.7f;
                                        GetComponent<AudioSource>().pitch = 1f;
                                        GetComponent<AudioSource>().Play();
                                        shootingSoundPlayed = false;
                                        chargingSoundPlayed = true;
                                    }
                                    laserChargeTime += Time.deltaTime;
                                    if (isCharging == false)
                                    {
                                        ChargeLaser();
                                    }

                                    //float lerpValue = time / 3;
                                    //lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

                                    chargePulse.localScale = Vector3.Lerp(chargePulse.localScale, new Vector3(10, 10, 10), Time.deltaTime * 0.5f);

                                    if (laserChargeTime > 4)
                                    {
                                        if (isPlayerGrounded == true)
                                        {
                                            DetectFloorBelowPlayer();
                                            GetPlayerLocation();
                                            ShowLaserWarningCircle();
                                            
                                        }

                                        GetPlayerLocation();
                                        laserChargeTime = 0;
                                        Destroy(laserChargeGO);
                                        laserOrigin.transform.LookAt(new Vector3(playerLastPos.x,playerLastPos.y + 1.5f, playerLastPos.z));
                                        laserStatus = LaserState.Shooting;

                                    }

                                    //laserStatus = LaserState.Shooting;
                                }
                                break;

                            case LaserState.Shooting:
                                {
                                    if (!shootingSoundPlayed) {
                                        GetComponent<AudioSource>().clip = shooting;
                                        GetComponent<AudioSource>().volume = 0.7f;
                                        GetComponent<AudioSource>().pitch = 1f;
                                        GetComponent<AudioSource>().Play();
                                        chargingSoundPlayed = false;
                                        shootingSoundPlayed = true;
                                    }
                                    isCharging = false;
                                    //laserBeamGO = (GameObject)Instantiate(laserBeam, laserOrigin.transform.position, laserOrigin.transform.rotation, laserOrigin.transform);
                                    //laserBeamGO.transform.LookAt(player.transform.position);
                                    //laserBeamGO.transform.LookAt(playerLastPos);
                                    EyeBeam();
                                    StartCoroutine(TimeTillLaserFinish(4));
                                    
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
                        if (!dyingSoundPlayed) {
                            GetComponent<AudioSource>().clip = slamRoar;
                            GetComponent<AudioSource>().volume = 1f;
                            GetComponent<AudioSource>().pitch = 1f;
                            GetComponent<AudioSource>().Play();
                            dyingSoundPlayed = true;
                        }
                        enmarAnim.SetBool("EnmarDead", true);
                        enmarDied = true;
                        Destroy(laserChargeGO);
                        Destroy(laserWarningCircleGO);
                        //iTween.RotateTo(gameObject, new Vector3(0, 80, 0), 3);
                    }
                    break;

                case FSMState.GameOver:
                    {
                        enmarAnim.SetTrigger("GameOver");

                        switch (gameoverState)
                        {
                            case 1:
                                {
                                    attackTime = 0;
                                    gameoverState = 2;
                                    
                                }
                                break;

                            case 2:
                                {

                                    StartCoroutine(WaitToShootFinalLaser(3));
                                }
                                break;

                            case 3:
                                {
                                    EyeBeam();
                                    StartCoroutine(TimeTillFinalLaserFinish(3));
                                }
                                break;

                            case 4:
                                {

                                }
                                break;
                        }


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
            //playerLastPos = player.transform.position;
            playerLastPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }

        else
        {
            playerLastPos = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
        }
        
    }

    public void ChargeLaser()
    {
        isCharging = true;
        
        //if(time < )
        laserChargeGO = (GameObject)Instantiate(laserCharge, laserOrigin.transform.position, laserOrigin.transform.rotation,laserOrigin.transform);
        laserChargeGO.SetActive(true);
        chargePulse = laserChargeGO.transform.GetChild(0).transform;
        //chargePulse.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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

    public void EyeBeam()
    {
        GameObject L1 = (GameObject)Instantiate(newLaserBeam, laserOrigin.transform.position, laserOrigin.transform.rotation);
        L1.SetActive(true);
        L1.GetComponent<BeamParam>().SetBeamParam(L1.GetComponent<BeamParam>());

        GameObject L2 = (GameObject)Instantiate(newLaserInnerBeam, laserOrigin.transform.position, laserOrigin.transform.rotation);
        L2.SetActive(true);
        L2.GetComponent<BeamParam>().SetBeamParam(L2.GetComponent<BeamParam>());
    }

    public void PrimeRightHand()
    {
        //rightHand.SetActive(true);
        //rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, new Vector3(rightHand.transform.position.x, 20, rightHand.transform.position.z), Time.deltaTime);
        enmarAnim.SetTrigger("AttackRight");
        shootingSoundPlayed = false;
        isRightHandAttack = true;
        StartCoroutine(DelayWarningCircleRight());
        GetComponent<AudioSource>().clip = slamRoar;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().pitch = 1f;
        GetComponent<AudioSource>().Play();
        //enmarState = FSMState.AttackDelayState;
    }

    public void PrimeLeftHand()
    {
        //leftHand.SetActive(true);
        //leftHand.GetComponent<Rigidbody>().AddForce(Vector3.down * 100000);
        //leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, new Vector3(leftHand.transform.position.x, 20, leftHand.transform.position.z), Time.deltaTime);
        enmarAnim.SetTrigger("AttackLeft");
        shootingSoundPlayed = false;
        isLeftHandAttack = true;
        StartCoroutine(DelayWarningCircleLeft());
        GetComponent<AudioSource>().clip = slamRoar;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().pitch = 1f;
        GetComponent<AudioSource>().Play();
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
        //Level1Controller.instance.currentWallHealth -= wallDamage;
    }

    public void shakeCamera()
    {
        player.GetComponent<CameraShake>().ShakeCamera(0.3f, 0.3f);
        GetComponent<AudioSource>().clip = stompSound;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().pitch = 1f;
        GetComponent<AudioSource>().Play();
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

    public void PlaySlamSound() {
        GetComponent<AudioSource>().clip = slam;
        GetComponent<AudioSource>().pitch = 0.5f;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().Play();
    }

    #region IEnumerators

    private IEnumerator TimeTillLaserFinish(float sec)
    {
        yield return new WaitForSeconds(sec);
        laserStatus = LaserState.ShootFinish;
    }

    private IEnumerator WaitToShootFinalLaser(float sec)
    {
        yield return new WaitForSeconds(sec);
        laserChargeTime += Time.deltaTime;
        if (isCharging == false)
        {
            ChargeLaser();
        }

        //float lerpValue = time / 3;
        //lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

        //chargePulse.localScale = Vector3.Lerp(chargePulse.localScale, new Vector3(10, 10, 10), Time.deltaTime * 0.5f);

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
            laserOrigin.transform.LookAt(gameoverTargetPoint.position);
            gameoverState = 3;

        }
    }

    private IEnumerator TimeTillFinalLaserFinish(float sec)
    {
        yield return new WaitForSeconds(sec);
        gameoverState = 4;
    }

    private IEnumerator DelayWarningCircleRight()
    {
        yield return new WaitForSeconds(2f);
        laserWarningCircleGO = (GameObject)Instantiate(laserWarningCircle, rightSide.transform.position, rightSide.transform.rotation);
    }

    private IEnumerator DelayWarningCircleLeft()
    {
        yield return new WaitForSeconds(2f);
        laserWarningCircleGO = (GameObject)Instantiate(laserWarningCircle, leftSide.transform.position, leftSide.transform.rotation);

    }

    #endregion


    public void CheatToSetEnmarHealthTo20()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            enmarCurrentHealth = 20;
        }
    }
}
