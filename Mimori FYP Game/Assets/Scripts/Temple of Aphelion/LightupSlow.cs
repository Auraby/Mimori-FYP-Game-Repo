using UnityEngine;
using System.Collections;

public class LightupSlow : MonoBehaviour {
    public static bool startLight = false;
	// Use this for initialization
	void Start () {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.GetChild(0).GetComponent<Light>().intensity < 2 && startLight) {
            transform.GetChild(0).GetComponent<Light>().intensity += Time.deltaTime * 0.25f;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
        }
        
	}
}
