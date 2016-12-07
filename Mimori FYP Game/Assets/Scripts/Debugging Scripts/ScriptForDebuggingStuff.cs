using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScriptForDebuggingStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Mimori");
        }
    }
}
