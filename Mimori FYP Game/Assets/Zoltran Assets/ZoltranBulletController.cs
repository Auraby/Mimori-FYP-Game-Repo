using UnityEngine;
using System.Collections;

public class ZoltranBulletController : MonoBehaviour {

    public int time = 2;

    private float seconds = 0;
    private ObjectPooling pooling;

    void Start()
    {
        pooling = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPooling>();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cover")
        {
            pooling.DevolveInstance(gameObject);
        }

        if(other.gameObject.tag == "Player")
        {
            //Damage player here
            pooling.DevolveInstance(gameObject);
        }
    }
}
