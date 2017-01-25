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
    //public float rotateSpeed;
    //public bool isIllusion = true;






    public static ZoltranIllusionController instance { get; set; }
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Bullet")
    //    {
    //        illusionCurrentHealth -= 5;
    //    }
    //}

}


