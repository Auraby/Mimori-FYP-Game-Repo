using UnityEngine;
using System.Collections;

public class EnmarController : MonoBehaviour {

    #region FSM
    public enum FSMState { Walking, Attacking, LaserAttack, Dying}

    FSMState enmarState;
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
    public GameObject enmarHead;
    public GameObject player;
    public GameObject pointA, pointB;

    //Float
    public float rotationValue;
    public float laserDamage = 10;

    //Variables


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

        switch (enmarState)
        {
            case FSMState.Walking:
                {

                }
                break;

            case FSMState.Attacking:
                {

                }
                break;

            case FSMState.LaserAttack:
                {

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
}
