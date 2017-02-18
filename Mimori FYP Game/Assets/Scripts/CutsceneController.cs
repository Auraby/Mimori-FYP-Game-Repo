using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour {

    AsyncOperation asyncOp;

    //public Slider loadingBar;
    //public Text loadingText;

	// Use this for initialization
	void Start () {
        asyncOp = SceneManager.LoadSceneAsync("Gate of Telluris");
        asyncOp.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown)
        {
            //If a save file exists, delete it
            GameController.gameController.Delete();
            asyncOp.allowSceneActivation = true;
        }
    }
}
