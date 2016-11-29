using UnityEngine;
using System.Collections;

public class EnmarFistController : MonoBehaviour {
    public enum WhichFist { RightFist, LeftFist}

    public WhichFist thisFist;

    public static EnmarFistController instance { get; set; }
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Health.instance.currentHealth -= 20.0f;
            Health.instance.healthbarslider.value -= 20.0f;
        }
    }
}
