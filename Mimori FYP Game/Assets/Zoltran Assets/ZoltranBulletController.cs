using UnityEngine;
using System.Collections;

public class ZoltranBulletController : MonoBehaviour {

    public int time = 2;
    public float bulletDamage = 2;

    private float seconds = 0;
    public GameObject pooling;

    void Start()
    {
        //pooling = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPooling>();
    }

    void Update()
    {
        seconds += Time.deltaTime;

        if (seconds >= time)
        {
            //Debug.Log("devolvido");
            pooling.GetComponent<ObjectPooling>().DevolveInstance(gameObject);
            seconds = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag != "Player")
        //{
        //    pooling.DevolveInstance(gameObject);
        //}

        if(other.gameObject.tag == "Player")
        {
            //Damage player here
            pooling.GetComponent<ObjectPooling>().DevolveInstance(gameObject);
            Health.instance.currentHealth -= bulletDamage;
        }
    }
}
