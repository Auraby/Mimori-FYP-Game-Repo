using UnityEngine;
using System.Collections;

public class RotateSkybox : MonoBehaviour {
    public Material skybox;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        skybox.SetFloat("_Rotation", Time.deltaTime);
	}
}
