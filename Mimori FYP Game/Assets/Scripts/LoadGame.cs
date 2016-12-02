using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load(){
		GameController.loadingGame = true;
		GameController.gameController.Load ();
		SceneManager.LoadScene (GameController.gameController.currentScene.ToString());
	}
}
