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
            //Debug.Log("Async Working");
        }
        if (SceneManager.GetActiveScene().name == "Mimori")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Forest of Misery");
            //Debug.Log("Async Working");
        }
        if (SceneManager.GetActiveScene().name == "Forest of Misery")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Temple of Aphelion");
            //Debug.Log("Async Working");
        }
        if (SceneManager.GetActiveScene().name == "Temple of Aphelion")
        {
            aSyncOp = SceneManager.LoadSceneAsync("Lost Crater");
            //Debug.Log("Async Working");
        }
        aSyncOp.allowSceneActivation = false;

        Debug.Log(SceneManager.GetActiveScene().name);
        //Debug.Log("is it starting async? " + aSyncOp.progress);
    }
	
	// Update is called once per frame
	void Update () {
       // if (aSyncOp.allowSceneActivation == true)
       // {
       // }
	}

    void OnTriggerEnter(Collider others) {
        if (others.gameObject.tag == "Player") {
            Debug.Log("GO");
            //aSyncOp.allowSceneActivation = true;

            if (SceneManager.GetActiveScene().name == "Gate of Telluris")
            {
                //aSyncOp = SceneManager.LoadSceneAsync("Mimori");
                //Debug.Log("Async Working");
                SceneManager.LoadScene("Mimori");
            }
            if (SceneManager.GetActiveScene().name == "Mimori")
            {
                //aSyncOp = SceneManager.LoadSceneAsync("Forest of Misery");
                //Debug.Log("Async Working");
                SceneManager.LoadScene("Forest of Misery");
            }
            if (SceneManager.GetActiveScene().name == "Forest of Misery")
            {
                //aSyncOp = SceneManager.LoadSceneAsync("Temple of Aphelion");
                //Debug.Log("Async Working");
                SceneManager.LoadScene("Temple of Aphelion");
            }
            if (SceneManager.GetActiveScene().name == "Temple of Aphelion")
            {
                //aSyncOp = SceneManager.LoadSceneAsync("Lost Crater");
                //Debug.Log("Async Working");
                SceneManager.LoadScene("Lost Crater");
            }
        }
    }
}
