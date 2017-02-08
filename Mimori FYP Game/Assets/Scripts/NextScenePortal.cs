using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextScenePortal : MonoBehaviour {
    AsyncOperation aSyncOp;
    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "Mimori")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Forest of Misery");
        }
        if (SceneManager.GetActiveScene().name == "Forest of Misery")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Temple of Aphelion");
        }
        aSyncOp.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider others) {
        if (others.gameObject.tag == "Player") {
            aSyncOp.allowSceneActivation = true;
        }
    }
}
