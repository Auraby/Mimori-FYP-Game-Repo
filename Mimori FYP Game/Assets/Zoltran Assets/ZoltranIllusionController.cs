using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoltranIllusionController : MonoBehaviour
{

    public enum IllusionStates { Start, FindWaypoint, Moving, Attacking, Vanish, Appear, Dying }

    [Header("Illusion Properties")]
    public IllusionStates currentState;
    public float illusionMaxHealth = 100;
    public float illusionCurrentHealth;
    public float rotateSpeed;
    public bool isIllusion = true;

    #region Attacks
    //public List<AttackInfo> actions = new List<AttackInfo>();
    //private float attackTime;
    //private int attackRNG;
    //private Transform playerTarget;
    [Header("Bullet Properties")]
    public GameObject soulShotBullet;
    public GameObject tryShot;
    public GameObject specialTryShot;
    GameObject soulShotGO;
    GameObject tryShotGO;
    GameObject specialTryShotGO;

    [Header("Bullet Spawn Points")]
    public GameObject mouthEnd;
    public GameObject orbEnd1;
    public GameObject orbEnd2;
    public GameObject orbEnd3;


    [Header("SoulShot Properties")]
    public float ssfireRate;
    public float ssBulletSpeed;
    float ssnextFire;

    [Header("Tryshot Properties")]
    public float tsFireRate;
    public float tsBulletSpeed;
    float tsNextFire;

    [Header("Spiral Burst Properties")]
    public float burstTurnRate;
    public float sbFireRate;
    public float sbBulletSpeed;
    #endregion


    #region Misc
    [Header("Misc")]
    public GameObject player;
    NavMeshAgent navAgent;
    public GameObject MovementArea;
    public float temptime = 0;
    Vector3 newWaypoint;
    #endregion




    public static ZoltranIllusionController instance { get; set; }
    // Use this for initialization
    void Start()
    {
        instance = this;
        navAgent = GetComponent<NavMeshAgent>();
        currentState = IllusionStates.FindWaypoint;
        illusionCurrentHealth = illusionMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(orbEnd1.transform.position, orbEnd1.transform.forward, Color.red);

        //SpiralBurst();
        switch (currentState)
        {
            case IllusionStates.Start:
                {

                }
                break;

            case IllusionStates.FindWaypoint:
                {
                    //Find random point to move to
                    newWaypoint = generateRandomPositionInArea();
                    currentState = IllusionStates.Moving;
                }
                break;

            case IllusionStates.Moving:
                {
                    navAgent.Resume();
                    navAgent.SetDestination(newWaypoint);
                    if (gameObject.transform.position == newWaypoint)
                    {
                        RotateTowardsPlayer();
                        //currentState = ZoltranStates.Attacking;
                        StartCoroutine(SwitchToAttackState());
                    }
                }
                break;

            case IllusionStates.Attacking:
                {
                    //transform.LookAt(player.transform.position);

                    navAgent.Stop();
                    //if (Time.time > ssnextFire)
                    //{
                    //    ssnextFire = Time.time + ssfireRate;
                    //    SoulChain();

                    //}
                    if (illusionCurrentHealth > 50)
                    {
                        SoulShot();
                    }

                    if (illusionCurrentHealth <= 50)
                    {
                        int rng = Random.Range(0, 2);

                        switch (rng)
                        {
                            case 0:
                                {
                                    SoulShot();
                                }
                                break;

                            case 1:
                                {
                                    SpiralBurst();
                                }
                                break;
                        }
                    }

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
                        currentState = IllusionStates.FindWaypoint;
                        temptime = 0;
                    }
                }
                break;

            case IllusionStates.Vanish:
                {
                    gameObject.transform.position = new Vector3(1000, 0, 1000);
                    newWaypoint = generateRandomPositionInArea();
                    temptime = 0;
                    StartCoroutine(SwitchToAppearState());

                }
                break;


            case IllusionStates.Appear:
                {
                    gameObject.transform.position = newWaypoint;
                    RotateTowardsPlayer();
                    StartCoroutine(SwitchToAttackState());
                }
                break;

            case IllusionStates.Dying:
                {

                }
                break;

            default:
                {

                }
                break;
        }




    }

    void FixedUpdate()
    {
        CheckPlayerProximity();
    }

    public void CheckPlayerProximity()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(gameObject.transform.position, 4);
        foreach (Collider col in objectsInRange)
        {
            if (col.gameObject.tag == "Player")
            {
                currentState = IllusionStates.Vanish;
            }
        }
    }

    public void AvoidObjectCollision()
    {

    }


    public Vector3 generateRandomPositionInArea()
    {
        float xTerrainMin = MovementArea.GetComponent<Collider>().bounds.min.x;
        float xTerrainMax = MovementArea.GetComponent<Collider>().bounds.max.x;
        float zTerrainMin = MovementArea.GetComponent<Collider>().bounds.min.z;
        float zTerrainMax = MovementArea.GetComponent<Collider>().bounds.max.z;
        Vector3 position = new Vector3(Random.Range(xTerrainMin, xTerrainMax), 0, Random.Range(zTerrainMin, zTerrainMax));
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 10f, 1);
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


    public void SoulShot()
    {
        //orbEnd2.transform.LookAt(player.transform.position);
        //orbEnd3.transform.LookAt(player.transform.position);
        RotateTowardsPlayer();

        if (Time.time > ssnextFire)
        {
            ssnextFire = Time.time + ssfireRate;
            soulShotGO = (GameObject)Instantiate(soulShotBullet, orbEnd2.transform.position, orbEnd2.transform.rotation);
            soulShotGO.GetComponent<Rigidbody>().velocity = orbEnd2.transform.forward * ssBulletSpeed;

            soulShotGO = (GameObject)Instantiate(soulShotBullet, orbEnd3.transform.position, orbEnd3.transform.rotation);
            soulShotGO.GetComponent<Rigidbody>().velocity = orbEnd3.transform.forward * ssBulletSpeed;
        }

        //soulShotGO.transform.LookAt(player.transform.position);

        //yield return new WaitForSeconds(1f);

    }

    public void SpiralBurst()
    {
        orbEnd1.transform.RotateAround(orbEnd1.transform.position, orbEnd1.transform.up, Time.deltaTime * 90f);
        if (Time.time > tsNextFire)
        {
            tsNextFire = Time.time + tsFireRate;
            //    SoulChain();
            tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(0).position, orbEnd1.transform.GetChild(0).rotation);
            tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(0).forward * tsBulletSpeed;

            tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(2).position, orbEnd1.transform.GetChild(2).rotation);
            tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(2).forward * tsBulletSpeed;

            tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(4).position, orbEnd1.transform.GetChild(4).rotation);
            tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(4).forward * tsBulletSpeed;

            tryShotGO = (GameObject)Instantiate(tryShot, orbEnd1.transform.GetChild(6).position, orbEnd1.transform.GetChild(6).rotation);
            tryShotGO.GetComponent<Rigidbody>().velocity = orbEnd1.transform.GetChild(6).forward * tsBulletSpeed;
        }
    }

    public void Tryshot()
    {

    }

    public void ResetRotations()
    {
        orbEnd1.transform.rotation = Quaternion.identity;
    }


    #region IEnumerators
    private IEnumerator SwitchToAttackState()
    {

        yield return new WaitForSeconds(1);
        currentState = IllusionStates.Attacking;
    }

    private IEnumerator SwitchToAppearState()
    {
        yield return new WaitForSeconds(2);
        currentState = IllusionStates.Appear;
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


