using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    public Camera camera;
    public GameObject player;
    public GameObject mainCanvas;
    public Image gameoverBlackPanel;
    public Text gameoverText, gameoverTextSubtitle;
    public Button saveGameBtn, loadGameBtn;

    //CursorLockMode cursMode;
    AsyncOperation aSyncOp;
	// Use this for initialization
	void Start () {
        aSyncOp = SceneManager.LoadSceneAsync("MainMenu");
        aSyncOp.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update() {
        //if no save file exists, load game button is disabled
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.mi"))
        {
            loadGameBtn.interactable = true;
        }
        else
        {
            loadGameBtn.interactable = false;
        }
        //unable to save during boss fight
        if (!GameController.gameController.fightingBoss)
        {
            saveGameBtn.interactable = true;
        }
        else
        {
            saveGameBtn.interactable = false;
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            if (!FirstPersonController.isPaused) {
                mainCanvas.SetActive(false);
                pauseMenu.gameObject.SetActive(true);
                FirstPersonController.isPaused = true;
                Time.timeScale = 0;
                camera.enabled = false;
                Cursor.visible = true;
                Player.curseMode = CursorLockMode.None;
            }
            else
            {
                ResumeGame();
            }
        }
	}

    public void SaveGame() {
        ResumeGame();

        GameController.gameController.playerPositionX = player.transform.position.x;
        GameController.gameController.playerPositionY = player.transform.position.y;
        GameController.gameController.playerPositionZ = player.transform.position.z;

        GameController.gameController.Save();
    }

    public void LoadGame() {
        ResumeGame();

        GameController.gameController.Load();
        player.transform.position = new Vector3(
            GameController.gameController.playerPositionX,
            GameController.gameController.playerPositionY,
            GameController.gameController.playerPositionZ
        );
    }

    public void ToMainMenu()
    {
        //aSyncOp.allowSceneActivation = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ResumeGame()
    {
        mainCanvas.SetActive(true);
        gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
        gameoverText.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
        pauseMenu.gameObject.SetActive(false);
        FirstPersonController.isPaused = false;
        Time.timeScale = 1;
        camera.enabled = true;
        Cursor.visible = false;
        Player.curseMode = CursorLockMode.Locked;
    }
}
