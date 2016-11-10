using UnityEngine;
using System.Collections;

public class ForestNightFall : MonoBehaviour {
	public bool ForestNightFallActivate;

	public static ForestNightFall instance { get; set; }
	// Use this for initialization
	void Start () {
		instance = this;
	}

	void OnTriggerEnter(Collider obj){
		ForestNightFallActivate = true;	
	}

	// Update is called once per frame
	void Update () {
		 

	}
}
