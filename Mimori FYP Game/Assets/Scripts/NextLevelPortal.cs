using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevelPortal : MonoBehaviour {

    public enum WhichScenes { MainMenu, Gate, Mimori, Forest, Temple, Crater}

    //public WhichScenes currScene;

    AsyncOperation asyncOp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            //asyncOp = SceneManager.LoadSceneAsync("Forest of Misery");
            //asyncOp.allowSceneActivation = false;
            SceneManager.LoadScene("Forest of Misery");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //asyncOp = SceneManager.LoadSceneAsync("Temple of Aphelion");
            //asyncOp.allowSceneActivation = false;
            SceneManager.LoadScene("Temple of Aphelion");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //asyncOp = SceneManager.LoadSceneAsync("Lost Crater");
            //asyncOp.allowSceneActivation = false;
            SceneManager.LoadScene("Lost Crater");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            asyncOp.allowSceneActivation = true;
        }
	}
}
