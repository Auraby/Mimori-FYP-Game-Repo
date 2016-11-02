using UnityEngine;
using System.Collections;

public class EnmarController : MonoBehaviour {

    #region FSM
    public enum FSMState { Walking, Attacking, LaserAttack, Dying}
    public enum LaserState { Charging, Shooting, ShootFinish}

    public FSMState enmarState;
    public LaserState laserStatus;
    #endregion

    #region Normal Attack
    //Gameobjects
    public GameObject attackArea1, attackArea2;

    public bool hasEnteredArea1 = false;
    public bool hasEnteredArea2 = false;

    //Float
    public float slamDamage = 20;

    #endregion

    #region Laser Attack
    //Gameobjects
    public GameObject laserBeam;
    public GameObject laserCharge;
    public GameObject laserOrigin;
    GameObject laserChargeGO;
    Transform chargepulse;
    public GameObject player;
    public GameObject pointA, pointB;

    //bool
    public bool isCharing = false;

    //Float
    public float rotationValue;
    public float laserDamage = 10;

    //Variables
    public float time;
    public float laserChargeTime;

    #endregion

    #region Health

    public float enmarMaxHealth = 100;

    #endregion

    public static EnmarController instance { get; set; }

    // Use this for initialization
    void Start () {

        instance = this;
       // enmarState = FSMState.Walking;
        enmarState = FSMState.Attacking;
	}
	
	// Update is called once per frame
	void Update () {
        //laserBeam.transform.Rotate(5, 10, 5);
        DetectAreaToAttack();
        //enmarHead.transform.Rotate()
        time += Time.deltaTime;
        switch (enmarState)
        {
            case FSMState.Walking:
                {

                }
                break;

            case FSMState.Attacking:
                {
                    //if(time > 3)
                    //{
                    //    enmarState = FSMState.LaserAttack;
                    //}
                    isCharing = false;
                }
                break;

            case FSMState.LaserAttack:
                {
                    switch (laserStatus)
                    {
                        case LaserState.Charging:
                            {
                                laserChargeTime += Time.deltaTime;
                                if (isCharing == false)
                                {
                                    ChargeLaser();
                                }

                                float lerpValue = time / 3;
                                lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

                                chargepulse.localScale = Vector3.Lerp(chargepulse.localScale, new Vector3(10, 10, 10), Time.deltaTime * 0.5f);

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
                                isCharing = false;
                                laserStatus = LaserState.ShootFinish;
                            }
                            break;

                        case LaserState.ShootFinish:
                            {

                                enmarState = FSMState.Attacking;
                                laserStatus = LaserState.Charging;
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


    public void slamHands()
    {

    }

    public void DetectAreaToAttack()
    {
        if(hasEnteredArea1 == true)
        {
            Debug.Log("Area 1 entered");
        }

        if(hasEnteredArea2 == true)
        {
            Debug.Log("Area 2 entered");
        }
    }

    public void ChargeLaser()
    {
        isCharing = true;
        
        //if(time < )
        laserChargeGO = (GameObject)Instantiate(laserCharge, laserOrigin.transform.position, laserOrigin.transform.rotation,laserOrigin.transform);
        chargepulse = laserChargeGO.transform.GetChild(0).transform;
        chargepulse.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        laserChargeGO.transform.position = laserOrigin.transform.position;
        laserChargeGO.transform.rotation = laserOrigin.transform.rotation; 
       
    }
}
