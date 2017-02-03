using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;

    bool gamePaused = false;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        Debug.Log(gamePaused);
        if (Input.GetKeyDown(KeyCode.O)) {
            if (!gamePaused) {
                pauseMenu.gameObject.SetActive(true);
                gamePaused = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;
            }
        }
	}
}
