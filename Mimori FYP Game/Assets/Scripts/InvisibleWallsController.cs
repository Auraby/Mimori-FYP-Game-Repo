using UnityEngine;
using System.Collections;

public class InvisibleWallsController : MonoBehaviour {

    //public Collider[] CollidersToIgnoreArray;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //public void IgnoreEverythingExceptPLayer()
    //{
    //    foreach (Collider col in CollidersToIgnoreArray)
    //    {
    //        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), col);
    //    }
    //}

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
    }
}
