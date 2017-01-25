using UnityEngine;
using System.Collections;

public class RotateSkybox : MonoBehaviour {
    public Material skybox;

    float rotate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rotate += Time.deltaTime * 0.25f;
        skybox.SetFloat("_Rotation", rotate);
        
	}
}
