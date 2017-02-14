using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {

    public float fireballDamage;

    public int time = 2;

    private float seconds = 0;
    public ObjectPooling pooling;

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
            pooling.DevolveInstance(gameObject);
            seconds = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Health.instance.currentHealth -= fireballDamage;
            pooling.DevolveInstance(gameObject);
        }

        //else
        //{
        //    pooling.DevolveInstance(gameObject);
        //}
    }
}
