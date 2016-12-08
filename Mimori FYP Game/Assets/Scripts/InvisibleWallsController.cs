using UnityEngine;
using System.Collections;

public class InvisibleWallsController : MonoBehaviour {

    public Collider[] CollidersToIgnoreArray;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void IgnoreEverythingExceptPLayer()
    {
        foreach (Collider col in CollidersToIgnoreArray)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), col);
        }
    }
}
