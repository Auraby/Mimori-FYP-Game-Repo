using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DebuggingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, Color.red);
        //Debug.DrawLine(gameObject.transform.position, gameObject.transform.forward, Color.red,100,false);
    }
}
