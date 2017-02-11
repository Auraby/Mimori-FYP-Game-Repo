using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextScenePortal : MonoBehaviour {
    AsyncOperation aSyncOp;
    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "Gate of Telluris")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Mimori");
        }
        if (SceneManager.GetActiveScene().name == "Mimori")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Forest of Misery");
        }
        if (SceneManager.GetActiveScene().name == "Forest of Misery")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Temple of Aphelion");
        }
        if (SceneManager.GetActiveScene().name == "Temple of Aphelion")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Lost Crater");
        }
        aSyncOp.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider others) {
        if (others.gameObject.tag == "Player") {
            Debug.Log("GO");
            aSyncOp.allowSceneActivation = true;
        }
    }
}
