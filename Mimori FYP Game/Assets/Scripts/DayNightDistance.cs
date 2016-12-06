using UnityEngine;
using System.Collections;

public class DayNightDistance : MonoBehaviour {
    public GameObject player;
    public float proximity;
    public float distance;
    public bool ForestNightFallActivate;

    public static DayNightDistance instance { get; set; }
    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            proximity = (transform.position - player.transform.position).magnitude;
            distance = 1 - (proximity / 300);
            //Debug.Log(distance*1500);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            ForestNightFallActivate = true;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            ForestNightFallActivate = false;
        }
    }

}
